using System.Net;

namespace Nasa.Business.Exceptions
{
    public class NotFoundException : CustomExceptionBase
    {
        public NotFoundException(string message) : base(message, (int)HttpStatusCode.NotAcceptable)
        {
        }
    }
}
