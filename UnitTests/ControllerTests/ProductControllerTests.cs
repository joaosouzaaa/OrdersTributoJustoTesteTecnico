using Moq;
using OrdersTributoJustoTesteTecnico.Api.Controllers;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using TestsBuilders;
using TestsBuilders.Helpers;

namespace UnitTests.ControllerTests
{
    public sealed class ProductControllerTests
    {
        Mock<IProductService> _service;
        ProductController _controller;

        public ProductControllerTests()
        {
            _service = new Mock<IProductService>();
            _controller = new ProductController(_service.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsEntity()
        {
            var id = 1;
            var productImageResponse = ProductBuilder.NewObject().ImageResponseBuild();
            _service.Setup(s => s.GetProductByIdAsync(id)).ReturnsAsync(productImageResponse);

            var controllerResult = await _controller.GetProductByIdAsync(id);

            _service.Verify(s => s.GetProductByIdAsync(id), Times.Once());
            Assert.Equal(controllerResult, productImageResponse);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsEntities()
        {
            var productResponseList = new List<ProductResponse>()
            {
                ProductBuilder.NewObject().ResponseBuild(),
                ProductBuilder.NewObject().ResponseBuild()
            };
            _service.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(productResponseList);

            var controllerResult = await _controller.GetAllProductsAsync();

            _service.Verify(s => s.GetAllProductsAsync(), Times.Once());
            Assert.Equal(controllerResult, productResponseList);
        }

        [Fact]
        public async Task GetAllProductsWithPaginationAsync_ReturnsEntities()
        {
            var pageParams = PageParamsBuilders.NewObject().DomainBuild();
            var productResponseList = new List<ProductResponse>()
            {
                ProductBuilder.NewObject().ResponseBuild(),
                ProductBuilder.NewObject().ResponseBuild(),
                ProductBuilder.NewObject().ResponseBuild(),
                ProductBuilder.NewObject().ResponseBuild(),
                ProductBuilder.NewObject().ResponseBuild()
            };
            var productResponsePageList = BuildersUtils.BuildPageList(productResponseList);
            _service.Setup(s => s.GetAllProductsWithPaginationAsync(pageParams)).ReturnsAsync(productResponsePageList);

            var controllerResult = await _controller.GetAllProductsWithPaginationAsync(pageParams);

            _service.Verify(s => s.GetAllProductsWithPaginationAsync(pageParams), Times.Once());
            Assert.Equal(controllerResult, productResponsePageList);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsTrue()
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            _service.Setup(s => s.AddProductAsync(productSaveRequest)).ReturnsAsync(true);

            var controllerResult = await _controller.AddProductAsync(productSaveRequest);

            _service.Verify(s => s.AddProductAsync(productSaveRequest), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsFalse()
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            _service.Setup(s => s.AddProductAsync(productSaveRequest)).ReturnsAsync(false);

            var controllerResult = await _controller.AddProductAsync(productSaveRequest);

            _service.Verify(s => s.AddProductAsync(productSaveRequest), Times.Once());
            Assert.False(controllerResult);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsTrue()
        {
            var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.UpdateProductAsync(productUpdateRequest)).ReturnsAsync(true);

            var controllerResult = await _controller.UpdateProductAsync(productUpdateRequest);

            _service.Verify(s => s.UpdateProductAsync(productUpdateRequest), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsFalse()
        {
            var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.UpdateProductAsync(productUpdateRequest)).ReturnsAsync(false);

            var controllerResult = await _controller.UpdateProductAsync(productUpdateRequest);

            _service.Verify(s => s.UpdateProductAsync(productUpdateRequest), Times.Once());
            Assert.False(controllerResult);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsTrue()
        {
            var id = 1;
            _service.Setup(s => s.DeleteProductAsync(id)).ReturnsAsync(true);

            var controllerResult = await _controller.DeleteProductAsync(id);

            _service.Verify(s => s.DeleteProductAsync(id), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsFalse()
        {
            var id = 1;
            _service.Setup(s => s.DeleteProductAsync(id)).ReturnsAsync(false);

            var controllerResult = await _controller.DeleteProductAsync(id);

            _service.Verify(s => s.DeleteProductAsync(id), Times.Once());
            Assert.False(controllerResult);
        }
    }
}
