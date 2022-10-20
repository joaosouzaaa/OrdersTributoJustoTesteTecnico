using Microsoft.Extensions.DependencyInjection;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Validatation;
using OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.Ioc
{
    public static class ValidateDependencyInjection
    {
        public static void AddValidateDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IValidate<Client>, ClientValidation>();
            services.AddScoped<IValidate<Product>, ProductValidation>();
            services.AddScoped<IValidate<Order>, OrderValidation>();
        }
    }
}
