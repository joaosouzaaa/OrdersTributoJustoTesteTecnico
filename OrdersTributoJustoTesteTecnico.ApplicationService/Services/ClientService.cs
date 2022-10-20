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

        public async Task<bool> AddAsync(ClientSaveRequest clientSaveRequest)
        {
            var client = clientSaveRequest.MapTo<ClientSaveRequest, Client>();

            if (!await ValidateAsync(client))
                return false;

            return await _clientRepository.AddAsync(client);
        }

        public async Task<bool> UpdateAsync(ClientUpdateRequest clientUpdateRequest)
        {
            var client = clientUpdateRequest.MapTo<ClientUpdateRequest, Client>();

            if (!await ValidateAsync(client))
                return false;

            return await _clientRepository.UpdateAsync(client);
        }

        public async Task<bool> DeleteAsync(int id)
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
    }
}
