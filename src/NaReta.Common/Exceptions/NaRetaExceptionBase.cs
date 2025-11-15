namespace NaReta.Common.Exceptions;

public abstract class NaRetaExceptionBase : System.Exception
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();

    public NaRetaExceptionBase(string message) : base(message) { }
}
