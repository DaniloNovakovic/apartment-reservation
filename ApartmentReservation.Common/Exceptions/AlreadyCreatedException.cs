using System;
using System.Net;

namespace ApartmentReservation.Common.Exceptions
{
    public class AlreadyCreatedException : CustomExceptionBase
    {
        public AlreadyCreatedException() : base("Resource is already created.")
        {
        }

        public AlreadyCreatedException(string message) : base(message)
        {
        }

        public AlreadyCreatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}