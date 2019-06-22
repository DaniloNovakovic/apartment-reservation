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
    public class UpdateCommentCommandHandlerTests : InMemoryContextTestBase
    {
        private UpdateCommentCommandHandler sut;
        private Comment comment;

        public UpdateCommentCommandHandlerTests()
        {
            this.sut = new UpdateCommentCommandHandler(this.Context);
        }

        [Fact]
        public async Task UpdatesComment()
        {
            var request = new UpdateCommentCommand() { Id = comment.Id, Text = "My New Text", Rating = 3, Approved = true };

            await sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbComment = await Context.Comments.FindAsync(comment.Id).ConfigureAwait(false);

            Assert.Equal(request.Text, dbComment.Text);
            Assert.Equal(request.Rating, dbComment.Rating);
            Assert.Equal(request.Approved, dbComment.Approved);
        }

        protected override void LoadTestData()
        {
            var host = Context.Add(new Host() { User = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host } }).Entity;
            var apartment = Context.Add(new Apartment() { Host = host, Title = "Test apartment" }).Entity;
            var guest = Context.Add(new Guest() { User = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest } }).Entity;
            this.comment = Context.Add(new Comment() { Apartment = apartment, Guest = guest, Text = "My text" }).Entity;

            Context.SaveChanges();
        }
    }
}