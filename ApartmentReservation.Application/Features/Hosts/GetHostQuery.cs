using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetHostQuery : IRequest<HostDto>
    {
        public string Id { get; set; }
    }

    public class GetHostQueryHandler : IRequestHandler<GetHostQuery, HostDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetHostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<HostDto> Handle(GetHostQuery request, CancellationToken cancellationToken)
        {
            var host = await unitOfWork.Hosts.GetAsync(new object[] { request.Id }, cancellationToken).ConfigureAwait(false);
            return mapper.Map<HostDto>(host);
        }
    }

    public class GetHostQueryValidator : AbstractValidator<GetHostQuery>
    {
        public GetHostQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }
}