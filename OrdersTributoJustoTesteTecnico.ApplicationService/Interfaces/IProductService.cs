using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(ProductSaveRequest productSaveRequest);
        Task<bool> UpdateProductAsync(ProductUpdateRequest productUpdateRequest);
        Task<bool> DeleteProductAsync(int id);
        Task<ProductImageResponse> GetProductByIdAsync(int id);
        Task<PageList<ProductResponse>> GetAllProductsWithPaginationAsync(PageParams pageParams);
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<Product> GetByIdAsyncReturnsDomainObject(int id);
    }
}
