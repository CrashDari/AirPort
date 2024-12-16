namespace AirPort.Presentation.Test.Interfaces;

public interface IApiTests
{
    Task<HttpResponseMessage> GetPlaneBySerialNumber(string url, string serialNumber);
    Task<HttpResponseMessage> PostPlane(string model, string manufacturer, string serialNumber);
    Task<HttpResponseMessage> DeletePlane(string url, string serialNumber);
}