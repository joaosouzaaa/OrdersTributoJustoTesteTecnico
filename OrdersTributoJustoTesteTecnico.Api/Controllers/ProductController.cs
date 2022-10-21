using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("get_by_id")]
        public async Task<ProductImageResponse> GetByIdAsync([FromQuery] int id) =>
            await _productService.GetByIdAsync(id);

        [HttpGet("get_all")]
        public async Task<List<ProductResponse>> GetAllAsync() =>
            await _productService.GetAllAsync();

        [HttpGet("get_all_paginated")]
        public async Task<PageList<ProductResponse>> GetAllWithPaginationAsync([FromQuery] PageParams pageParams) =>
            await _productService.GetAllWithPaginationAsync(pageParams);

        [HttpPost("create")]
        public async Task<bool> AddAsync([FromForm] ProductSaveRequest productSaveRequest) =>
            await _productService.AddAsync(productSaveRequest);

        [HttpPut("update")]
        public async Task<bool> UpdateAsync([FromForm] ProductUpdateRequest productUpdateRequest) =>
            await _productService.UpdateAsync(productUpdateRequest);

        [HttpDelete("delete")]
        public async Task<bool> DeleteAsync(int id) =>
            await _productService.DeleteAsync(id);
    }
}
