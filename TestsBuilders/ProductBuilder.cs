using Microsoft.AspNetCore.Http;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders.BaseBuilders;
using TestsBuilders.Helpers;

namespace TestsBuilders
{
    public class ProductBuilder : BaseBuilder
    {
        public static ProductBuilder NewObject() => new ProductBuilder();

        private byte[] _image = { 0x32, 0x00, 0x1E, 0x00 };
        private string _name = GenerateRandomWord();
        private decimal _price = GenerateRandomDecimal();
        private IFormFile _imageToSave = BuildersUtils.BuildIFormFile();

        public Product DomainBuild() =>
            new Product()
            {
                Id = _id,
                Image = _image,
                Name = _name,
                Price = _price,
                Order = new List<Order>()
            };

        public ProductResponse ResponseBuild() =>
            new ProductResponse()
            {
                Id = _id,
                Name = _name,
                Price = _price
            };

        public ProductSaveRequest SaveRequestBuild() =>
            new ProductSaveRequest()
            {
                ImageToSave = _imageToSave,
                Name = _name,
                Price = _price
            };

        public ProductUpdateRequest UpdateRequestBuild() =>
            new ProductUpdateRequest()
            {
                Id = _id,
                ImageToSave = _imageToSave,
                Name = _name,
                Price = _price
            };

        public ProductImageResponse ImageResponseBuild() =>
            new ProductImageResponse
            {
                Id = _id,
                Image = _image,
                Name = _name,
                Price = _price
            };

        public ProductBuilder WithName(string name)
        {
            _name = name;

            return this;
        }

        public ProductBuilder WithPrice(decimal price)
        {
            _price = price;

            return this;
        }

        public ProductBuilder WithImageToSave(IFormFile imageToSave)
        {
            _imageToSave = imageToSave;

            return this;
        }

        public ProductBuilder WithId(int id)
        {
            _id = id;

            return this;
        }
    }
}
