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

        public OrderService(IOrderRepository orderRepository, IProductService productService,
                            INotificationHandler notification, IValidate<Order> validate) 
                            : base(notification, validate)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _orderRepository.HaveObjectInDbAsync(c => c.Id == id))
                return _notification.AddDomainNotification("Client", EMessage.NotFound.Description().FormatTo("Client"));

            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<bool> AddOrderAsync(OrderSaveRequest orderSaveRequest)
        {
            if(!orderSaveRequest.Products.Any())
                return _notification.AddDomainNotification("Order", "Produtos devem ser inseridos");

            var order = await AddOrderEntitiesAsync(orderSaveRequest);

            if (order == null)
                return false;

            return await _orderRepository.AddAsync(order);
        }

        public async Task<bool> AddProductAsync(OrderUpdateRequest orderUpdateRequest)
        {
            var productResponse = await _productService.GetByIdAsync(orderUpdateRequest.ProductId);

            var product = productResponse.MapTo<ProductImageResponse, Product>();
            
            if (productResponse == null)
                return _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));

            var order = await _orderRepository.GetByIdAsync(orderUpdateRequest.OrderId);

            if (order == null)
                return _notification.AddDomainNotification("Order", EMessage.NotFound.Description().FormatTo("Order"));

            order.Products.Add(product);

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> RemoveProductAsync(OrderUpdateRequest orderUpdateRequest)
        {
            var productResponse = await _productService.GetByIdAsync(orderUpdateRequest.ProductId);

            var product = productResponse.MapTo<ProductImageResponse, Product>();

            if (productResponse == null)
                return _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));

            var order = await _orderRepository.GetByIdAsync(orderUpdateRequest.OrderId);

            if (order == null)
                return _notification.AddDomainNotification("Order", EMessage.NotFound.Description().FormatTo("Order"));

            order.Products.Remove(product);

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id, o => o.Include(o => o.Client).Include(o => o.Products));

            return order.MapTo<Order, OrderResponse>();
        }

        public async Task<List<OrderResponse>> GetAllAsync()
        {
            var ordersList = await _orderRepository.GetAllAsync(o => o.Include(o => o.Client).Include(o => o.Products));

            return ordersList.MapTo<List<Order>, List<OrderResponse>>();
        }

        public async Task<PageList<OrderResponse>> FindAllWithPaginationAsync(PageParams pageParams)
        {
            var ordersPageList = await _orderRepository.FindAllWithPaginationAsync(pageParams, o => o.Include(o => o.Client).Include(o => o.Products));

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
                var productResponse = await _productService.GetByIdAsync(productId);

                if(productResponse == null)
                {
                    _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));
                    return null;
                }

                var product = productResponse.MapTo<ProductImageResponse, Product>();

                order.TotalPrice += product.Price;

                order.Products.Add(product);
            }

            return order;
        }
    }
}
