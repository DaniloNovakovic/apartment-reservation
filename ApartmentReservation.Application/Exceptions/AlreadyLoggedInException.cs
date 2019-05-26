using System;

namespace ApartmentReservation.Application.Exceptions
{
    internal class AlreadyLoggedInException : Exception
    {
        public AlreadyLoggedInException() : base("You are already logged in.")
        {
        }

        public AlreadyLoggedInException(string message) : base(message)
        {
        }

        public AlreadyLoggedInException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}