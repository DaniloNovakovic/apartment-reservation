using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Users.Queries
{
    public class GetUserByUsernameQueryHandlerTests : InMemoryContextTestBase
    {
        private readonly GetUserByUsernameQueryHandler sut;
        private User dbUser;

        public GetUserByUsernameQueryHandlerTests()
        {
            this.sut = new GetUserByUsernameQueryHandler(this.Context);
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
            var result = await this.sut.Handle(new GetUserByUsernameQuery() { Username = this.dbUser.Username }, CancellationToken.None).ConfigureAwait(false);

            var resultDto = Assert.IsAssignableFrom<UserDto>(result);
            Assert.Equal(this.dbUser.Id, resultDto.Id);
        }

        [Fact]
        public async Task WhenUserDoesNotExist_ThrowNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new GetUserByUsernameQuery() { Username = "" }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}