using System.Net;

namespace Nasa.Business.Exceptions
{
    class ExpectationFailedException : CustomExceptionBase
    {
        public ExpectationFailedException(string message) : base(message, (int)HttpStatusCode.NotFound)
        {
        }
    }
}
