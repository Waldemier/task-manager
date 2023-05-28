namespace TaskManager.Infrastructure.Exceptions;

public sealed class ContextException : Exception
{
    public ContextException(string message) : base(message)
    {
        
    }

    public ContextException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}