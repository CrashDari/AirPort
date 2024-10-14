namespace AirPort.Domain.Exceptions;

public class PilotsLimitReachedException(string message) : Exception(message);