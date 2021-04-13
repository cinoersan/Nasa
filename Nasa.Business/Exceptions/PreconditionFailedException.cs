using System.Net;

namespace Nasa.Business.Exceptions
{
    public class PreconditionFailedException : CustomExceptionBase
    {
        public PreconditionFailedException(string message) : base(message, (int)HttpStatusCode.PreconditionFailed)
        {
        }
    }
}
