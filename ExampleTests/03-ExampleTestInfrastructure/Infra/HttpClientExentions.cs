using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ExampleTests._03_ExampleTestInfrastructure.Infra;

public static class HttpClientExentions
{
    public static async Task<TResponse> PostAsync<TRequest, TResponse>(this HttpClient httpClient, string requestUri, TRequest payload)
    {
        var json = JsonSerializer.Serialize(payload);
        
        var response = await httpClient.PostAsync(
            content: new StringContent(json, Encoding.UTF8, "application/json"),
            requestUri: requestUri
        );
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public static async Task<TResponse> GetAsync<TResponse>(this HttpClient httpClient, string requestUri)
    {
        var response = await httpClient.GetAsync(requestUri);
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
}