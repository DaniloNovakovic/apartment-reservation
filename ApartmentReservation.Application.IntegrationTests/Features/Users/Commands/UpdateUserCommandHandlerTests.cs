using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Users.Commands;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Users.Commands
{
    public class UpdateUserCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateUserCommandHandler sut;
        private User dbUser;

        public UpdateUserCommandHandlerTests()
        {
            this.sut = new UpdateUserCommandHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var user = new User()
            {
                Username = "host",
                Password = "host"
            };

            this.dbUser = this.Context.Add(user).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenUserExists_UpdateUser()
        {
            var request = new UpdateUserCommand()
            {
                Id = this.dbUser.Id,
                FirstName = "Danilo",
                LastName = "Novakovic",
                Gender = "Male",
                Password = "codex"
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var host = await this.Context.Users
                .SingleOrDefaultAsync(u => u.Id == request.Id)
                .ConfigureAwait(false);

            Assert.NotNull(host);
            CustomAssertAreEqual(request, host);
        }

        [Fact]
        public async Task UserDoesNotExist_ThrowsNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new UpdateUserCommand() { Id = -1, Password = "codex" }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        private static void CustomAssertAreEqual(UpdateUserCommand request, User user)
        {
            Assert.Equal(request.Password, user.Password);
            Assert.Equal(request.FirstName, user.FirstName);
            Assert.Equal(request.LastName, user.LastName);
            Assert.Equal(request.Gender, user.Gender);
            Assert.False(user.IsDeleted);
        }
    }
}