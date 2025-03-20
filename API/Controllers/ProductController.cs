using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.CreateProductAsync(productDto);

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            if (!products.Any())
            {
                return NoContent();
            }

            return Ok(products);
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            await _productService.UpdateProductAsync(id, productUpdateDto);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProductsAsync(
            [FromQuery] string searchWord
        )
        {
            var products = await _productService.SearchProductsAsync(searchWord);
            if (!products.Any())
            {
                return NoContent();
            }
            return Ok(products);
        }

        //[HttpPatch]
        //[Route("{id}")]
        //public async Task<IActionResult> UpdateProduct(int id, Dictionary<string, object> updates)
        //{
        //    await _productService.UpdateProduct(id, updates);
        //    return Ok();
        //}
    }
}
