using System;
using System.Net;

namespace ApartmentReservation.Common.Exceptions
{
    public class NotFoundException : CustomExceptionBase
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }

        public NotFoundException() : base("Requested resource could not be found")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;
    }
}