namespace OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order
{
    public sealed class OrderUpdateRequest
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
