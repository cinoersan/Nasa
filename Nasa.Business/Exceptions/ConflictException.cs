using System.Net;

namespace Nasa.Business.Exceptions
{
    public class ConflictException : CustomExceptionBase
    {
        public ConflictException(string message) : base(message, (int)HttpStatusCode.Conflict)
        {
        }
    }
}
