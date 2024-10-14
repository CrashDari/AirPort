//using AirPort.Domain.Enums;

using AirPort.Domain.Enums;

namespace AirPort.Domain.Entities.PlaneModels;

public class CargoPlane : BasicPlane
{
    public float LiftingCapacity { get; init; }

    public CargoPlane(string manufacturer, EnginesConfigurations engineConfiguration, string model, int pilotsNumber, int liftingCapacity) :
        base(manufacturer, engineConfiguration, model, pilotsNumber)
    {
        TakeOffAltitude = 12000;
        
        LiftingCapacity = liftingCapacity;
        
        if (LiftingCapacity <= 0)
        {
            throw new ArgumentException("lifting capacity can't be null or negative");
            
        }

        if (liftingCapacity > 53000)
        {
            throw new ArgumentException("Lifting capacity can't be more than 53000");
        }
    }
    
    public float CurrentLiftingCapacity { get; private set; }
    
    public void AddLuggageToPlane(float luggage)
    {
        if (luggage > LiftingCapacity)
        {
            throw new ArgumentException("Box is too big");
        }
        if (luggage < 0)
        {
            throw new ArgumentException("lifting capacity can't be negative");
        }
        CurrentLiftingCapacity = luggage;
    }

    public static CargoPlane Create(string manufacturer, EnginesConfigurations engineConfiguration, string model,
        int pilotsNumber, int liftingCapacity) => new CargoPlane(manufacturer: manufacturer,
        engineConfiguration: engineConfiguration, model: model, pilotsNumber: pilotsNumber,
        liftingCapacity: liftingCapacity);
}
