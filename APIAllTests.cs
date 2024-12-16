using System.Net;
using System.Text;
using AirPort.Presentation.Test.Interfaces;
using Newtonsoft.Json;

namespace AirPort.Presentation.Test;

public class ApiAllTests 
{
    private readonly IApiTests _apiTests;
    
    public ApiAllTests()
    {
        _apiTests = new ApiTests("http://localhost:5138"); 
    }
    
    //успешный пост запрос
    [Fact]
    public async Task UserCanAddNewPlaneSuccessfully()
    {
        // Arrange
        var client = new HttpClient();
        var planePayload = new { SerialNumber = "SN12345", Model = "Boeing747", Manufacturer = "Boeing" };
        var requestContent = new StringContent(JsonConvert.SerializeObject(planePayload), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("http://localhost:5116/postPlane/SN12345?model=Boeing747&manufacturer=Boeing", requestContent);
    
        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.Contains("Boeing747", responseBody); // Проверяем, что самолет был добавлен
    }
    
    // Успешный запрос по серийному номеру
    [Fact]
    public async Task SuccessfullyGetPlaneBySerialNumber()
    {
        // Arrange
        var client = new HttpClient();
        string serialNumber = "SN12345"; // Предполагается, что этот номер существует.
    
        // Act
        var response = await client.GetAsync($"http://localhost:5116/plane/{serialNumber}");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"serialNumber\":\"SN12345\"", responseBody); // Предполагается, что JSON содержит эту информацию
    }
    
    // Запрос на получение информации о несуществующем самолете
    [Fact]
    public async Task GetNonExistentPlaneReturns404()
    {
        // Arrange
        var client = new HttpClient();
        string nonExistentSerialNumber = "SN99999"; 

        // Act
        var response = await client.GetAsync($"http://localhost:5116/plane/{nonExistentSerialNumber}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    //Успешное удаление самолета
    [Fact]
    public async Task SuccessfullyDeletePlane()
    {
        // Arrange
        var client = new HttpClient();
        var deletedPlaneSerialNumber = "6666";

        // Act
        var response = await client.DeleteAsync($"http://localhost:5116/deletePlane/{deletedPlaneSerialNumber}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    //Попытка удалить несуществующий самолет
    [Fact]
    public async Task DeleteNonExistentPlaneReturns404()
    {
        // Arrange
        var client = new HttpClient();
        var deletedPlaneSerialNumber = "PUPUPU";

        // Act
        var response = await client.DeleteAsync($"http://localhost:5116/deletePlane/{deletedPlaneSerialNumber}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

/* NOT WORKING
    [Fact]
    public async Task Post_Endpoint_Should_Return_200()
    {
        var response = await _apiTests.PostPlane("NG737", "boeing", "1235");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    */

//http://localhost:5116