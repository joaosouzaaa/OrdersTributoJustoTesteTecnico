using Microsoft.EntityFrameworkCore;
using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
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
    public sealed class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;

        public OrderService(IOrderRepository orderRepository, IProductService productService,
                            IClientService clientService,
                            INotificationHandler notification, IValidate<Order> validate) 
                            : base(notification, validate)
        {
            _orderRepository = orderRepository;
            _clientService = clientService;
            _productService = productService;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            if (!await _orderRepository.HaveObjectInDbAsync(c => c.Id == id))
                return _notification.AddDomainNotification("Client", EMessage.NotFound.Description().FormatTo("Client"));

            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<bool> AddOrderAsync(OrderSaveRequest orderSaveRequest)
        {
            if(!await _clientService.ClientIdExistAsync(orderSaveRequest.ClientId))
                return _notification.AddDomainNotification("Client", "Client não existe.");

            if(!orderSaveRequest.Products.Any())
                return _notification.AddDomainNotification("Order", "Produtos devem ser inseridos");

            var order = await AddOrderEntitiesAsync(orderSaveRequest);

            if (order == null)
                return false;

            return await _orderRepository.AddAsync(order);
        }

        public async Task<bool> AddProductToOrderAsync(OrderUpdateRequest orderUpdateRequest)
        {
            var product = await _productService.GetByIdAsyncReturnsDomainObject(orderUpdateRequest.ProductId);

            if (product == null)
                return _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));

            var order = await _orderRepository.GetByIdAsync(orderUpdateRequest.OrderId, o => o.Include(o => o.Products), false);

            if (order == null)
                return _notification.AddDomainNotification("Order", EMessage.NotFound.Description().FormatTo("Order"));

            AddOrderPropertiesBasedOnProduct(order, product);

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> RemoveProductFromOrderAsync(OrderUpdateRequest orderUpdateRequest)
        {
            var order = await _orderRepository.GetByIdAsync(orderUpdateRequest.OrderId, o => o.Include(o => o.Products));

            if (order == null)
                return _notification.AddDomainNotification("Order", EMessage.NotFound.Description().FormatTo("Order"));
            
            var product = order.Products.FirstOrDefault(p => p.Id == orderUpdateRequest.ProductId);
            
            if (product == null)
                return _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));

            RemoveOrderPropertiesBasedOnProduct(order, product);

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<OrderResponse> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id, o => o.Include(o => o.Client).Include(o => o.Products));

            return order.MapTo<Order, OrderResponse>();
        }

        public async Task<List<OrderResponse>> GetAllOrdersAsync()
        {
            var ordersList = await _orderRepository.GetAllAsync(o => o.Include(o => o.Client).Include(o => o.Products));

            return ordersList.MapTo<List<Order>, List<OrderResponse>>();
        }

        public async Task<PageList<OrderResponse>> GetAllOrdersWithPaginationAsync(PageParams pageParams)
        {
            var ordersPageList = await _orderRepository.GetAllWithPaginationAsync(pageParams, o => o.Include(o => o.Client).Include(o => o.Products));

            return ordersPageList.MapTo<PageList<Order>, PageList<OrderResponse>>();
        }

        private async Task<Order> AddOrderEntitiesAsync(OrderSaveRequest orderSaveRequest)
        {
            var order = new Order()
            {
                ClientId = orderSaveRequest.ClientId,
                Products = new List<Product>(),
                Quantity = orderSaveRequest.Products.Count
            };

            foreach (var productId in orderSaveRequest.Products)
            {
                var product = await _productService.GetByIdAsyncReturnsDomainObject(productId);

                if(product == null)
                {
                    _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));
                    return null;
                }

                order.TotalPrice += product.Price;

                order.Products.Add(product);
            }

            return order;
        }

        private void AddOrderPropertiesBasedOnProduct(Order order, Product product)
        {
            if (order.Products != null)
            {
                order.Products.Add(product);
                order.Quantity -= 1;
                order.TotalPrice -= product.Price;
            }
        }

        private void RemoveOrderPropertiesBasedOnProduct(Order order, Product product)
        {
            if(order.Products != null)
            {
                order.Products.Remove(product);
                order.Quantity -= 1;
                order.TotalPrice -= product.Price;
            }
        }
    }
}
