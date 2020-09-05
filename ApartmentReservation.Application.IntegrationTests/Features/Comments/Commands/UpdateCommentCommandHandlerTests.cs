using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Comments.Commands;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Comments.Commands
{
    public class UpdateCommentCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateCommentCommandHandler sut;
        private Comment comment;

        public UpdateCommentCommandHandlerTests()
        {
            this.sut = new UpdateCommentCommandHandler(this.Context);
        }

        [Fact]
        public async Task UpdatesComment()
        {
            var request = new UpdateCommentCommand() { Id = this.comment.Id, Text = "My New Text", Rating = 3, Approved = true };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbComment = await this.Context.Comments.FindAsync(this.comment.Id).ConfigureAwait(false);

            Assert.Equal(request.Text, dbComment.Text);
            Assert.Equal(request.Rating, dbComment.Rating);
            Assert.Equal(request.Approved, dbComment.Approved);
        }

        protected override void LoadTestData()
        {
            var host = this.Context.Add(new Host() { User = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host } }).Entity;
            var apartment = this.Context.Add(new Apartment() { Host = host, Title = "Test apartment" }).Entity;
            var guest = this.Context.Add(new Guest() { User = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest } }).Entity;
            this.comment = this.Context.Add(new Comment() { Apartment = apartment, Guest = guest, Text = "My text" }).Entity;

            this.Context.SaveChanges();
        }
    }
}