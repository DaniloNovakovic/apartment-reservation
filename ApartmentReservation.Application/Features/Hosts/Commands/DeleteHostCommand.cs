using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ApartmentReservation.Application.Features.Hosts.Commands
{
    public class DeleteHostCommand : IRequest
    {
        public long Id { get; set; }
    }
}