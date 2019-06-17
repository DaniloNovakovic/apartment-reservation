using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using MediatR;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class UpdateApartmentAmenitiesCommand : IRequest
    {
        public UpdateApartmentAmenitiesCommand()
        {
            this.Amenities = new List<AmenityDto>();
        }

        public IEnumerable<AmenityDto> Amenities { get; set; }
        public long ApartmentId { get; set; }
    }

    public class UpdateApartmentAmenitiesCommandHandler : IRequestHandler<UpdateApartmentAmenitiesCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateApartmentAmenitiesCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public Task<Unit> Handle(UpdateApartmentAmenitiesCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}