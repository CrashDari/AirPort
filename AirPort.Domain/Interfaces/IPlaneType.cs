using AirPort.Domain.Enums;

namespace AirPort.Domain.Interfaces;

interface IPlaneType
{
    string Manufacturer { get; }
    string Model { get; }
    EnginesConfigurations EnginesConfiguration { get; }
    int PilotsNumber { get; }
    int CurrentFlyingAltitudeMeters { get; }
    int TakeOffAltitude { get; }
    FlyingConditions Condition { get; }

    void TakeOff();
    void TakeDown();
}
