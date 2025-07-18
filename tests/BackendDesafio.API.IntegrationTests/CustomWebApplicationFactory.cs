using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.MongoDb;

namespace BackendDesafio.API.IntegrationTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MongoDbContainer _mongoContainer;

    public string ConnectionString => _mongoContainer.GetConnectionString();

    public CustomWebApplicationFactory()
    {
        _mongoContainer = new MongoDbBuilder()
            .WithUsername("admin")
            .WithPassword("admin")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            var config = new Dictionary<string, string?>
            {
                { "MongoDB:ConnectionString", ConnectionString }
            };
            configBuilder.AddInMemoryCollection(config!);
        });
    }

    public async Task InitializeAsync() => await _mongoContainer.StartAsync();

    public new async Task DisposeAsync() => await _mongoContainer.StopAsync();
}
