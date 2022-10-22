using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using OrdersTributoJustoTesteTecnico.ApplicationService.Services.BaseServices;
using OrdersTributoJustoTesteTecnico.Business.Extensions;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Validatation;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Domain.Enum;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Services
{
    public sealed class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotificationHandler notification, IValidate<Product> validate) : base(notification, validate)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> AddProductAsync(ProductSaveRequest productSaveRequest)
        {
            var product = productSaveRequest.MapTo<ProductSaveRequest, Product>();

            if (productSaveRequest.ImageToSave != null)
            {
                product.Image = productSaveRequest.ImageToSave.ImageToBytes();

                if (product.Image == null)
                    return _notification.AddDomainNotification("File format", "Imagem no formato inválido");
            }

            if (!await ValidateAsync(product))
                return false;

            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateRequest productUpdateRequest)
        {
            if (!await _productRepository.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id))
                return _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));

            var product = productUpdateRequest.MapTo<ProductUpdateRequest, Product>();
            
            if(productUpdateRequest.ImageToSave != null)
            {
                product.Image = productUpdateRequest.ImageToSave.ImageToBytes();

                if (product.Image == null)
                    return _notification.AddDomainNotification("File format", "Imagem no formato inválido");
            }

            if (!await ValidateAsync(product))
                return false;

            return await _productRepository.UpdateAsync(product);
        }
        
        public async Task<bool> DeleteProductAsync(int id)
        {
            if (!await _productRepository.HaveObjectInDbAsync(p => p.Id == id))
                return _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));

            return await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductImageResponse> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return product.MapTo<Product, ProductImageResponse>();
        }

        public async Task<PageList<ProductResponse>> GetAllProductsWithPaginationAsync(PageParams pageParams)
        {
            var productPageList = await _productRepository.GetAllWithPaginationAsync(pageParams);

            return productPageList.MapTo<PageList<Product>, PageList<ProductResponse>>();
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var productList = await _productRepository.GetAllAsync();

            return productList.MapTo<List<Product>, List<ProductResponse>>();
        }

        public async Task<Product> GetByIdAsyncReturnsDomainObject(int id) =>
            await _productRepository.GetByIdAsync(id);
    }
}
