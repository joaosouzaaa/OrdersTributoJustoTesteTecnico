using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces
{
    public interface IClientService
    {
        Task<bool> AddAsync(ClientSaveRequest clientSaveRequest);
        Task<bool> UpdateAsync(ClientUpdateRequest clientUpdateRequest);
        Task<bool> DeleteAsync(int id);
        Task<ClientResponse> GetClientByIdAsync(int id);
    }
}
