using System;

namespace Secret_Santa_Generator.Model.IdsProvider
{
    public class OutOfIdsException : Exception
    {
        public OutOfIdsException()
        {
        }

        public OutOfIdsException(string message) : base(message)
        {
        }

        public OutOfIdsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}