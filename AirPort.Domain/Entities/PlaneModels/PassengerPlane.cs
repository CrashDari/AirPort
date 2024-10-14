using System.Collections.Immutable;
using AirPort.Domain.Entities.FlyingPeopleModels;
using AirPort.Domain.Enums;

namespace AirPort.Domain.Entities.PlaneModels;

public class PassengerPlane : BasicPlane
{
    private readonly HashSet<Passenger> _passengers = [];
    
    public PassengerPlane(string manufacturer, EnginesConfigurations engineConfiguration, string model,
        int pilotsNumber, int passengerCapacity) : base(manufacturer, engineConfiguration, model, pilotsNumber)
    {
        TakeOffAltitude = 9000;
        
        PassengerCapacity = passengerCapacity;
        
        if (PassengerCapacity <= 0)
            throw new ArgumentException("Passenger capacity can't be null or negative");

        if (PassengerCapacity > 500)
            throw new ArgumentException("You can't create a plane with such number of passengers");
    }
    
    public int PassengerCapacity { get; init; }
    public IEnumerable<Passenger> Passengers => _passengers.ToImmutableArray();
 
    public void AddToPassengerPlane(Passenger passenger)
    {
        if (_passengers.Count == PassengerCapacity)
            throw new InvalidOperationException("Passenger limit reached");
        
        _passengers.Add(passenger);
    }

    public static PassengerPlane Create(string manufacturer, EnginesConfigurations engineConfiguration, string model,
        int pilotsNumber, int passengerCapacity) => new(manufacturer: manufacturer,
        engineConfiguration: engineConfiguration, model: model, pilotsNumber: pilotsNumber,
        passengerCapacity: passengerCapacity);
}