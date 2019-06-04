using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MediatR;

namespace ApartmentReservation.Application.Features.Hosts.Commands
{
    public class UpdateHostCommand : IRequest
    {
        public long Id { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Gender { get; set; } = "";
    }

    public class UpdateHostCommandValidator : AbstractValidator<UpdateHostCommand>
    {
        public UpdateHostCommandValidator()
        {
            this.RuleFor(u => u.Password).NotEmpty().MinimumLength(4);
        }
    }
}