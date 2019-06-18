using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class AddImagesToApartmentCommand : IRequest
    {
        public AddImagesToApartmentCommand()
        {
            this.Images = new List<IFormFile>();
        }

        public IEnumerable<IFormFile> Images { get; set; }
    }

    public class AddImagesToApartmentCommandHandler : IRequestHandler<AddImagesToApartmentCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IHostingEnvironment env;

        public AddImagesToApartmentCommandHandler(IApartmentReservationDbContext context, IHostingEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public async Task<Unit> Handle(AddImagesToApartmentCommand request, CancellationToken cancellationToken)
        {
            string imagesFolderPath = Path.Combine(this.env.WebRootPath, "images");

            foreach (var formFile in request.Images)
            {
                string filePath = Path.Combine(imagesFolderPath, formFile.FileName);
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream).ConfigureAwait(false);
                    }
                }
            }

            await Task.Delay(20).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}