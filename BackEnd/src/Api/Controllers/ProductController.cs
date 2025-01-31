using System;
using System.Threading.Tasks;
using Application.Request.Product;
using Application.Response;
using Application.Response.Product;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet("v1/lists-products")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<ProductGetResponse>))]
        public async Task<IActionResult> GetAsync(int pageSize = 10, int page = 1)
        {
            var products = await _productService.GetAsync(pageSize, page);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("v1/product-by-id/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<ProductGetByIdResponse>))]
        public async Task<IActionResult> GetByAsync([FromRoute] Guid id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost("v1/created-product")]
        [ProducesResponseType(201, Type = typeof(BaseResponse<CreateProductResponse>))]
        public async Task<IResult> PostAsync([FromBody] CreateProductRequest request)
        {
            var result = await _productService.Create(request);

            return result.IsSuccess
                 ? TypedResults.Created($"v1/Product-by-id/{result.Data}", result)
                 : TypedResults.BadRequest(result.Data);
        }

        [HttpPut("v1/update-product/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<UpdateProductResponse>))]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, UpdateProductRequest request)
        {
            var product = await _productService.UpdateAsync(id, request);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpDelete("v1/remove-product/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<DeleteProductResponse>))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var product = await _productService.DeleteAsync(id);

            if (product is null)
            {
                return NoContent();
            }
            return Ok(product);

        }
    }
}
