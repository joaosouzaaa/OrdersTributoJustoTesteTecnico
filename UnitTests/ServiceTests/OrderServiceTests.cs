using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using OrdersTributoJustoTesteTecnico.ApplicationService.Services;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders;
using TestsBuilders.Helpers;

namespace UnitTests.ServiceTests
{
    public sealed class OrderServiceTests
    {
        Mock<IOrderRepository> _repositoryMock;
        Mock<IProductService> _productServiceMock;
        Mock<IClientService> _clientServiceMock;
        Mock<INotificationHandler> _notificationHandler;
        OrderValidation _validation;
        OrderService _orderService;

        public OrderServiceTests()
        {
            _repositoryMock = new Mock<IOrderRepository>();
            _productServiceMock = new Mock<IProductService>();
            _clientServiceMock = new Mock<IClientService>();
            _notificationHandler = new Mock<INotificationHandler>();
            _validation = new OrderValidation();
            _orderService = new OrderService(_repositoryMock.Object, _productServiceMock.Object,
                                             _clientServiceMock.Object,
                                             _notificationHandler.Object, _validation);

            AutoMapperConfigurations.Inicialize();
        }

        [Fact]
        public async Task DeleteOrderAsync_ReturnsSucces()
        {
            var id = 1;
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(o => o.Id == id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            var serviceResult = await _orderService.DeleteOrderAsync(id);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(o => o.Id == id), Times.Once());
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task DeleteOrderAsync_OrderDoesNoExist_ReturnsFalse()
        {
            var id = 1;
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(o => o.Id == id)).ReturnsAsync(false);

            var serviceResult = await _orderService.DeleteOrderAsync(id);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(o => o.Id == id), Times.Once());
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task AddOrderAsync_ReturnsSuccess()
        {
            var orderSaveRequest = OrderBuilder.NewObject().SaveRequestBuild();
            var product = ProductBuilder.NewObject().DomainBuild();
            _clientServiceMock.Setup(c => c.ClientIdExistAsync(orderSaveRequest.ClientId)).ReturnsAsync(true);
            foreach (var productId in orderSaveRequest.Products)
                _productServiceMock.Setup(p => p.GetByIdAsyncReturnsDomainObject(productId)).ReturnsAsync(product);
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>())).ReturnsAsync(true);

            var serviceResult = await _orderService.AddOrderAsync(orderSaveRequest);

