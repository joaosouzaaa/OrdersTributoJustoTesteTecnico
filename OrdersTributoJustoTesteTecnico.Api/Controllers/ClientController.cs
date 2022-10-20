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

        [HttpGet("get_client")]
        public async Task<ClientResponse> GetClientByIdAsync([FromQuery] int id) =>
            await _clientService.GetClientByIdAsync(id);

        [HttpPost("create")]
        public async Task<bool> AddAsync([FromBody] ClientSaveRequest clientSaveRequest) =>
            await _clientService.AddAsync(clientSaveRequest);

        [HttpPut("update")]
        public async Task<bool> UpdateAsync([FromBody] ClientUpdateRequest clientUpdateRequest) =>
            await _clientService.UpdateAsync(clientUpdateRequest);

        [HttpDelete("delete")]
        public async Task<bool> DeleteAsync([FromQuery] int id) =>
            await _clientService.DeleteAsync(id);
    }
}
