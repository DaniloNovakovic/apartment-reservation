using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ApartmentReservation.Application.Features.Amenities.Commands
{
    public class CreateAmenityCommand : IRequest
    {
        public string Name { get; set; }
    }

    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public CreateAmenityCommandHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<Unit> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}