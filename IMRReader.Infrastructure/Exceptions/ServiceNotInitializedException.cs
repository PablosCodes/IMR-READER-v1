using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Infrastructure.Exceptions
{
    public class ServiceNotInitializedException : Exception
    {
        public ServiceNotInitializedException() : base("The service was not properly initialized.")
        {
        }

        public ServiceNotInitializedException(string message) : base(message)
        {
        }

        public ServiceNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
