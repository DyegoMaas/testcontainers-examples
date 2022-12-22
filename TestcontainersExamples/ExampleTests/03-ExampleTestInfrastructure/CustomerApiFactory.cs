using System.Data.Common;
using System.Data.Entity.Infrastructure;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Npgsql;
using WebAapi;

namespace ExampleTests._03_ExampleTestInfrastructure;

public class CustomerApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly TestcontainerDatabase _dbContainer = 
        new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "testdatabase",
                Username = "customUser",
                Password = "customPassword"
            })
            .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.TryAddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(_dbContainer.ConnectionString));
        });
        
        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }
    
    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
    
    private class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}