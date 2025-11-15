
using System.Net;

namespace NaReta.Common.Exceptions;

public class DomainException : NaRetaExceptionBase
{
    public DomainException(string message) : base(message) { }

    public override int StatusCode => (int)HttpStatusCode.UnprocessableEntity;

    public override List<string> GetErrors() => new List<string> { Message };
}
