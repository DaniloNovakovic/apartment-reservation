using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentReservation.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("You are not authorized to perform requested operation.")
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
}