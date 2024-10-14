using Airport.Web.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AirPort.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaneController(IMemoryCache memoryCache) : ControllerBase
{
    //todo: private func
    //404 не нужна, только считает
    [HttpGet]
    public async Task<ActionResult<List<PlaneViewModel>>> Get(CancellationToken cancellationToken = default)
    {
        var plane = memoryCache;

        return Ok(plane);
    }
    
    //'?' - переменная может быть null
    [HttpGet("BySerialNumber")]
    public async Task<ActionResult<PlaneViewModel>> GetBySerialNumber(string serialNumber,
        CancellationToken cancellationToken = default)
    {
        if (memoryCache.TryGetValue(serialNumber, out PlaneViewModel? plane))
        {
            return Ok(plane);
        }
        
        return NotFound();
    }
    
    // с методом GetOrCreate
    // вопрос: что должно возвращаться если такое серийный номер уже существует
    [HttpPost("AddToListOfPlanes")]
    public async Task<ActionResult<PlaneViewModel>> Post(string? model, string manufacturer, 
        string serialNumber, CancellationToken cancellationToken = default)
    {
        var plane = memoryCache.GetOrCreate(serialNumber, addToCache => 
            new PlaneViewModel(Model: model, Manufacturer: manufacturer, SerialNumber: serialNumber));
        
        return Ok(plane);
    }

    //'?' - переменная может быть null
    [HttpDelete("DeletePlane")]
    public async Task<ActionResult<PlaneViewModel>> Delete(string serialNumber, CancellationToken cancellationToken)
    {
        if (memoryCache.TryGetValue(serialNumber, out PlaneViewModel? plane))
        {
            memoryCache.Remove(serialNumber);
            return Ok(plane);
        }
        
        return NotFound();
    }
    
}

