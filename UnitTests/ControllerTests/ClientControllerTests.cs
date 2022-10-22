using Moq;
using OrdersTributoJustoTesteTecnico.Api.Controllers;
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

            _service.Verify(s => s.GetClientByIdAsync(id), Times.Once());
            Assert.Equal(clientResponse, controllerResult);
        }

        [Fact]
        public async Task AddClientAsync_ReturnsTrue()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            _service.Setup(s => s.AddClientAsync(clientSaveRequest)).ReturnsAsync(true);
            
            var controllerResult = await _controller.AddClientAsync(clientSaveRequest);

            _service.Verify(s => s.AddClientAsync(clientSaveRequest), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task AddClientAsync_ReturnsFalse()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            _service.Setup(s => s.AddClientAsync(clientSaveRequest)).ReturnsAsync(false);

            var controllerResult = await _controller.AddClientAsync(clientSaveRequest);

            _service.Verify(s => s.AddClientAsync(clientSaveRequest), Times.Once());
            Assert.False(controllerResult);
        }

        [Fact]
        public async Task UpdateClientAsync_ReturnsTrue()
        {
            var clientUpdateRequest = ClientBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.UpdateClientAsync(clientUpdateRequest)).ReturnsAsync(true);

            var controllerResult = await _controller.UpdateClientAsync(clientUpdateRequest);

            _service.Verify(s => s.UpdateClientAsync(clientUpdateRequest), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task UpdateClientAsync_ReturnsFalse()
        {
            var clientUpdateRequest = ClientBuilder.NewObject().UpdateRequestBuild();
            _service.Setup(s => s.UpdateClientAsync(clientUpdateRequest)).ReturnsAsync(false);

            var controllerResult = await _controller.UpdateClientAsync(clientUpdateRequest);

            _service.Verify(s => s.UpdateClientAsync(clientUpdateRequest), Times.Once());
            Assert.False(controllerResult);
        }

        [Fact]
        public async Task DeleteClientAsync_ReturnsTrue()
        {
            var id = 1;
            _service.Setup(s => s.DeleteClientAsync(id)).ReturnsAsync(true);

            var controllerResult = await _controller.DeleteClientAsync(id);

            _service.Verify(s => s.DeleteClientAsync(id), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task DeleteClientAsync_ReturnsFalse()
        {
            var id = 1;
            _service.Setup(s => s.DeleteClientAsync(id)).ReturnsAsync(false);

            var controllerResult = await _controller.DeleteClientAsync(id);

            _service.Verify(s => s.DeleteClientAsync(id), Times.Once());
            Assert.False(controllerResult);
        }
    }
}
