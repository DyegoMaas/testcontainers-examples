using System.Net;
using ExampleTests._03_ExampleTestInfrastructure.Infra;
using FluentAssertions;
using WebAapi.Controllers;
using WebAapi.Entities;

namespace ExampleTests._04_ExampleClassFixture;

public class GetCustomerTests : IClassFixture<CustomerApiFactory>, IAsyncLifetime
{
    private readonly CustomerApiFactory _applicationFactory;
    private readonly HttpClient _httpClient;

    public GetCustomerTests(CustomerApiFactory applicationFactory)
    {
        _applicationFactory = applicationFactory;
        _httpClient = applicationFactory.CreateDefaultClient();
    }

    [Fact]
    public async Task GetCustomers_ShouldReturnEmptyList_WhenNoCustomersExist()
    {
        var customers = await _httpClient.GetAsync<List<Customer>>("/Customers");
        
        customers.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Get_ShouldReturnNoFound_WhenNoCustomersExist()
    {
        var response = await _httpClient.GetAsync("/Customers/1");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task Post_ShouldInsert_ValidCustomer()
    {
        var response = await _httpClient.PostAsync<Customer, NewId>("/Customers", new Customer
        {
            Name = "Test Customer"
        });
        
        response.Id.Should().BeGreaterThan(0);
    }
        
    [Fact]
    public async Task GetExistingUser_ShouldReturn_TheUser()
    {
        var postResponse = await _httpClient.PostAsync<Customer, NewId>("/Customers", new Customer
        {
            Name = "Test Customer"
        });
        
        var customer = await _httpClient.GetAsync<Customer>($"/Customers/{postResponse.Id}");

        customer.Name.Should().Be("Test Customer");
    }

    public Task InitializeAsync()
    {
        return _applicationFactory.InitializeAsync();
    }

    public Task DisposeAsync()
    {
        return _applicationFactory.DisposeAsync();
    }
}
