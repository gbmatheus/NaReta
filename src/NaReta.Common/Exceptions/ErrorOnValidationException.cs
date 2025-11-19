using System.Net;

namespace NaReta.Common.Exceptions;


public class ErrorOnValidationException : NaRetaExceptionBase
{
    private List<string> _errors = new List<string>();

    public ErrorOnValidationException(List<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors() => _errors;
}