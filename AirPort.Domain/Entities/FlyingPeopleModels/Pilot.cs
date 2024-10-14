using AirPort.Domain.Enums;

namespace AirPort.Domain.Entities.FlyingPeopleModels;

public class Pilot : BasicFlyingPerson
{
    public Pilot(string name, string surname, Genders gender, PilotPositions position) : base(name, surname, gender)
    {
        CurrentPilotPosition = position;
    }
    
    public PilotPositions CurrentPilotPosition { get; private set; }
    
    public void Demotion()
    {
        if (CurrentPilotPosition == PilotPositions.Second)
            throw new InvalidOperationException("You are already second in the cabin");
        
        CurrentPilotPosition = PilotPositions.Second;
    }

    public void Promotion()
    {
        if (CurrentPilotPosition == PilotPositions.Captain)
            throw new InvalidOperationException("You are already boss in the cabin");

        CurrentPilotPosition = PilotPositions.Captain; 
    }

    public static Pilot Create(string name, string surname, string middleName, Genders gender, PilotPositions position) =>
        new Pilot(name: name, surname: surname, gender: gender, position: position)
        {
            MiddleName = middleName
        };
}