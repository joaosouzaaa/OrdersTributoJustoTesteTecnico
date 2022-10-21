using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;

namespace OrdersTributoJustoTesteTecnico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get_by_id")]
        public async Task<OrderResponse> GetByIdAsync([FromQuery] int id) =>
            await _orderService.GetByIdAsync(id);

        [HttpGet("get_all")]
        public async Task<List<OrderResponse>> GetAllAsync() =>
            await _orderService.GetAllAsync();

        [HttpGet("get_all_paginated")]
        public async Task<PageList<OrderResponse>> GetAllWithPaginationAsync([FromQuery] PageParams pageParams) =>
            await _orderService.GetAllWithPaginationAsync(pageParams);

        [HttpPost("create")]
        public async Task<bool> AddOrderAsync([FromBody] OrderSaveRequest orderSaveRequest) =>
            await _orderService.AddOrderAsync(orderSaveRequest);

        [HttpPut("add_product")]
        public async Task<bool> AddProductAsync([FromBody] OrderUpdateRequest orderUpdateRequest) =>
            await _orderService.AddProductAsync(orderUpdateRequest);

        [HttpPut("remove_product")]
        public async Task<bool> RemoveProductAsync([FromBody] OrderUpdateRequest orderUpdateRequest) =>
            await _orderService.RemoveProductAsync(orderUpdateRequest);

        [HttpDelete("delete")]
        public async Task<bool> DeleteAsync([FromQuery] int id) =>
            await _orderService.DeleteAsync(id);
    }
}
