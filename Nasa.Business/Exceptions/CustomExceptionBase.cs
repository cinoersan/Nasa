using System;

namespace Nasa.Business.Exceptions
{
    public class CustomExceptionBase: Exception
    {
        public int Code { get; }

        public CustomExceptionBase(string message, int code) : base(message)
        {
            Code = code;
        }
    }
}
