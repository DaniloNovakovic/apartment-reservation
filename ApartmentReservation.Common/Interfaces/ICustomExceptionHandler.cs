using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApartmentReservation.Common.Interfaces
{
    public interface ICustomExceptionHandler
    {
        HttpStatusCode StatusCode { get; set; }

        void Handle(ExceptionContext context);
    }
}