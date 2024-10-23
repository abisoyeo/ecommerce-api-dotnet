using EcommerceApiSnapnetTestApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApiSnapnetTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IEcommerceRepo ecommerceRepo;
        public ProductsController(IEcommerceRepo ecommerceRepo)
        {
            this.ecommerceRepo = ecommerceRepo;
        }

        // GET: api/<ProductsController>
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var result = await ecommerceRepo.GetAllProducts();
                if (!result.Any())
                    return StatusCode(204, new { Message = "No products" });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "An internal error occurred while processing the request", Details = ex.Message });
            }
        }

        // POST api/<ProductsController>
        [HttpPost("AddProducts")]
        public async Task<IActionResult> AddProducts([FromBody] ProductsDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = ecommerceRepo.Add(value);

            return new JsonResult(result);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("EditProducts/{id}")]
        public async Task<IActionResult> EditProducts([FromBody] ProductsModel value)
        {
            var result = ecommerceRepo.Update(value);

            return new JsonResult(result);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("DeleteProducts/{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var result = ecommerceRepo.Delete(id);

            return new JsonResult(result);
        }
    }
}
