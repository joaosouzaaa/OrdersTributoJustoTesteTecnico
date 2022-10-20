using AutoMapper;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings.EntitiesProfile
{
    public sealed class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(or => or.ClientResponse, map => map.MapFrom(o => o.Client))
                .ForMember(or => or.ProductResponses, map => map.MapFrom(o => o.Products))
                .ForMember(or => or.ClientResponse, map => map.MapFrom(o => o.Client));

            CreateMap<PageList<Order>, PageList<OrderResponse>>();
        }
    }
}
