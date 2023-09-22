using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace API.Tests;

public class PlatformEndpointTests : IDisposable
{
    private readonly HttpClient _httpClient;
    private bool _disposed = false;


    public PlatformEndpointTests()
    {
        // Set up an HttpClient for testing your API.
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000"),
        };
    }


    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            // called via myClass.Dispose(). 
            // OK to use any private object references
        }

        _disposed = true;
    }

    public void Dispose() // Implement IDisposable
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task GetEndpoint_ShouldReturnOk()
    {
        // Arrange: Set up any necessary test data or conditions.

        // Act: Make a GET request to your API endpoint.
        var response = await _httpClient.GetAsync("/api/platforms"); // Replace with your endpoint URL.

        // Assert: Verify the response.
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DetailsEndpoint_ShouldReturnOk()
    {
        // Random platform entity Guid
        const string id = "5111D33E-33BE-4B9D-856A-A2D6D3E9EA0D";
        var response = await _httpClient.GetAsync($"/api/platforms/{id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}