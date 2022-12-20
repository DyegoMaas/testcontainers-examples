using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FluentAssertions;
using Npgsql;

namespace ExampleTests.Example2;

public class Testcontainers2Tests : IAsyncLifetime
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

    [Fact]
    public async Task Should_Select1_FromDatabase()
    {
        await using var connection = new NpgsqlConnection(_dbContainer.ConnectionString);
        await connection.OpenAsync();

        var command = new NpgsqlCommand("SELECT 1", connection);
        var result = (int?)await command.ExecuteScalarAsync();

        result.Should().Be(1);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }
    
    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}