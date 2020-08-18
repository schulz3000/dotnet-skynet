using System;

namespace Skynet
{
    public class SkynetException : Exception
    {
        public SkynetException()
        {
        }

        public SkynetException(string message)
            : base(message)
        {
        }

        public SkynetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
