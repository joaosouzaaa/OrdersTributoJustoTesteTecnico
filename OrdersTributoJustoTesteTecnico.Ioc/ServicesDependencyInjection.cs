using Microsoft.Extensions.DependencyInjection;
using OrdersTributoJustoTesteTecnico.ApplicationService.Interfaces;
using OrdersTributoJustoTesteTecnico.ApplicationService.Services;

namespace OrdersTributoJustoTesteTecnico.Ioc
{
    public static class ServicesDependencyInjection
    {
        public static void AddServicesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
