using System.Net;

namespace Nasa.Business.Exceptions
{
    public class UnauthorizedException : CustomExceptionBase
    {
        public UnauthorizedException(string message) : base(message, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}
