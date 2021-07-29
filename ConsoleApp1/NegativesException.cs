using System;

namespace Kata
{
    public class NegativesException : Exception
    {
        public NegativesException(string message)
            : base(message)
        {

        }

        public NegativesException()
            : base()
        {

        }
    }
}