            _clientServiceMock.Verify(c => c.ClientIdExistAsync(orderSaveRequest.ClientId), Times.Once());
            _productServiceMock.Verify(p => p.GetByIdAsyncReturnsDomainObject(It.IsAny<int>()), Times.Exactly(orderSaveRequest.Products.Count));
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny < Order>()), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task AddOrderAsync_ClientDoesNotExist_ReturnsFalse()
        {
            var orderSaveRequest = OrderBuilder.NewObject().SaveRequestBuild();
            _clientServiceMock.Setup(c => c.ClientIdExistAsync(orderSaveRequest.ClientId)).ReturnsAsync(false);

            var serviceResult = await _orderService.AddOrderAsync(orderSaveRequest);

            _clientServiceMock.Verify(c => c.ClientIdExistAsync(orderSaveRequest.ClientId), Times.Once());
            _productServiceMock.Verify(p => p.GetByIdAsyncReturnsDomainObject(It.IsAny<int>()), Times.Never());
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task AddOrderAsync_ProductsWereNotAdded_ReturnsFalse()
        {
            var orderSaveRequest = OrderBuilder.NewObject().WithProducts(new List<int>()).SaveRequestBuild();
            _clientServiceMock.Setup(c => c.ClientIdExistAsync(orderSaveRequest.ClientId)).ReturnsAsync(true);

            var serviceResult = await _orderService.AddOrderAsync(orderSaveRequest);

            _clientServiceMock.Verify(c => c.ClientIdExistAsync(orderSaveRequest.ClientId), Times.Once());
            _productServiceMock.Verify(p => p.GetByIdAsyncReturnsDomainObject(It.IsAny<int>()), Times.Never());
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task AddOrderAsync_ProductIdDoesNotExist_ReturnsFalse()
        {
            var orderSaveRequest = OrderBuilder.NewObject().SaveRequestBuild();
            _clientServiceMock.Setup(c => c.ClientIdExistAsync(orderSaveRequest.ClientId)).ReturnsAsync(true);
            _productServiceMock.Setup(p => p.GetByIdAsyncReturnsDomainObject(It.IsAny<int>()));

            var serviceResult = await _orderService.AddOrderAsync(orderSaveRequest);

            _clientServiceMock.Verify(c => c.ClientIdExistAsync(orderSaveRequest.ClientId), Times.Once());
            _productServiceMock.Verify(p => p.GetByIdAsyncReturnsDomainObject(It.IsAny<int>()), Times.Once());
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task AddProductToOrderAsync_ReturnsSuccess()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            var product = ProductBuilder.NewObject().DomainBuild();
            var order = OrderBuilder.NewObject().DomainBuild();
            _productServiceMock.Setup(p => p.GetByIdAsyncReturnsDomainObject(orderUpdateRequest.ProductId)).ReturnsAsync(product);
            _repositoryMock.Setup(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false)).ReturnsAsync(order);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Order>())).ReturnsAsync(true);

            var serviceResult = await _orderService.AddProductToOrderAsync(orderUpdateRequest);

            _productServiceMock.Verify(p => p.GetByIdAsyncReturnsDomainObject(orderUpdateRequest.ProductId), Times.Once());
            _repositoryMock.Verify(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task AddProductToOrderAsync_ProductDoesNotExist_ReturnsFalse()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            var order = OrderBuilder.NewObject().DomainBuild();
            _productServiceMock.Setup(p => p.GetByIdAsyncReturnsDomainObject(orderUpdateRequest.ProductId));

            var serviceResult = await _orderService.AddProductToOrderAsync(orderUpdateRequest);

            _productServiceMock.Verify(p => p.GetByIdAsyncReturnsDomainObject(orderUpdateRequest.ProductId), Times.Once());
            _repositoryMock.Verify(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false), Times.Never());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task AddProductToOrderAsync_OrderDoesNotExist_ReturnsFalse()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            var product = ProductBuilder.NewObject().DomainBuild();
            _productServiceMock.Setup(p => p.GetByIdAsyncReturnsDomainObject(orderUpdateRequest.ProductId)).ReturnsAsync(product);
            _repositoryMock.Setup(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false));

            var serviceResult = await _orderService.AddProductToOrderAsync(orderUpdateRequest);

            _productServiceMock.Verify(p => p.GetByIdAsyncReturnsDomainObject(orderUpdateRequest.ProductId), Times.Once());
            _repositoryMock.Verify(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task RemoveProductFromOrderAsync_ReturnsSuccess()
        {
            var productId = 1;
            var productsList = new List<Product>()
            {
                ProductBuilder.NewObject().WithId(productId).DomainBuild(),
                ProductBuilder.NewObject().DomainBuild()
            };
            var orderUpdateRequest = OrderBuilder.NewObject().WithProductId(productId).UpdateRequestBuild();
            var order = OrderBuilder.NewObject().WithProductsList(productsList).DomainBuild();
            _repositoryMock.Setup(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false)).ReturnsAsync(order);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Order>())).ReturnsAsync(true);

            var serviceResult = await _orderService.RemoveProductFromOrderAsync(orderUpdateRequest);

            _repositoryMock.Verify(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task RemoveProductFromOrderAsync_OrderDoesNotExist_ReturnsFalse()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            _repositoryMock.Setup(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false));

            var serviceResult = await _orderService.RemoveProductFromOrderAsync(orderUpdateRequest);

            _repositoryMock.Verify(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task RemoveProductFromOrderAsync_ProductDoesExist_ReturnsFalse()
        {
            var orderUpdateRequest = OrderBuilder.NewObject().UpdateRequestBuild();
            var order = OrderBuilder.NewObject().WithProductsList(new List<Product>()).DomainBuild();
            _repositoryMock.Setup(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false)).ReturnsAsync(order);

            var serviceResult = await _orderService.RemoveProductFromOrderAsync(orderUpdateRequest);

            _repositoryMock.Verify(r => r.GetByIdAsync(orderUpdateRequest.OrderId, BuildersUtils.BuildIncluadableMock<Order>(), false), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnEntity()
        {
            var id = 1;
            var order = OrderBuilder.NewObject().DomainBuild();
            _repositoryMock.Setup(r => r.GetByIdAsync(id, BuildersUtils.BuildIncluadableMock<Order>(), false)).ReturnsAsync(order);

            var serviceResult = await _orderService.GetOrderByIdAsync(id);

            _repositoryMock.Verify(r => r.GetByIdAsync(id, BuildersUtils.BuildIncluadableMock<Order>(), false), Times.Once());
            Assert.NotNull(serviceResult);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ReturnsEntities()
        {
            var orderList = new List<Order>()
            {
                OrderBuilder.NewObject().DomainBuild(),
                OrderBuilder.NewObject().DomainBuild()
            };
            _repositoryMock.Setup(r => r.GetAllAsync(BuildersUtils.BuildIncluadableMock<Order>())).ReturnsAsync(orderList);

            var serviceResult = await _orderService.GetAllOrdersAsync();

            _repositoryMock.Verify(r => r.GetAllAsync(BuildersUtils.BuildIncluadableMock<Order>()), Times.Once());
            Assert.Equal(serviceResult.Count, orderList.Count);
        }

        [Fact]
        public async Task GetAllOrdersWithPaginationAsync_ReturnsEntities()
        {
            var pageParams = PageParamsBuilders.NewObject().DomainBuild();
            var orderList = new List<Order>();
            var orderPageList = BuildersUtils.BuildPageList(orderList);
            _repositoryMock.Setup(r => r.GetAllWithPaginationAsync(pageParams, BuildersUtils.BuildIncluadableMock<Order>())).ReturnsAsync(orderPageList);

            var serviceResult = await _orderService.GetAllOrdersWithPaginationAsync(pageParams);

            Assert.Equal(serviceResult.Result.Count, orderPageList.Result.Count);
        }
    }
}
