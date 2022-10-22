using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using OrdersTributoJustoTesteTecnico.Infra.Contexts;
using System.Net.Http.Json;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Business.Settings.NotificationSettings;

namespace IntegrationTests.BaseConsumers
{
    public abstract class HttpClientFixture
    {
        protected readonly HttpClient _httpClient;

        protected HttpClientFixture()
        {
            var root = new InMemoryDatabaseRoot();

            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<OrdersTributoJustoTesteTecnicoDbContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<OrdersTributoJustoTesteTecnicoDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("DbForTests", root);
                        });
                    });
                });

            _httpClient = appFactory.CreateClient();
        }

        protected async Task<bool> CreatePostAsync(string requestUri, HttpContent httpContent)
        {
            var httpResponseMessage = await _httpClient.PostAsync(requestUri, httpContent);

            return httpResponseMessage.IsSuccessStatusCode;
        }

        protected async Task<bool> CreatePutAsync(string requestUri, HttpContent httpContent)
        {
            var httpResponseMessage = await _httpClient.PutAsync(requestUri, httpContent);

            return httpResponseMessage.IsSuccessStatusCode;
        }

        protected async Task<bool> CreatePostAsJsonAsync<TSave>(string requestUri, TSave save) 
            where TSave : class
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(requestUri, save);

            return httpResponseMessage.IsSuccessStatusCode;
        }

        protected async Task<bool> CreatePutAsJsonAsync<TUpdate>(string requestUri, TUpdate update) 
            where TUpdate : class
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync(requestUri, update);
            var r = await httpResponseMessage.Content.ReadAsStringAsync();
            return httpResponseMessage.IsSuccessStatusCode;
        }

        protected async Task<bool> CreateDeleteAsync(string requestUri)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(requestUri);

            return httpResponseMessage.IsSuccessStatusCode;
        }

        protected async Task<TResponse> CreateGetAsync<TResponse>(string requestUri)
            where TResponse : class
        {
            var httpResponseMessage = await _httpClient.GetAsync(requestUri);

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        protected async Task<List<TResponse>> CreateGetAllAsync<TResponse>(string requestUri)
        {
            var httpResponseMessage = await _httpClient.GetAsync(requestUri);

            return await httpResponseMessage.Content.ReadFromJsonAsync<List<TResponse>>();
        }

        protected async Task<PageList<TResponse>> CreateGetAllWithPaginationAsync<TResponse>(string requestUri)
            where TResponse : class
        {
            var httpResponseMessage = await _httpClient.GetAsync(requestUri);

            return await httpResponseMessage.Content.ReadFromJsonAsync<PageList<TResponse>>();
        }
    }
}
