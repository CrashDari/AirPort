using AirPort.Domain.Enums;

namespace AirPort.Domain.Interfaces;

public interface IFlyingPersonType
{
    string Surname { get; }
    string Name { get; }
    Genders Gender { get; }
    string MiddleName { get; }
}