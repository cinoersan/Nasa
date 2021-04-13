using System.Net;

namespace Nasa.Business.Exceptions
{
    public class ForbiddenException : CustomExceptionBase
    {
        public ForbiddenException(string message) : base(message, (int)HttpStatusCode.Forbidden)
        {
        }
    }
}
