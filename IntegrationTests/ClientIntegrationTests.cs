using IntegrationTests.BaseConsumers;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;
using TestsBuilders;

namespace IntegrationTests
{
    public sealed class ClientIntegrationTests : HttpClientFixture
    {
        [Fact]
        public async Task AddClientAsync_ReturnsSuccess()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();

            var saveResult = await PostClientAsync(clientSaveRequest);

            Assert.True(saveResult);
        }

        [Fact]
        public async Task GetClientByIdAsync_ReturnsEntity()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            var saveResult = await PostClientAsync(clientSaveRequest);
            var requestUri = "api/Client/get-by-id?id=1";

            var getResult = await CreateGetAsync<ClientResponse>(requestUri);

            Assert.True(saveResult);
            Assert.NotNull(getResult);
        }

        [Fact]
        public async Task UpdateClientAsync_ReturnsSuccess()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            var saveResult = await PostClientAsync(clientSaveRequest);
            var clientUpdateRequest = ClientBuilder.NewObject().WithId(1).UpdateRequestBuild();
            var requestUri = "api/Client/update-client";

            var updateResult = await CreatePutAsJsonAsync(requestUri, clientUpdateRequest);

            Assert.True(saveResult);
            Assert.True(updateResult);
        }

        [Fact]
        public async Task DeleteClientAsync_ReturnsSuccess()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            var saveResult = await PostClientAsync(clientSaveRequest);
            var requestUri = "api/Client/delete-client?id=1";

            var deleteResult = await CreateDeleteAsync(requestUri);

            Assert.True(saveResult);
            Assert.True(deleteResult);
        }

        private async Task<bool> PostClientAsync(ClientSaveRequest clientSaveRequest)
        {
            var requestUri = "api/Client/create-client";

            return await CreatePostAsJsonAsync(requestUri, clientSaveRequest);
        }
    }
}
