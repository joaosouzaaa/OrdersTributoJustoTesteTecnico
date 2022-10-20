using OrdersTributoJustoTesteTecnico.Api.Filters;

namespace OrdersTributoJustoTesteTecnico.Api.DependencyInjection
{
    public static class FiltersDependencyInjection
    {
        public static void AddFiltersDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<NotificationFilter>();

            services.AddMvc(options =>
            {
                options.Filters.AddService<NotificationFilter>();
            });
        }
    }
}
