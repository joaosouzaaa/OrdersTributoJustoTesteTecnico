using AutoMapper;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Product;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Product;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings.EntitiesProfile
{
    public sealed class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductSaveRequest, Product>();

            CreateMap<ProductUpdateRequest, Product>();

            CreateMap<Product, ProductResponse>();

            CreateMap<PageList<Product>, PageList<ProductResponse>>();

            CreateMap<Product, ProductImageResponse>();
        }
    }
}
