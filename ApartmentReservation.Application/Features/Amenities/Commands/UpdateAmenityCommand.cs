using System;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ApartmentReservation.Application.Features.Amenities.Commands
{
    public class UpdateAmenityCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public UpdateAmenityCommandHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<Unit> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}