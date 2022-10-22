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
        private List<int> _products = new List<int>()
        {
            GenerateRandomNumber(),
            GenerateRandomNumber(),
            GenerateRandomNumber()
        };
        private List<Product> _productsList = new List<Product>();
        private int _productId = GenerateRandomNumber();

        public Order DomainBuild() =>
            new Order()
            {
                ClientId = _clientId,
                Id = _id,
                Products = _productsList,
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
                Products = _products
            };

        public OrderUpdateRequest UpdateRequestBuild() =>
            new OrderUpdateRequest()
            {
                OrderId = _id,
                ProductId = _productId
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

        public OrderBuilder WithProducts(List<int>? products)
        {
            _products = products;

            return this;
        }

        public OrderBuilder WithProductsList(List<Product> productsList)
        {
            _productsList = productsList;

            return this;
        }

        public OrderBuilder WithProductId(int productId)
        {
            _productId = productId;

            return this;
        }

        public OrderBuilder WithId(int id)
        {
            _id = id;

            return this;
        }
    }
}
