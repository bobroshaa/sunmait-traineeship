namespace ClothingStore.Application.Exceptions;

public class IncorrectParamsException : Exception
{
    public IncorrectParamsException(string message) : base(message)
    {
    }
}