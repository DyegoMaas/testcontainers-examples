

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using FluentAssertions;
using Npgsql;

namespace ExampleTests;

public class Testcontainers1Tests : IAsyncLifetime
{
    private readonly TestcontainersContainer _dbContainer = 
        new TestcontainersBuilder<TestcontainersContainer>()
            .WithImage("postgres:11")
            .WithName("my-postgres")
            .WithEnvironment("POSTGRES_DB", "testdatabase")
            .WithEnvironment("PGDATA", "/data/postgres")
            .WithEnvironment("POSTGRES_USERNAME", "customUser")
            .WithEnvironment("POSTGRES_PASSWORD", "customPassword")
            .WithPortBinding(5555, 5432)
            .Build();
    
    [Fact]
    public async Task Should_Select1_FromDatabase()
    {
        await using var connection = new NpgsqlConnection("Host=localhost:5555;Username=customUser;Password=customPassword;Database=testdatabase");
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