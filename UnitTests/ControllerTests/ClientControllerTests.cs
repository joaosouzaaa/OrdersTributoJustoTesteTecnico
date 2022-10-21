using Moq;
using OrdersTributoJustoTesteTecnico.Api.Controllers;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using TestsBuilders;

namespace UnitTests.ControllerTests
{
    public sealed class ClientControllerTests
    {
        Mock<IClientService> _service;
        ClientController _controller;

        public ClientControllerTests()
        {
            _service = new Mock<IClientService>();
            _controller = new ClientController(_service.Object);
        }

        [Fact]
        public async Task GetClientByIdAsync_ReturnsEntity()
        {
            var id = 1;
            var clientResponse = ClientBuilder.NewObject().ResponseBuild();
            _service.Setup(s => s.GetClientByIdAsync(id)).ReturnsAsync(clientResponse);

            var controllerResult = await _controller.GetClientByIdAsync(id);

            Assert.Equal(clientResponse, controllerResult);
        }

        [Fact]
        public async Task AddAsync_ReturnsTrue()
        {
            var controllerResult = await AddMockedClientAsync(addReturn: true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task AddAsync_ReturnsFalse()
        {
            var controllerResult = await AddMockedClientAsync(addReturn: false);

            Assert.False(controllerResult);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue()
        {
            var controllerResult = await UpdateMockedClientAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse()
        {
            var controllerResult = await UpdateMockedClientAsync(false);

            Assert.False(controllerResult);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue()
        {
            var controllerResult = await DeleteMockedClientAsync(true);

            Assert.True(controllerResult);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse()
        {
            var controllerResult = await DeleteMockedClientAsync(false);

            Assert.False(controllerResult);
        }

        private async Task<bool> DeleteMockedClientAsync(bool deleteReturn)
        {
            var id = 1;
            _service.Setup(s => s.DeleteAsync(id)).ReturnsAsync(deleteReturn);

            return await _controller.DeleteAsync(id);
        }

        private async Task<bool> UpdateMockedClientAsync(bool updateReturn)
        {
            var clientUpdateRequest = ClientBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.UpdateAsync(clientUpdateRequest)).ReturnsAsync(updateReturn);

            return await _controller.UpdateAsync(clientUpdateRequest);
        }

        private async Task<bool> AddMockedClientAsync(bool addReturn)
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            _service.Setup(s => s.AddAsync(clientSaveRequest)).ReturnsAsync(addReturn);

            return await _controller.AddAsync(clientSaveRequest);
        }
    }
}
