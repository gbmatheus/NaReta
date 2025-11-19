
using System.Net;

namespace NaReta.Common.Exceptions;

public class NotFoundException : NaRetaExceptionBase
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors() => new List<string> { Message };
}
