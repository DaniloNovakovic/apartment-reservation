using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Comments.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Comments.Commands
{
    public class CreateCommentCommandHandlerTests : InMemoryContextTestBase
    {
        private CreateCommentCommandHandler sut;
        private Apartment apartment;
        private Guest guest;

        public CreateCommentCommandHandlerTests()
        {
            this.sut = new CreateCommentCommandHandler(this.Context);
        }

        [Fact]
        public async Task CreatesComment()
        {
            var request = new CreateCommentCommand()
            {
                ApartmentId = apartment.Id,
                GuestId = guest.UserId,
                Rating = 5,
                Text = "Good apartment, i really like it."
            };

            var response = await sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var comment = await Context.Comments.FindAsync(response.Id).ConfigureAwait(false);

            Assert.NotNull(comment);
            Assert.Equal(request.Rating, comment.Rating);
            Assert.Equal(request.Text, comment.Text);
            Assert.Equal(request.ApartmentId, comment.ApartmentId);
            Assert.Equal(request.GuestId, comment.GuestId);
        }

        protected override void LoadTestData()
        {
            var host = Context.Add(new Host() { User = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host } }).Entity;
            this.apartment = Context.Add(new Apartment() { Host = host, Title = "Test apartment" }).Entity;
            this.guest = Context.Add(new Guest() { User = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest } }).Entity;

            Context.SaveChanges();
        }
    }
}