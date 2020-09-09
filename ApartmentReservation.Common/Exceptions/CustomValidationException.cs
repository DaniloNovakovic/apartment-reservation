using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApartmentReservation.Common.Exceptions
{
    public class CustomValidationException : CustomExceptionBase
    {
        public CustomValidationException()
            : base("One or more validation failures have occurred.")
        {
        }

        public CustomValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (string propertyName in propertyNames)
            {
                string[] propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public CustomValidationException(string message) : base(message)
        {
        }

        public CustomValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

        public override void Handle(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)StatusCode;

            var errors = new List<string>();

            foreach (string[] propNameFailures in ((CustomValidationException)context.Exception).Failures.Values)
            {
                errors.AddRange(propNameFailures);
            }

            context.Result = new JsonResult(new
            {
                error = errors.ToArray()
            });
        }
    }
}