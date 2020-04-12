using System;
using System.Collections.Generic;
using System.Text;

namespace ZaizaiDate.Common.Exceptions
{
    public class ServerHandledException : Exception
    {
        public ServerHandledException(string message) : base(message)
        {
        }

        public ServerHandledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ServerHandledException()
        {
        }
    }
}
