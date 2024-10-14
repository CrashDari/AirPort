using AirPort.Domain.Enums;

namespace AirPort.Domain.Entities.FlyingPeopleModels;

public class Passenger : BasicFlyingPerson
{
    public Passenger(string name, string surname, Genders gender, PassengersFlyingClasses flyingClass) : base(name, surname, gender)
    {
        PassengerClass = flyingClass;
    }
    public PassengersFlyingClasses PassengerClass { get; private set; }

    public void ClassPromotion()
    {
        if (PassengerClass == PassengersFlyingClasses.Bisness)
            throw new InvalidOperationException("You are already in comfort seats");

        PassengerClass = PassengersFlyingClasses.Bisness;
    }

    public void ClassDemotion()
    {
        if (PassengerClass == PassengersFlyingClasses.Economy)
            throw new InvalidOperationException("You are already with knee in pain");

        PassengerClass = PassengersFlyingClasses.Economy;
        
    }

    public static Passenger Create(string name, string surname, string middleName, 
        Genders gender, PassengersFlyingClasses flyingClass) => 
        new Passenger(name: name, surname: surname, gender: gender,
        flyingClass: flyingClass) 
        { 
            MiddleName = middleName
        };
}