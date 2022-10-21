using Moq;
using OrdersTributoJustoTesteTecnico.Api.Controllers;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using TestsBuilders;
using TestsBuilders.Helpers;

namespace UnitTests.ControllerTests
{
    public sealed class OrderControllerTests
    {
        Mock<IOrderService> _service;
        OrderController _controller;

        public OrderControllerTests()
        {
            _service = new Mock<IOrderService>();
            _controller = new OrderController(_service.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEntity()
        {
            var id = 1;
            var orderResponse = OrderBuilder.NewObject().ResponseBuild();
            _service.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(orderResponse);

            var controllerResult = await _controller.GetByIdAsync(id);

            Assert.Equal(controllerResult, orderResponse);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEntities()
        {
            var orderResponseList = new List<OrderResponse>()
            {
                OrderBuilder.NewObject().ResponseBuild(),
                OrderBuilder.NewObject().ResponseBuild()
            };
            _service.Setup(s => s.GetAllAsync()).ReturnsAsync(orderResponseList);

            var controllerResult = await _controller.GetAllAsync();

            Assert.Equal(controllerResult, orderResponseList);
        }

        [Fact]
        public async Task GetAllWithPaginationAsync_ReturnsEntities()
        {
            var pageParams = PageParamsBuilders.NewObject().DomainBuild();
            var orderResponseList = new List<OrderResponse>()
            {
                OrderBuilder.NewObject().ResponseBuild()
            };
            var orderResponsePageList = BuildersUtils.BuildPageList(orderResponseList);
            _service.Setup(s => s.GetAllWithPaginationAsync(pageParams)).ReturnsAsync(orderResponsePageList);

            var controllerResult = await _controller.GetAllWithPaginationAsync(pageParams);

            Assert.Equal(controllerResult, orderResponsePageList);
        }

        [Fact]
        public async Task AddAsync_ReturnsTrue()
        {
            var controllerResult = await AddMockedOrderAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task AddAsync_ReturnsFalse()
        {
            var controllerResult = await AddMockedOrderAsync(false);

            Assert.False(controllerResult);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsTrue()
        {
            var controllerResult = await AddProductMockedAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsFalse()
        {
            var controllerResult = await AddProductMockedAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task RemoveProductAsync_ReturnsTrue()
        {
            var controllerResult = await RemoveProductMockedAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task RemoveProductAsync_ReturnsFalse()
        {
            var controllerResult = await RemoveProductMockedAsync(false);

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

        private async Task<bool> AddMockedOrderAsync(bool addReturn)
        {
            var orderSaveRequest = OrderBuilder.NewObject().SaveRequestBuild();

            _service.Setup(s => s.AddOrderAsync(orderSaveRequest)).ReturnsAsync(addReturn);

            return await _controller.AddOrderAsync(orderSaveRequest);
        }

        private async Task<bool> AddProductMockedAsync(bool updateReturn)
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.AddProductAsync(orderUpdateRequest)).ReturnsAsync(updateReturn);

            return await _controller.AddProductAsync(orderUpdateRequest);
        }

        private async Task<bool> RemoveProductMockedAsync(bool updateReturn)
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.RemoveProductAsync(orderUpdateRequest)).ReturnsAsync(updateReturn);

            return await _controller.RemoveProductAsync(orderUpdateRequest);
        }

        private async Task<bool> DeleteAsyncMockedAsync(bool deleteReturn)
        {
            var id = 1;
            _service.Setup(s => s.DeleteAsync(id)).ReturnsAsync(deleteReturn);

            return await _controller.DeleteAsync(id);
        }
    }
}
