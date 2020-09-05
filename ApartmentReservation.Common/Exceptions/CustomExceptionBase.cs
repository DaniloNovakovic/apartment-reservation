using System;
using System.Net;
using ApartmentReservation.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApartmentReservation.Common.Exceptions
{
    public class CustomExceptionBase : Exception, ICustomExceptionHandler
    {
        public CustomExceptionBase() : base()
        {
        }

        public CustomExceptionBase(string message) : base(message)
        {
        }

        public CustomExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        public virtual HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public virtual void Handle(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)StatusCode;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message }
            });
        }
    }
}