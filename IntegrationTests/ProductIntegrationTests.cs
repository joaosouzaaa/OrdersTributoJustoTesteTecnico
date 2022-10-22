using IntegrationTests.BaseConsumers;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using System.Net.Http.Headers;
using System.Text;
using TestsBuilders;

namespace IntegrationTests
{
    public sealed class ProductIntegrationTests : HttpClientFixture
    {
        [Fact]
        public async Task AddProductAsync_ReturnsSucces()
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();

            var saveResult = await PostProductAsync(productSaveRequest);

            Assert.True(saveResult);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnEntity()
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            var saveResult = await PostProductAsync(productSaveRequest);
            var requestUri = "api/Product/get-product-by-id?id=1";

            var getResult = await CreateGetAsync<ProductResponse>(requestUri);

            Assert.True(saveResult);
            Assert.NotNull(getResult);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsEntities()
        {
            var requestUri = "api/Product/get-all";
            var numberOfEntities = 3;
            var isSaveSuccess = await CreateProductsInRangeAsync(numberOfEntities);

            var getAllResult = await CreateGetAllAsync<ProductResponse>(requestUri);

            Assert.True(isSaveSuccess);
            Assert.Equal(getAllResult.Count, numberOfEntities);
        }

        [Fact]
        public async Task GetAllProductWithPaginationAsync_PaginationIsWorking_ReturnsEntities()
        {
            var numberOfEntities = 3;
            var requestUri = $"api/Product/get-all-paginated?PageSize={numberOfEntities - 1}&PageNumber=1";
            var isSaveSuccess = await CreateProductsInRangeAsync(numberOfEntities);

            var getAllWithPaginationResult = await CreateGetAllWithPaginationAsync<ProductResponse>(requestUri);

            Assert.True(isSaveSuccess);
            Assert.NotEqual(getAllWithPaginationResult.Result.Count, numberOfEntities);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsSuccess()
        {
            var productUpdateRequest = ProductBuilder.NewObject().WithId(1).UpdateRequestBuild();
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            var saveResult = await PostProductAsync(productSaveRequest);

            var updateResult = await PutProductAsync(productUpdateRequest);

            Assert.True(saveResult);
            Assert.True(updateResult);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsSuccess()
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            var saveResult = await PostProductAsync(productSaveRequest);
            var requestUri = "api/Product/delete-product?id=1";

            var deleteResult = await CreateDeleteAsync(requestUri);

            Assert.True(saveResult);
            Assert.True(deleteResult);
        }

        private MultipartFormDataContent CreateProductMultipartFormDataContent(string name, string price)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            var streamContent = new StreamContent(new MemoryStream(bytes));
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return new MultipartFormDataContent()
            {
                    { new StringContent(name), "Name"},
                    { streamContent, "ImageToSave", "image.jpg" },
                    { new StringContent(price), "Price"},
            };
        }

        private async Task<bool> PutProductAsync(ProductUpdateRequest productUpdateRequest)
        {
            var requestUri = "api/Product/update-product";

            var multipartFormDataContent = CreateProductMultipartFormDataContent(productUpdateRequest.Name, productUpdateRequest.Price.ToString());

            multipartFormDataContent.Add(new StringContent(productUpdateRequest.Id.ToString()), "Id");

            return await CreatePutAsync(requestUri, multipartFormDataContent);
        }

        private async Task<bool> PostProductAsync(ProductSaveRequest productSaveRequest)
        {
            var requestUri = "api/Product/create-product";

            var multipartFormDataContent = CreateProductMultipartFormDataContent(productSaveRequest.Name, productSaveRequest.Price.ToString());

            return await CreatePostAsync(requestUri, multipartFormDataContent);
        }

        private async Task<bool> CreateProductsInRangeAsync(int numberOfEntities)
        {
            var productSaveRequest = ProductBuilder.NewObject().SaveRequestBuild();
            var isSaveSuccess = true;
            for (var i = 0; i < numberOfEntities; i++)
                isSaveSuccess = await PostProductAsync(productSaveRequest);

            return isSaveSuccess;
        }
    }
}
