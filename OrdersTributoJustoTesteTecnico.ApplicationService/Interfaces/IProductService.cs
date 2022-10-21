using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddAsync(ProductSaveRequest productSaveRequest);
        Task<bool> UpdateAsync(ProductUpdateRequest productUpdateRequest);
        Task<bool> DeleteAsync(int id);
        Task<ProductImageResponse> GetByIdAsync(int id);
        Task<PageList<ProductResponse>> GetAllWithPaginationAsync(PageParams pageParams);
        Task<List<ProductResponse>> GetAllAsync();
        Task<Product> GetByIdAsyncReturnsDomainObject(int id);
    }
}
