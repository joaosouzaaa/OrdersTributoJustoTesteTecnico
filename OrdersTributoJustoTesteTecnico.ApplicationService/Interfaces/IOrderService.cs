using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces
{
    public interface IOrderService
    {
        Task<bool> DeleteOrderAsync(int id);
        Task<bool> AddOrderAsync(OrderSaveRequest orderSaveRequest);
        Task<bool> AddProductToOrderAsync(OrderUpdateRequest orderUpdateRequest);
        Task<bool> RemoveProductFromOrderAsync(OrderUpdateRequest orderUpdateRequest);
        Task<OrderResponse> GetOrderByIdAsync(int id);
        Task<List<OrderResponse>> GetAllOrdersAsync();
        Task<PageList<OrderResponse>> GetAllOrdersWithPaginationAsync(PageParams pageParams);
    }
}
