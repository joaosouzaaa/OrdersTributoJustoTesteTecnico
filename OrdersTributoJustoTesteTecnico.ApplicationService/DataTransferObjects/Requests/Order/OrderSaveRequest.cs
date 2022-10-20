namespace OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order
{
    public sealed class OrderSaveRequest
    {
        public int ClientId { get; set; }
        public List<int> Products { get; set; }
    }
}
