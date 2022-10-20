using Microsoft.AspNetCore.Http;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product
{
    public sealed class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile? ImageToSave { get; set; }
    }
}
