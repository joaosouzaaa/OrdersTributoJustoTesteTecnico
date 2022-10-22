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

        [HttpGet("get-by-id")]
        public async Task<OrderResponse> GetOrderByIdAsync([FromQuery] int id) =>
            await _orderService.GetOrderByIdAsync(id);

        [HttpGet("get-all")]
        public async Task<List<OrderResponse>> GetAllOrdersAsync() =>
            await _orderService.GetAllOrdersAsync();

        [HttpGet("get-all-paginated")]
        public async Task<PageList<OrderResponse>> GetAllOrdersWithPaginationAsync([FromQuery] PageParams pageParams) =>
            await _orderService.GetAllOrdersWithPaginationAsync(pageParams);

        [HttpPost("create-order")]
        public async Task<bool> AddOrderAsync([FromBody] OrderSaveRequest orderSaveRequest) =>
            await _orderService.AddOrderAsync(orderSaveRequest);

        [HttpPut("add-product")]
        public async Task<bool> AddProductToOrderAsync([FromBody] OrderUpdateRequest orderUpdateRequest) =>
            await _orderService.AddProductToOrderAsync(orderUpdateRequest);

        [HttpPut("remove-product")]
        public async Task<bool> RemoveProductFromOrderAsync([FromBody] OrderUpdateRequest orderUpdateRequest) =>
            await _orderService.RemoveProductFromOrderAsync(orderUpdateRequest);

        [HttpDelete("delete-order")]
        public async Task<bool> DeleteOrderAsync([FromQuery] int id) =>
            await _orderService.DeleteOrderAsync(id);
    }
}
