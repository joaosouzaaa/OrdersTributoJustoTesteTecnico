using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;
using OrdersTributoJustoTesteTecnico.Business.Settings.NotificationSettings;
using OrdersTributoJustoTesteTecnico.Infra.Contexts;

namespace OrdersTributoJustoTesteTecnico.Ioc
{
    public static class OthersDependencyInjection
    {
        public static void AddOthersDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<OrdersTributoJustoTesteTecnicoDbContext>();

            services.AddDbContext<OrdersTributoJustoTesteTecnicoDbContext>(options =>
            {
                var mySqlConnectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString));
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<INotificationHandler, NotificationHandler>();

            AutoMapperConfigurations.Inicialize();
        }
    }
}
