using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Comments.Commands;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Comments.Commands
{
    public class CreateCommentCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly CreateCommentCommandHandler sut;
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
                ApartmentId = this.apartment.Id,
                GuestId = this.guest.UserId,
                Rating = 5,
                Text = "Good apartment, i really like it."
            };

            var response = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var comment = await this.Context.Comments.FindAsync(response.Id).ConfigureAwait(false);

            Assert.NotNull(comment);
            Assert.Equal(request.Rating, comment.Rating);
            Assert.Equal(request.Text, comment.Text);
            Assert.Equal(request.ApartmentId, comment.ApartmentId);
            Assert.Equal(request.GuestId, comment.GuestId);
        }

        protected override void LoadTestData()
        {
            var host = this.Context.Add(new Host() { User = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host } }).Entity;
            this.apartment = this.Context.Add(new Apartment() { Host = host, Title = "Test apartment" }).Entity;
            this.guest = this.Context.Add(new Guest() { User = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest } }).Entity;

            this.Context.SaveChanges();
        }
    }
}