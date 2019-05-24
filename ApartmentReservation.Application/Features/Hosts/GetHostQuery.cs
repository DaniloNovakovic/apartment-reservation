using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetHostQuery : IRequest<string>
    {
        public string Id { get; set; }
    }

    public class GetHostQueryHandler : IRequestHandler<GetHostQuery, string>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetHostQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(GetHostQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            return "valie " + request.Id;
        }
    }

    public class GetHostQueryValidator : AbstractValidator<GetHostQuery>
    {
        public GetHostQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty().MinimumLength(2);
        }
    }
}