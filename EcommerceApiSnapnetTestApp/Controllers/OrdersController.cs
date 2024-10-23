using EcommerceApiSnapnetTestApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public OrdersController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("history")]
    public async Task<IActionResult> OrderHistory()
    {
        var customerId = GetCustomerIdFromToken();
        var customerOrders = await dbContext.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.Products)
            .ToListAsync();

        if (!customerOrders.Any())
            return NotFound(new { Message = "No orders found for this customer." });

        return Ok(customerOrders);
    }

    [HttpPost("place")]
    public async Task<IActionResult> PlaceOrder([FromBody] Orders newOrder)
    {
        var customerId = GetCustomerIdFromToken();
        newOrder.CustomerId = customerId;
        newOrder.OrderDate = DateTime.UtcNow;

        dbContext.Orders.Add(newOrder);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(OrderHistory), new { customerId = newOrder.CustomerId }, newOrder);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] bool status)
    {
        var existingOrder = await dbContext.Orders.FindAsync(id);
        if (existingOrder == null)
            return NotFound(new { Message = "Order not found." });

        existingOrder.Status = status;
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    private int GetCustomerIdFromToken()
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        return int.Parse(customerIdClaim);
    }
}
