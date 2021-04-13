using System.Net;

namespace Nasa.Business.Exceptions
{
    public class BadRequestException: CustomExceptionBase
    {
        public BadRequestException(string message) : base(message, (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
