using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces
{
    public interface IClientService
    {
        Task<bool> AddClientAsync(ClientSaveRequest clientSaveRequest);
        Task<bool> UpdateClientAsync(ClientUpdateRequest clientUpdateRequest);
        Task<bool> DeleteClientAsync(int id);
        Task<ClientResponse> GetClientByIdAsync(int id);
        Task<bool> ClientIdExistAsync(int id);
    }
}
