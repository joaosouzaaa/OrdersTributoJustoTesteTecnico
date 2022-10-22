using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersTributoJustoTesteTecnico.Api.ControllersAttributes;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;

namespace OrdersTributoJustoTesteTecnico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-product-by-id")]
        [QueryCommandsResponseTypes]
        public async Task<ProductImageResponse> GetProductByIdAsync([FromQuery] int id) =>
            await _productService.GetProductByIdAsync(id);

        [HttpGet("get-all")]
        [QueryCommandsResponseTypes]
        public async Task<List<ProductResponse>> GetAllProductsAsync() =>
            await _productService.GetAllProductsAsync();

        [HttpGet("get-all-paginated")]
        [QueryCommandsResponseTypes]
        public async Task<PageList<ProductResponse>> GetAllProductsWithPaginationAsync([FromQuery] PageParams pageParams) =>
            await _productService.GetAllProductsWithPaginationAsync(pageParams);

        [HttpPost("create-product")]
        [CommandsResponseTypes]
        public async Task<bool> AddProductAsync([FromForm] ProductSaveRequest productSaveRequest) =>
            await _productService.AddProductAsync(productSaveRequest);

        [HttpPut("update-product")]
        [CommandsResponseTypes]
        public async Task<bool> UpdateProductAsync([FromForm] ProductUpdateRequest productUpdateRequest) =>
            await _productService.UpdateProductAsync(productUpdateRequest);

        [HttpDelete("delete-product")]
        [CommandsResponseTypes]
        public async Task<bool> DeleteProductAsync(int id) =>
            await _productService.DeleteProductAsync(id);
    }
}
