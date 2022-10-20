using Microsoft.Extensions.DependencyInjection;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Pagination;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Infra.Services;

namespace OrdersTributoJustoTesteTecnico.Ioc
{
    public static class PaginationDependencyInjection
    {
        public static void AddPaginationDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IPaginationService<Order>, PaginationService<Order>>();
            services.AddScoped<IPaginationService<Product>, PaginationService<Product>>();
        }
    }
}
