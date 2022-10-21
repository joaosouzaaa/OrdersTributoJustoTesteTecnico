using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders;
using TestsBuilders.Helpers;

namespace UnitTests.AutoMapperTests
{
    public sealed class ProductAutoMapperTests
    {
        public ProductAutoMapperTests()
        {
            AutoMapperConfigurations.Inicialize();
        }

        [Fact]
        public void ProductSaveRequest_To_Product()
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            var product = productSaveRequest.MapTo<ProductSaveRequest, Product>();

            Assert.Equal(productSaveRequest.Name, product.Name);
            Assert.Equal(productSaveRequest.Price, product.Price);
        }

        [Fact]
        public void ProductUpdateRequest_To_Product()
        {
            var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();
            var product = productUpdateRequest.MapTo<ProductUpdateRequest, Product>();

            Assert.Equal(productUpdateRequest.Id, product.Id);
            Assert.Equal(productUpdateRequest.Name, product.Name);
            Assert.Equal(productUpdateRequest.Price, product.Price);
        }

        [Fact]
        public void Product_To_ProductResponse()
        {
            var product = ProductBuilder.NewObject().DomainBuild();
            var productResponse = product.MapTo<Product, ProductResponse>();

            Assert.Equal(product.Id, productResponse.Id);
            Assert.Equal(product.Name, productResponse.Name);
            Assert.Equal(product.Price, productResponse.Price);
        }

        [Fact]
        public void ProductPageList_To_ProductResponsePageList()
        {
            var productList = new List<Product>()
            {
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild()
            };
            var productPageList = BuildersUtils.BuildPageList(productList);
            var productResponsePageList = productPageList.MapTo<PageList<Product>, PageList<ProductResponse>>();

            Assert.Equal(productResponsePageList.Result.Count, productPageList.Result.Count);
            Assert.Equal(productResponsePageList.TotalCount, productPageList.TotalCount);
            Assert.Equal(productResponsePageList.PageSize, productPageList.PageSize);
            Assert.Equal(productResponsePageList.CurrentPage, productPageList.CurrentPage);
            Assert.Equal(productResponsePageList.TotalPages, productPageList.TotalPages);
        }

        [Fact]
        public void Product_To_ProductImageResponse()
        {
            var product = ProductBuilder.NewObject().DomainBuild();
            var productImageResponse = product.MapTo<Product, ProductImageResponse>();

            Assert.Equal(productImageResponse.Id, product.Id);
            Assert.Equal(productImageResponse.Name, product.Name);
            Assert.Equal(productImageResponse.Price, product.Price);
            Assert.Equal(productImageResponse.Image, product.Image);
        }

        [Fact]
        public void ProductImageResponse_To_Product()
        {
            var productImageResponse = ProductBuilder.NewObject().ImageResponseBuild();
            var product = productImageResponse.MapTo<ProductImageResponse, Product>();

            Assert.Equal(product.Id, productImageResponse.Id);
            Assert.Equal(product.Name, productImageResponse.Name);
            Assert.Equal(product.Price, productImageResponse.Price);
            Assert.Equal(product.Image, productImageResponse.Image);
        }
    }
}
