using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace ExampleTests._00_Introduction;

public class TestcontainersTests : IAsyncLifetime
{
    private TestcontainersContainer _dbContainer =
        new TestcontainersBuilder<TestcontainersContainer>()
            .WithImage("rabbitmq:latest")
            .Build();
    

    [Fact]
    public void It_ShouldCreateRabbitContainer()
    {
        
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