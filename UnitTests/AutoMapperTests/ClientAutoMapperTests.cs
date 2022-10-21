using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders;

namespace UnitTests.AutoMapperTests
{
    public sealed class ClientAutoMapperTests
    {
        public ClientAutoMapperTests()
        {
            AutoMapperConfigurations.Inicialize();
        }

        [Fact]
        public void ClientSaveRequest_To_Client()
        {
            var clientSaveRequest = ClientBuilder.NewObject().SaveRequestBuild();
            var client = clientSaveRequest.MapTo<ClientSaveRequest, Client>();

            Assert.Equal(clientSaveRequest.FirstName, client.FirstName);
            Assert.Equal(clientSaveRequest.LastName, client.LastName);
            Assert.Equal(clientSaveRequest.Cpf, client.Cpf);
            Assert.Equal(clientSaveRequest.Age, client.Age);
        }

        [Fact]
        public void ClientUpdateRequest_To_Client()
        {
            var clientUpdateRequest = ClientBuilder.NewObject().UpdateRequestBuild();
            var client = clientUpdateRequest.MapTo<ClientUpdateRequest, Client>();

            Assert.Equal(clientUpdateRequest.Id, client.Id);
            Assert.Equal(clientUpdateRequest.FirstName, client.FirstName);
            Assert.Equal(clientUpdateRequest.LastName, client.LastName);
            Assert.Equal(clientUpdateRequest.Cpf, client.Cpf);
            Assert.Equal(clientUpdateRequest.Age, client.Age);
        }

        [Fact]
        public void Client_To_ClientResponse()
        {
            var client = ClientBuilder.NewObject().DomainBuild();
            var clientResponse = client.MapTo<Client, ClientResponse>();

            Assert.Equal(clientResponse.Id, client.Id);
            Assert.Equal(clientResponse.FirstName, client.FirstName);
            Assert.Equal(clientResponse.LastName, client.LastName);
            Assert.Equal(clientResponse.Cpf, client.Cpf);
            Assert.Equal(clientResponse.Age, client.Age);
        }
    }
}
