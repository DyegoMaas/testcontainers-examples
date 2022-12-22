namespace ExampleTests._03_ExampleTestInfrastructure;

public class GetCustomerTests : CustomerApiFactory
{
    [Fact]
    public async Task Get_ShouldReturnNoCustomers_AtFirst()
    {
        var httpClient = base.CreateDefaultClient();
        var response = await httpClient.GetAsync("/WeatherForecast");
    }
}