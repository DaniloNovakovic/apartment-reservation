using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace ApartmentReservation.Application.Exceptions
{
    public class CustomValidationException : Exception
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

                this.Failures.Add(propertyName, propertyFailures);
            }
        }

        public CustomValidationException(string message) : base(message)
        {
        }

        public CustomValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();
    }
}