using System;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ApartmentReservation.Application.Features.Amenities.Commands
{
    public class DeleteAmenityCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteAmenityCommandHandler : IRequestHandler<DeleteAmenityCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public DeleteAmenityCommandHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<Unit> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}