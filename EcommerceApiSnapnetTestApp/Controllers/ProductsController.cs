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
        /// <summary>
        /// Get All Products Endpoint
        /// </summary>
        /// <returns>All products</returns>
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
        /// <summary>
        /// Add Products Endpoint
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Added Product</returns>
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
        /// <summary>
        /// Edit Products Endpoint
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Edited Product</returns>
        [HttpPut("EditProducts/{id}")]
        public async Task<IActionResult> EditProducts([FromBody] ProductsModel value)
        {
            var result = ecommerceRepo.Update(value);

            return new JsonResult(result);
        }

        // DELETE api/<ProductsController>/5
        /// <summary>
        /// Delete Products Endpoint
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Deleted Product</returns>
        [HttpDelete("DeleteProducts/{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var result = ecommerceRepo.Delete(id);

            return new JsonResult(result);
        }
    }
}
