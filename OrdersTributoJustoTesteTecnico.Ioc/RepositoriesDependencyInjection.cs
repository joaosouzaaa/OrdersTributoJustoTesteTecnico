using Microsoft.Extensions.DependencyInjection;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Infra.Repositories;

namespace OrdersTributoJustoTesteTecnico.Ioc
{
    public static class RepositoriesDependencyInjection
    {
        public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
