using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class AddImagesToApartmentCommand : IRequest
    {
        public AddImagesToApartmentCommand()
        {
            this.Images = new List<IFormFile>();
        }

        public IEnumerable<IFormFile> Images { get; set; }
        public long ApartmentId { get; set; }
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
            var apartment = await context.Apartments.SingleOrDefaultAsync(a => a.Id == request.ApartmentId, cancellationToken);

            if(apartment == null)
            {
                throw new NotFoundException($"Apartment with id {request.ApartmentId} not found!");
            }

            string imagesFolderPath = Path.Combine(this.env.WebRootPath, "images");

            foreach (var formFile in request.Images)
            {
                string imgFileName = $"apartment_{request.ApartmentId}_{formFile.FileName}";
                string filePath = Path.Combine(imagesFolderPath, imgFileName);
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream, cancellationToken).ConfigureAwait(false);
                    }

                    AddImage(imgFileName, request.ApartmentId);
                }
            }

            apartment.IsSyncNeeded = true;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private void AddImage(string imgFileName, long apartmentId)
        {
            this.context.Images.Add(new Domain.Entities.Image()
            {
                ApartmentId = apartmentId,
                ImageUri = "./images/" + imgFileName
            });
        }
    }
}