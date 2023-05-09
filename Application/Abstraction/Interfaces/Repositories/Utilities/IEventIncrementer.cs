namespace Application.Abstraction.Interfaces.Repositories.Utilities
{
    public interface IEventIncrementer
    {
         string EventSequence(string value);
    }
}