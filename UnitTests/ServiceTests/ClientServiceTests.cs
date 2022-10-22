using Moq;
using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.Services;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Validatation;
using OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using System.Linq.Expressions;
using TestsBuilders;

namespace UnitTests.ServiceTests
{
    public sealed class ClientServiceTests
    {
        Mock<IClientRepository> _repositoryMock;
        Mock<INotificationHandler> _notificationMock;
        ClientValidation _validate;
        ClientService _service;

        public ClientServiceTests()
        {
            _repositoryMock = new Mock<IClientRepository>();
            _notificationMock = new Mock<INotificationHandler>();
            _validate = new ClientValidation();
            _service = new ClientService(_repositoryMock.Object, _notificationMock.Object, _validate);

            AutoMapperConfigurations.Inicialize();
        }

        [Fact]
        public async Task AddClientAsync_ReturnsSuccess()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Cpf == clientSaveRequest.Cpf)).ReturnsAsync(false);
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Client>())).ReturnsAsync(true);

            var serviceResult = await _service.AddClientAsync(clientSaveRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Cpf == clientSaveRequest.Cpf), Times.Once());
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Client>()), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task AddClientAsync_CpfAlreadyExists_ReturnsFalse()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Cpf == clientSaveRequest.Cpf)).ReturnsAsync(true);

            var serviceResult = await _service.AddClientAsync(clientSaveRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Cpf == clientSaveRequest.Cpf), Times.Once());
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Client>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task AddClientAsync_ClientInvalid_ReturnFalse()
        {
            var clientSaveRequest = ClientBuilder.NewObject().WithCpf("aa").SaveRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Cpf == clientSaveRequest.Cpf)).ReturnsAsync(false);

            var serviceResult = await _service.AddClientAsync(clientSaveRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Cpf == clientSaveRequest.Cpf), Times.Once());
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Client>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task UpdateClientAsync_ReturnsSuccess()
        {
            var clientUpdateRequest = ClientBuilder.NewObject().UpdateRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Id == clientUpdateRequest.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Client>())).ReturnsAsync(true);

            var serviceResult = await _service.UpdateClientAsync(clientUpdateRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Id == clientUpdateRequest.Id), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Client>()), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task UpdateClientAsync_ClientDoesNotExist_ReturnsFalse()
        {
            var clientUpdateRequest = ClientBuilder.NewObject().UpdateRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Id == clientUpdateRequest.Id)).ReturnsAsync(false);

            var serviceResult = await _service.UpdateClientAsync(clientUpdateRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Id == clientUpdateRequest.Id), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Client>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task UpdateClientAsync_ClientInvalid_ReturnsFalse()
        {
            var clientUpdateRequest = ClientBuilder.NewObject().WithAge(0).UpdateRequestBuild();
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Id == clientUpdateRequest.Id)).ReturnsAsync(true);

            var serviceResult = await _service.UpdateClientAsync(clientUpdateRequest);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Id == clientUpdateRequest.Id), Times.Once());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Client>()), Times.Never());
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task DeleteClientAsync_ReturnsSucces()
        {
            var id = 1;
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Id == id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            var controllerResult = await _service.DeleteClientAsync(id);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Id == id), Times.Once());
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once());
            Assert.True(controllerResult);
        }

        [Fact]
        public async Task DeleteClientAsync_ClientDoesNotExist_ReturnsFalse()
        {
            var id = 1;
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Id == id)).ReturnsAsync(false);

            var controllerResult = await _service.DeleteClientAsync(id);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Id == id), Times.Once());
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Never());
            Assert.False(controllerResult);
        }

        [Fact]
        public async Task GetClientByIdAsync_ReturnsEntity()
        {
            var id = 1;
            var client = ClientBuilder.NewObject().DomainBuild();
            _repositoryMock.Setup(r => r.GetClientByIdAsync(id)).ReturnsAsync(client);

            var clientResponse = await _service.GetClientByIdAsync(id);

            _repositoryMock.Verify(r => r.GetClientByIdAsync(id), Times.Once());
            Assert.NotNull(clientResponse);
        }

        [Fact]
        public async Task ClientIdExistAsync_ReturnTrue()
        {
            var id = 1;
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Id == id)).ReturnsAsync(true);

            var serviceResult = await _service.ClientIdExistAsync(id);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Id == id), Times.Once());
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task ClientIdExistAsync_ReturnFalse()
        {
            var id = 1;
            _repositoryMock.Setup(r => r.HaveObjectInDbAsync(c => c.Id == id)).ReturnsAsync(false);

            var serviceResult = await _service.ClientIdExistAsync(id);

            _repositoryMock.Verify(r => r.HaveObjectInDbAsync(c => c.Id == id), Times.Once());
            Assert.False(serviceResult);
        }
    }
}
