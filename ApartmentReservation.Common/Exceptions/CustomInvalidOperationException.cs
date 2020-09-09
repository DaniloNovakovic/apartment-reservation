using System;
using System.Net;

namespace ApartmentReservation.Common.Exceptions
{
    public class CustomInvalidOperationException : CustomExceptionBase
    {
        public CustomInvalidOperationException() : base("Invalid operation!")
        {
        }

        public CustomInvalidOperationException(string message) : base(message)
        {
        }

        public CustomInvalidOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}