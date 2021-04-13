using System.Net;

namespace Nasa.Business.Exceptions
{
    public class NotAcceptableException : CustomExceptionBase
    {
        public NotAcceptableException(string message) : base(message, (int)HttpStatusCode.NotAcceptable)
        {
        }
    }
}
