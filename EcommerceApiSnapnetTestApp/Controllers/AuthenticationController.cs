using EcommerceApiSnapnetTestApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;

    public AuthenticationController(IConfiguration configuration, ApplicationDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    [HttpPost("token")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticationData data)
    {
        var user = await ValidateCredentialsAsync(data);
        if (user == null) return Unauthorized();

        var token = GenerateToken(user);
        return Ok(token);
    }

    private string GenerateToken(CustomerModel customer)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretKey"]));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new(JwtRegisteredClaimNames.Sub, customer.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, customer.CustomerName)
        };

        var token = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<CustomerModel?> ValidateCredentialsAsync(AuthenticationData data)
    {
        return await _dbContext.Customers
            .FirstOrDefaultAsync(c => c.CustomerName == data.UserName && c.Password == data.Password);
    }
}

public class AuthenticationData
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
