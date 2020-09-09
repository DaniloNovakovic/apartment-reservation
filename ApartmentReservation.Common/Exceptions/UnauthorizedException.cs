using System;
using System.Net;

namespace ApartmentReservation.Common.Exceptions
{
    public class UnauthorizedException : CustomExceptionBase
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

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}