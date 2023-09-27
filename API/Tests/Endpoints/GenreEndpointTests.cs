using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace API.Tests;

public sealed class GenreEndpointTests : IDisposable
{
    private readonly HttpClient _httpClient;
    private bool _disposed = false;


    public GenreEndpointTests()
    {
        // Set up an HttpClient for testing your API.
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000"),
        };
    }


    private void Dispose(bool disposing)
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
        var response = await _httpClient.GetAsync("/api/genres"); // Replace with your endpoint URL.

        // Assert: Verify the response.
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DetailsEndpoint_ShouldReturnOk()
    {
        // Random game entity Guid
        const string id = "2070BC89-D753-4BBF-8025-6B07DE327165";
        var response = await _httpClient.GetAsync($"/api/genres/{id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}