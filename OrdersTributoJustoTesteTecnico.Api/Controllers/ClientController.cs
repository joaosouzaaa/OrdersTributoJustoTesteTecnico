using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;

namespace OrdersTributoJustoTesteTecnico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("get-by-id")]
        public async Task<ClientResponse> GetClientByIdAsync([FromQuery] int id) =>
            await _clientService.GetClientByIdAsync(id);

        [HttpPost("create-client")]
        public async Task<bool> AddClientAsync([FromBody] ClientSaveRequest clientSaveRequest) =>
            await _clientService.AddClientAsync(clientSaveRequest);

        [HttpPut("update-client")]
        public async Task<bool> UpdateClientAsync([FromBody] ClientUpdateRequest clientUpdateRequest) =>
            await _clientService.UpdateClientAsync(clientUpdateRequest);

        [HttpDelete("delete-client")]
        public async Task<bool> DeleteClientAsync([FromQuery] int id) =>
            await _clientService.DeleteClientAsync(id);
    }
}
