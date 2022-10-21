using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders.BaseBuilders;

namespace TestsBuilders
{
    public class OrderBuilder : BaseBuilder
    {
        public static OrderBuilder NewObject() => new OrderBuilder();

        private int _clientId = GenerateRandomNumber();
        private int _quantity = GenerateRandomNumber();
        private decimal _totalPrice = GenerateRandomDecimal();

        public Order DomainBuild() =>
            new Order()
            {
                ClientId = _clientId,
                Id = _id,
                Products = new List<Product>(),
                Quantity = _quantity,
                TotalPrice = _totalPrice
            };

        public OrderResponse ResponseBuild() =>
            new OrderResponse()
            {
                Id = _id,
                ClientResponse = ClientBuilder.NewObject().ResponseBuild(),
                ProductResponses = new List<ProductResponse>(),
                Quantity = _quantity,
                TotalPrice = _totalPrice
            };

        public OrderSaveRequest SaveRequestBuild() =>
            new OrderSaveRequest()
            {
                ClientId = _clientId,
                Products = new List<int>()
                {
                    GenerateRandomNumber(),
                    GenerateRandomNumber(),
                    GenerateRandomNumber()
                }
            };

        public OrderUpdateRequest UpdateRequestBuild() =>
            new OrderUpdateRequest()
            {
                OrderId = _id,
                ProductId = GenerateRandomNumber()
            };

        public OrderBuilder WithQuantity(int quantity)
        {
            _quantity = quantity;

            return this;
        }

        public OrderBuilder WithTotalPrice(decimal totalPrice)
        {
            _totalPrice = totalPrice;

            return this;
        }
    }
}
