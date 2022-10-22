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
        public async Task GetOrderByIdAsync_ReturnsEntity()
        {
            var id = 1;
            var orderResponse = OrderBuilder.NewObject().ResponseBuild();
            _service.Setup(s => s.GetOrderByIdAsync(id)).ReturnsAsync(orderResponse);

            var controllerResult = await _controller.GetOrderByIdAsync(id);

            _service.Verify(s => s.GetOrderByIdAsync(id), Times.Once());
            Assert.Equal(controllerResult, orderResponse);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ReturnsEntities()
        {
            var orderResponseList = new List<OrderResponse>()
            {
                OrderBuilder.NewObject().ResponseBuild(),
                OrderBuilder.NewObject().ResponseBuild()
            };
            _service.Setup(s => s.GetAllOrdersAsync()).ReturnsAsync(orderResponseList);

            var controllerResult = await _controller.GetAllOrdersAsync();

            _service.Verify(s => s.GetAllOrdersAsync(), Times.Once());
            Assert.Equal(controllerResult, orderResponseList);
        }

        [Fact]
        public async Task GetAllOrdersWithPaginationAsync_ReturnsEntities()
        {
            var pageParams = PageParamsBuilders.NewObject().DomainBuild();
            var orderResponseList = new List<OrderResponse>()
            {
                OrderBuilder.NewObject().ResponseBuild()
            };
            var orderResponsePageList = BuildersUtils.BuildPageList(orderResponseList);
            _service.Setup(s => s.GetAllOrdersWithPaginationAsync(pageParams)).ReturnsAsync(orderResponsePageList);

            var controllerResult = await _controller.GetAllOrdersWithPaginationAsync(pageParams);

            _service.Verify(s => s.GetAllOrdersWithPaginationAsync(pageParams), Times.Once());
            Assert.Equal(controllerResult, orderResponsePageList);
        }

        [Fact]
        public async Task AddOrderAsync_ReturnsTrue()
        {
            var orderSaveRequest = OrderBuilder.NewObject().SaveRequestBuild();

            _service.Setup(s => s.AddOrderAsync(orderSaveRequest)).ReturnsAsync(true);

            var controllerResult = await _controller.AddOrderAsync(orderSaveRequest);

            _service.Verify(s => s.AddOrderAsync(orderSaveRequest), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task AddOrderAsync_ReturnsFalse()
        {
            var orderSaveRequest = OrderBuilder.NewObject().SaveRequestBuild();

            _service.Setup(s => s.AddOrderAsync(orderSaveRequest)).ReturnsAsync(false);

            var controllerResult = await _controller.AddOrderAsync(orderSaveRequest);

            _service.Verify(s => s.AddOrderAsync(orderSaveRequest), Times.Once());
            Assert.False(controllerResult);
        }

        [Fact]
        public async Task AddProductToOrderAsync_ReturnsTrue()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.AddProductToOrderAsync(orderUpdateRequest)).ReturnsAsync(true);

            var controllerResult = await _controller.AddProductToOrderAsync(orderUpdateRequest);

            _service.Verify(s => s.AddProductToOrderAsync(orderUpdateRequest), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task AddProductToOrderAsync_ReturnsFalse()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.AddProductToOrderAsync(orderUpdateRequest)).ReturnsAsync(false);

            var controllerResult = await _controller.AddProductToOrderAsync(orderUpdateRequest);
            
            _service.Verify(s => s.AddProductToOrderAsync(orderUpdateRequest), Times.Once());
            Assert.False(controllerResult);
        }

        [Fact]
        public async Task RemoveProductFromOrderAsync_ReturnsTrue()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.RemoveProductFromOrderAsync(orderUpdateRequest)).ReturnsAsync(true);

            var controllerResult = await _controller.RemoveProductFromOrderAsync(orderUpdateRequest);

            _service.Verify(s => s.RemoveProductFromOrderAsync(orderUpdateRequest), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task RemoveProductFromOrderAsync_ReturnsFalse()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.RemoveProductFromOrderAsync(orderUpdateRequest)).ReturnsAsync(false);

            var controllerResult = await _controller.RemoveProductFromOrderAsync(orderUpdateRequest);

            _service.Verify(s => s.RemoveProductFromOrderAsync(orderUpdateRequest), Times.Once());
            Assert.False(controllerResult);
        }

        [Fact]
        public async Task DeleteOrderAsync_ReturnsTrue()
        {
            var id = 1;
            _service.Setup(s => s.DeleteOrderAsync(id)).ReturnsAsync(true);

            var controllerResult = await _controller.DeleteOrderAsync(id);

            _service.Verify(s => s.DeleteOrderAsync(id), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task DeleteOrderAsync_ReturnsFalse()
        {
            var id = 1;
            _service.Setup(s => s.DeleteOrderAsync(id)).ReturnsAsync(false);

            var controllerResult = await _controller.DeleteOrderAsync(id);

            _service.Verify(s => s.DeleteOrderAsync(id), Times.Once());
            Assert.False(controllerResult);
        }
    }
}
