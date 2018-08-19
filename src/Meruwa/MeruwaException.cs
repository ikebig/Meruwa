using System;

namespace Meruwa
{
    public class MeruwaException : Exception
    {
        public MeruwaException() : base()
        {
        }
        public MeruwaException(string message) : base(message)
        {
        }
        public MeruwaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
