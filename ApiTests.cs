using System.Net.Http.Json;
using AirPort.Presentation.Test.Interfaces;

namespace AirPort.Presentation.Test;

public class ApiTests : IApiTests
{
    private readonly HttpClient _client;

    public ApiTests(string baseAddress)
    {
        _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
    }
    
    public async Task<HttpResponseMessage> GetPlaneBySerialNumber(string url, string serialNumber)
    {
        //var url = $"Plane/BySerialNumber?serialNumber={serialNumber}"; 
        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        return response;
    }
    
    public async Task<HttpResponseMessage> DeletePlane(string url, string serialNumber)
    {
        //var url = $"Plane/DeletePlane?serialNumber={serialNumber}"; 
        var response = await _client.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
        
        return response;
    }
    
    public async Task<HttpResponseMessage> PostPlane(string model, string manufacturer, string serialNumber)
    {
        var url = $"Plane/AddToListOfPlanes?model={model}&manufacturer={manufacturer}&serialNumber={serialNumber}"; 
        var response = await _client.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
        
        return response;
    }
    
}