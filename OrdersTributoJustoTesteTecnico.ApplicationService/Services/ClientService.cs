using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using OrdersTributoJustoTesteTecnico.ApplicationService.Services.BaseServices;
using OrdersTributoJustoTesteTecnico.Business.Extensions;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Validatation;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Domain.Enum;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Services
{
    public sealed class ClientService : BaseService<Client>, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository, INotificationHandler notification, IValidate<Client> validate) : base(notification, validate)
        {
            _clientRepository = clientRepository;
        }

        public async Task<bool> AddClientAsync(ClientSaveRequest clientSaveRequest)
        {
            var client = clientSaveRequest.MapTo<ClientSaveRequest, Client>();

            if (await _clientRepository.HaveObjectInDbAsync(c => c.Cpf == clientSaveRequest.Cpf))
                return _notification.AddDomainNotification("Cpf", "Cpf já existente");

            if (!await ValidateAsync(client))
                return false;

            return await _clientRepository.AddAsync(client);
        }

        public async Task<bool> UpdateClientAsync(ClientUpdateRequest clientUpdateRequest)
        {
            if (!await _clientRepository.HaveObjectInDbAsync(c => c.Id == clientUpdateRequest.Id))
                return _notification.AddDomainNotification("Product", EMessage.NotFound.Description().FormatTo("Product"));

            var client = clientUpdateRequest.MapTo<ClientUpdateRequest, Client>();

            if (!await ValidateAsync(client))
                return false;

            return await _clientRepository.UpdateAsync(client);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            if (!await _clientRepository.HaveObjectInDbAsync(c => c.Id == id))
                return _notification.AddDomainNotification("Client", EMessage.NotFound.Description().FormatTo("Client"));

            return await _clientRepository.DeleteAsync(id);
        }

        public async Task<ClientResponse> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            return client.MapTo<Client, ClientResponse>();
        }

        public async Task<bool> ClientIdExistAsync(int id) =>
            await _clientRepository.HaveObjectInDbAsync(c => c.Id == id);
    }
}
