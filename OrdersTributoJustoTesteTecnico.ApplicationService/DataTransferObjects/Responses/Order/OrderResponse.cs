using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order
{
    public sealed class OrderResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public ClientResponse ClientResponse{ get; set; }
        public List<ProductResponse> ProductResponses { get; set; }
    }
}
