using IntegrationTests.BaseConsumers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Order;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
using System.Net.Http.Headers;
using System.Text;
using TestsBuilders;
using TestsBuilders.Helpers;

namespace IntegrationTests
{
    public sealed class OrderIntegrationTests : HttpClientFixture
    {
        [Fact]
        public async Task AddOrderAsync_ReturnsSuccess()
        {
            var postResult = await PostOrderAsync(numberOfEntities: 5);

            Assert.True(postResult);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsEntity()
        {
            var postResult = await PostOrderAsync(numberOfEntities: 2);
            var requestUri = "api/Order/get-by-id?id=1";

            var getResult = CreateGetAsync<OrderResponse>(requestUri);

            Assert.True(postResult);
            Assert.NotNull(getResult);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ReturnsEntities()
        {
            var numberOfEntities = 4;
            var postResult = await CreateOrdersInRangeAsync(numberOfEntities, 2);
            var requestUri = "api/Order/get-all";

            var getAllResult = await CreateGetAllAsync<OrderResponse>(requestUri);

            Assert.True(postResult);
            Assert.Equal(getAllResult.Count, numberOfEntities);
        }

        [Fact]
        public async Task GetAllOrdersWithPaginationAsync_PaginationIsWorking_ReturnsEntities()
        {
            var numberOfEntities = 2;
            var postResult = await CreateOrdersInRangeAsync(numberOfEntities, 3);
            var requestUri = $"api/Order/get-all-paginated?PageSize={numberOfEntities - 1}&PageNumber=1";

            var getAllWithPaginationResult = await CreateGetAllWithPaginationAsync<OrderResponse>(requestUri);

            Assert.True(postResult);
            Assert.NotEqual(getAllWithPaginationResult.Result.Count, numberOfEntities);
        }

        [Fact]
        public async Task AddProductToOrderAsync_ReturnsSuccess()
        {
            var postResult = await PostOrderAsync(1);
            var orderUpdateRequest = OrderBuilder.NewObject().WithProductId(1).WithId(1).UpdateRequestBuild();
            var requestUri = "api/Order/add-product";

            var updateResult = await CreatePutAsJsonAsync(requestUri, orderUpdateRequest);

            Assert.True(postResult);
            Assert.True(updateResult);
        }

        [Fact]
        public async Task RemoveProductFromOrderAsync_ReturnsSuccess()
        {
            var postResult = await PostOrderAsync(2);
            var orderUpdateRequest = OrderBuilder.NewObject().WithProductId(2).WithId(1).UpdateRequestBuild();
            var requestUri = "api/Order/remove-product";

            var updateResult = await CreatePutAsJsonAsync(requestUri, orderUpdateRequest);

            Assert.True(postResult);
            Assert.True(updateResult);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsSuccess()
        {
            var postResult = await PostOrderAsync(2);
            var requestUri = "api/Order/delete-order?id=1";

            var deleteResult = await CreateDeleteAsync(requestUri);

            Assert.True(postResult);
            Assert.True(deleteResult);
        }

        private async Task<bool> CreateOrdersInRangeAsync(int numberOfEntities, int numberOfOrderEntities)
        {
            var isSaveSuccess = true;
            for (var i = 0; i < numberOfEntities; i++)
                isSaveSuccess = await PostOrderAsync(numberOfOrderEntities);

            return isSaveSuccess;
        }

        private async Task<bool> PostOrderAsync(int numberOfEntities)
        {
            var cpf = BuildersUtils.GenerateRandomCpf();
            var clientSaveRequest = ClientBuilder.NewObject().WithCpf(cpf).SaveRequestBuild();
            var saveClientResult = await PostClientAsync(clientSaveRequest);
            var saveProductResult = await CreateProductsInRangeAsync(numberOfEntities);
            var productsIdList = new List<int>();
            for (var i = 1; i <= numberOfEntities; i++)
            {
                productsIdList.Add(i);
            }

            var orderSaveRequest = new OrderSaveRequest()
            {
                ClientId = 1,
                Products = productsIdList
            };
            var requestUri = "api/Order/create-order";

            var saveResult = await CreatePostAsJsonAsync(requestUri, orderSaveRequest);

            if (!saveClientResult || !saveProductResult || !saveResult)
                return false;

            return true;
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

        private async Task<bool> PostClientAsync(ClientSaveRequest clientSaveRequest)
        {
            var requestUri = "api/Client/create-client";

            return await CreatePostAsJsonAsync(requestUri, clientSaveRequest);
        }
    }
}
