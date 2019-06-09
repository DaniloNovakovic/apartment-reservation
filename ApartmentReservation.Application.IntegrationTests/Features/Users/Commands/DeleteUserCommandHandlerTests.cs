using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Users.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Users.Commands
{
    public class DeleteUserCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly DeleteUserCommandHandler sut;
        private User dbUser;

        public DeleteUserCommandHandlerTests()
        {
            this.sut = new DeleteUserCommandHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var user = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host };

            this.dbUser = this.Context.Users.Add(user).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task UserExists_DeletesUserLogically()
        {
            await this.sut.Handle(new DeleteUserCommand() { Id = this.dbUser.Id }, CancellationToken.None).ConfigureAwait(false);

            var user = await this.Context.Users.FindAsync(this.dbUser.Id).ConfigureAwait(false);

            Assert.True(user.IsDeleted);
        }

        [Fact]
        public async Task UserDoesNotExist_ThrowsNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new DeleteUserCommand() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}