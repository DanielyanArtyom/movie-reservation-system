namespace MovieReservation.Business.Exceptions;

public class DuplicateFoundException : Exception
{
    public DuplicateFoundException()
    {
    }

    public DuplicateFoundException(string message)
        : base(message)
    {
    }

    public DuplicateFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}