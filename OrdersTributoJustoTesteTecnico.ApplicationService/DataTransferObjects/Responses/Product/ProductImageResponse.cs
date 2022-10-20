namespace OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product
{
    public sealed class ProductImageResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
    }
}
