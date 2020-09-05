using System;
using System.Net;

namespace ApartmentReservation.Common.Exceptions
{
    public class ApartmentUnavailableException : CustomExceptionBase
    {
        public ApartmentUnavailableException() : base("Apartment is unavailable!")
        {
        }

        public ApartmentUnavailableException(string message) : base(message)
        {
        }

        public ApartmentUnavailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}