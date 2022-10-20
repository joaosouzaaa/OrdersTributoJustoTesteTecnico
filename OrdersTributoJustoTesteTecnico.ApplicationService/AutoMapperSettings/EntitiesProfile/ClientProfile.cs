using AutoMapper;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings.EntitiesProfile
{
    public sealed class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientSaveRequest, Client>();

            CreateMap<ClientUpdateRequest, Client>();

            CreateMap<Client, ClientResponse>();
        }
    }
}
