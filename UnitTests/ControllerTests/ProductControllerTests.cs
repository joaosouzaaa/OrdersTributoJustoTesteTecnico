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
        public async Task GetByIdAsync_ReturnsEntity()
        {
            var id = 1;
            var productImageResponse = ProductBuilder.NewObject().ImageResponseBuild();
            _service.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(productImageResponse);

            var controllerResult = await _controller.GetByIdAsync(id);

            Assert.Equal(controllerResult, productImageResponse);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEntities()
        {
            var productResponseList = new List<ProductResponse>()
            {
                ProductBuilder.NewObject().ResponseBuild(),
                ProductBuilder.NewObject().ResponseBuild()
            };
            _service.Setup(s => s.GetAllAsync()).ReturnsAsync(productResponseList);

            var controllerResult = await _controller.GetAllAsync();

            Assert.Equal(controllerResult, productResponseList);
        }

        [Fact]
        public async Task GetAllWithPagination_ReturnsEntities()
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
            _service.Setup(s => s.GetAllWithPaginationAsync(pageParams)).ReturnsAsync(productResponsePageList);

            var controllerResult = await _controller.GetAllWithPaginationAsync(pageParams);

            Assert.Equal(controllerResult, productResponsePageList);
        }

        [Fact]
        public async Task AddAsync_ReturnsTrue()
        {
            var controllerResult = await AddAsyncMockedAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task AddAsync_ReturnsFalse()
        {
            var controllerResult = await AddAsyncMockedAsync(false);

            Assert.False(controllerResult);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue()
        {
            var controllerResult = await UpdateAsyncMockedAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse()
        {
            var controllerResult = await UpdateAsyncMockedAsync(false);

            Assert.False(controllerResult);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue()
        {
            var controllerResult = await DeleteAsyncMockedAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse()
        {
            var controllerResult = await DeleteAsyncMockedAsync(false);

            Assert.False(controllerResult);
        }

        private async Task<bool> AddAsyncMockedAsync(bool addReturn)
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            _service.Setup(s => s.AddAsync(productSaveRequest)).ReturnsAsync(addReturn);

            return await _controller.AddAsync(productSaveRequest);
        }

        private async Task<bool> UpdateAsyncMockedAsync(bool updateReturn)
        {
            var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.UpdateAsync(productUpdateRequest)).ReturnsAsync(updateReturn);

            return await _controller.UpdateAsync(productUpdateRequest);
        }

        private async Task<bool> DeleteAsyncMockedAsync(bool deleteReturn)
        {
            var id = 1;
            _service.Setup(s => s.DeleteAsync(id)).ReturnsAsync(deleteReturn);

            return await _controller.DeleteAsync(id);
        }
    }
}
