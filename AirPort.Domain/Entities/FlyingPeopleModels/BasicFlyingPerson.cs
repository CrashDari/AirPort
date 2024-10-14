using AirPort.Domain.Enums;
using AirPort.Domain.Interfaces;

namespace AirPort.Domain.Entities.FlyingPeopleModels;

public abstract class BasicFlyingPerson : IFlyingPersonType
{
    public BasicFlyingPerson(string name, string surname, Genders gender)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Name can't be null or empty");
        
        Name = name;
        
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException("Surename cant't be null or empty");
        Surname = surname;
        
        if (gender != Genders.Female && gender != Genders.Male)
            throw new InvalidOperationException("You set the wrong gender");
        Gender = gender;
    }
    
    public string Surname { get; }
    public string Name { get; }
    public Genders Gender { get;}
    public string MiddleName { get; init; }
    
}