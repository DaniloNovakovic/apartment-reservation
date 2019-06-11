using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Users.Queries
{
    public class GetUserByIdQueryHandlerTests : InMemoryContextTestBase
    {
        private readonly GetUserByIdQueryHandler sut;
        private User dbUser;

        public GetUserByIdQueryHandlerTests()
        {
            this.sut = new GetUserByIdQueryHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var user = new User() { Username = "admin", Password = "admin", RoleName = RoleNames.Administrator };

            this.dbUser = this.Context.Users.Add(user).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenUserExists_ReturnUser()
        {
            var result = await this.sut.Handle(new GetUserByIdQuery() { Id = this.dbUser.Id }, CancellationToken.None).ConfigureAwait(false);

            Assert.IsAssignableFrom<UserDto>(result);
            Assert.Equal(this.dbUser.Username, result.Username);
        }

        [Fact]
        public async Task WhenUserDoesNotExist_ThrowNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new GetUserByIdQuery() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}