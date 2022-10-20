using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrdersTributoJustoTesteTecnico.Ioc
{
    public static class OrdersTributoJustoTesteTecnicoDependencyInjection
    {
        public static void AddOrdersTributoJustoTesteTecnicoDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOthersDependencyInjection(configuration);
            services.AddValidateDependencyInjection();
            services.AddPaginationDependencyInjection();
            services.AddRepositoriesDependencyInjection();
            services.AddServicesDependencyInjection();
        }
    }
}
