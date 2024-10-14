using System.Collections.Immutable;
using AirPort.Domain.Entities.FlyingPeopleModels;
using AirPort.Domain.Enums;
using AirPort.Domain.Exceptions;
using AirPort.Domain.Interfaces;

namespace AirPort.Domain.Entities.PlaneModels;

public abstract class BasicPlane : IPlaneType
{
    private readonly HashSet<Pilot> _pilots = [];
    
    public BasicPlane(string manufacturer, EnginesConfigurations engineConfiguration, string model, int pilotsNumber)
    {
        if (string.IsNullOrWhiteSpace(manufacturer))
            throw new ArgumentNullException("Manufacturer can't be null or empty");

        Manufacturer = manufacturer;
        
        if (engineConfiguration != EnginesConfigurations.Four && engineConfiguration != EnginesConfigurations.Two)
            throw new InvalidOperationException("You can't take off with this number of engines");
        
        EnginesConfiguration = engineConfiguration;

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentNullException("Model cant't be null or empty");
        
        Model = model;
        
        if (pilotsNumber < 0)
        {
            throw new InvalidOperationException("Empty cabin has 0 pilots, not negative number");
        }

        if (pilotsNumber > 2)
            throw new InvalidOperationException("Too much pilots in cabin");

        PilotsNumber = pilotsNumber;
        
        Condition = FlyingConditions.OnTheGround;
        
        
        CurrentFlyingAltitudeMeters = 0;

    }
    public string Manufacturer { get;}
    public string Model { get;}
    public EnginesConfigurations EnginesConfiguration { get;}
    public int PilotsNumber { get; }
    public int CurrentFlyingAltitudeMeters { get; private set; }
    public int TakeOffAltitude { get; init;}
    public FlyingConditions Condition { get; private set; }
    
    public void TakeOff()
    {
        if (  _pilots.Count < 2)
            throw new InvalidOperationException("You can take off only with 2 and more pilots on board");
        
        if (Condition is FlyingConditions.TakingOff or FlyingConditions.OnAir or FlyingConditions.TakingDown)
            throw new InvalidOperationException("You have already take off the ground");
        
        while (CurrentFlyingAltitudeMeters < TakeOffAltitude)
        {
            CurrentFlyingAltitudeMeters += 1;
            Condition = FlyingConditions.TakingOff;
        }

        Condition = FlyingConditions.OnAir;
    }
    
    public void TakeDown()
    {
        if (_pilots.Count <2 )
            throw new InvalidOperationException("You can take down only with 2 pilots on board");
        
        if (Condition == FlyingConditions.OnTheGround || Condition == FlyingConditions.TakingDown)
            throw new InvalidOperationException("You have already done that");
        if (Condition == FlyingConditions.TakingOff)
            throw new InvalidOperationException("You did not take down");
        
        while (CurrentFlyingAltitudeMeters >0)
        {
            CurrentFlyingAltitudeMeters -= 1;
            Condition = FlyingConditions.TakingDown;
        }
        
        Condition = FlyingConditions.OnTheGround;
    }
    
    public IEnumerable<Pilot> Pilots => _pilots.ToImmutableArray();

    public void AddPilotToPlane(Pilot pilot)
    {
        var position = _pilots
            .FirstOrDefault(p => p.CurrentPilotPosition.Equals(pilot.CurrentPilotPosition));

        if (position is not null) 
            throw new DoublePilotException("You have already set this type of pilot in cabin");
        
        if (_pilots.Count == PilotsNumber)
            throw new PilotsLimitReachedException("Pilots limit reached");
        
        _pilots.Add(pilot);
    }
}