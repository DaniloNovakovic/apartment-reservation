using System;
using System.Net;

namespace ApartmentReservation.Common.Exceptions
{
    public class AlreadyLoggedInException : CustomExceptionBase
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

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}