using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApartmentReservation.Application.Exceptions
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