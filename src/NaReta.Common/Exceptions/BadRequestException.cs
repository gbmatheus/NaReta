
using System.Net;

namespace NaReta.Common.Exceptions;

public class BadRequestException : NaRetaExceptionBase
{
    public BadRequestException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors() => new List<string> { Message };
}
