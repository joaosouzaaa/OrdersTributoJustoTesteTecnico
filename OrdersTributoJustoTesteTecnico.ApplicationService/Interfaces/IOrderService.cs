using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces
{
    public interface IOrderService
    {
        Task<bool> DeleteAsync(int id);
        Task<bool> AddOrderAsync(OrderSaveRequest orderSaveRequest);
        Task<bool> AddProductAsync(OrderUpdateRequest orderUpdateRequest);
        Task<bool> RemoveProductAsync(OrderUpdateRequest orderUpdateRequest);
        Task<OrderResponse> GetByIdAsync(int id);
        Task<List<OrderResponse>> GetAllAsync();
        Task<PageList<OrderResponse>> FindAllWithPaginationAsync(PageParams pageParams);
    }
}
