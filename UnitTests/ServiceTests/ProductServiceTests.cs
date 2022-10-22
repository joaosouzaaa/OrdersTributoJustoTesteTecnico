using Moq;
using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.Services;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders;
using TestsBuilders.Helpers;

namespace UnitTests.ServiceTests
{
    public sealed class ProductServiceTests
    {
        Mock<IProductRepository> _repositoryMock;
        Mock<INotificationHandler> _notificationMock;
        ProductValidation _validation;
        ProductService _service;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _notificationMock = new Mock<INotificationHandler>();
            _validation = new ProductValidation();
            _service = new ProductService(_repositoryMock.Object, _notificationMock.Object, _validation);

            AutoMapperConfigurations.Inicialize();
        }

        [Fact]
        public async Task AddProductAsync_ReturnsSuccess()
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>())).ReturnsAsync(true);

            var serviceResult = await _service.AddProductAsync(productSaveRequest);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task AddProductAsync_FileExtensionInvalid_ReturnsFalse()
        {
            var invalidIFormFile = BuildersUtils.BuildIFormFile(".xls");
            var productSaveRequest = ProductBuilder.NewObject().WithImageToSave(invalidIFormFile).SaveRequestBuild();

            var serviceResult = await _service.AddProductAsync(productSaveRequest);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task AddProductAsync_ProductInvalid_ReturnsFalse()
        {
            var productSaveRequest = ProductBuilder.NewObject().WithPrice(-20).SaveRequestBuild();

            var serviceResult = await _service.AddProductAsync(productSaveRequest);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsSuccess()
        {
            var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(true);

            var serviceResult = await _service.UpdateProductAsync(productUpdateRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task UpdateProductAsync_ProductDoesNotExist_ReturnsFalse()
        {
            var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id)).ReturnsAsync(false);

            var serviceResult = await _service.UpdateProductAsync(productUpdateRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task UpdateProductAsync_FileExtensionInvalid_ReturnsFalse()
        {
            var invalidIFormFile = BuildersUtils.BuildIFormFile(".xls");
            var productUpdateRequest = ProductBuilder.NewObject().WithImageToSave(invalidIFormFile).UpdateRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id)).ReturnsAsync(true);

            var serviceResult = await _service.UpdateProductAsync(productUpdateRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task UpdateProductAsync_ProductInvalid_ReturnsFalse()
        {
            var productUpdateRequest = ProductBuilder.NewObject().WithName("a").UpdateRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id)).ReturnsAsync(true);

            var serviceResult = await _service.UpdateProductAsync(productUpdateRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(p => p.Id == productUpdateRequest.Id), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsSuccess()
        {
            var id = 1;
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(p => p.Id == id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            var serviceResult = await _service.DeleteProductAsync(id);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(p => p.Id == id), Times.Once());
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task DeleteProductAsync_ProductDoesExist_ReturnsFalse()
        {
            var id = 1;
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(p => p.Id == id)).ReturnsAsync(false);

            var serviceResult = await _service.DeleteProductAsync(id);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(p => p.Id == id), Times.Once());
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsEntity()
        {
            var id = 1;
            var product = ProductBuilder.NewObject().DomainBuild();
            _repositoryMock.Setup(r => r.GetByIdAsync(id, null, false)).ReturnsAsync(product);

            var serviceResult = await _service.GetProductByIdAsync(id);

            _repositoryMock.Verify(r => r.GetByIdAsync(id, null, false), Times.Once());
            Assert.NotNull(serviceResult);
        }

        [Fact]
        public async Task GetAllProductsWithPaginationAsync_ReturnsEntities()
        {
            var pageParams = PageParamsBuilders.NewObject().DomainBuild();
            var productsList = new List<Product>()
            {
                ProductBuilder.NewObject().DomainBuild()
            };
            var productsPageList = BuildersUtils.BuildPageList(productsList);
            _repositoryMock.Setup(r => r.GetAllWithPaginationAsync(pageParams, null)).ReturnsAsync(productsPageList);

            var serviceResult = await _service.GetAllProductsWithPaginationAsync(pageParams);

            _repositoryMock.Verify(r => r.GetAllWithPaginationAsync(pageParams, null), Times.Once());
            Assert.Equal(serviceResult.Result.Count, productsPageList.Result.Count);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsEntities()
        {
            var productsList = new List<Product>()
            {
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild()
            };
            _repositoryMock.Setup(r => r.GetAllAsync(null)).ReturnsAsync(productsList);

            var serviceResult = await _service.GetAllProductsAsync();

            _repositoryMock.Verify(r => r.GetAllAsync(null), Times.Once());
            Assert.Equal(serviceResult.Count, productsList.Count);
        }

        [Fact]
        public async Task GetByIdAsyncReturnsDomainObject_ReturnsEntity()
        {
            var id = 1;
            var product = ProductBuilder.NewObject().DomainBuild();
            _repositoryMock.Setup(r => r.GetByIdAsync(id, null, false)).ReturnsAsync(product);

            var serviceResult = await _service.GetByIdAsyncReturnsDomainObject(id);

            _repositoryMock.Verify(r => r.GetByIdAsync(id, null, false), Times.Once());
            Assert.NotNull(serviceResult);
        }
    }
}
