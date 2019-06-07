using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Users.Queries
{
    public class GetAllUsersQueryHandlerTests : InMemoryContextTestBase
    {
        private List<User> dbUsers;
        private readonly GetAllUsersQueryHandler sut;

        public GetAllUsersQueryHandlerTests()
        {
            this.sut = new GetAllUsersQueryHandler(this.Context, this.Mapper);
        }

        protected override void LoadTestData()
        {
            this.dbUsers = new List<User>()
            {
                new User()
                {
                    Username = "host",
                    Password = "host",
                    RoleName = RoleNames.Host,
                    IsDeleted = true
                },
                new User()
                {
                    Username = "guest",
                    Password = "guest",
                    RoleName = RoleNames.Guest
                },
                new User()
                {
                    Username = "admin",
                    Password = "admin",
                    RoleName = RoleNames.Administrator
                }
            };

            this.Context.Users.AddRange(this.dbUsers);
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task ReturnsUndeletedUsers()
        {
            var expectedResultUsernames = new List<string> { "guest", "admin" };

            var result = await this.sut.Handle(new GetAllUsersQuery(), CancellationToken.None).ConfigureAwait(false);
            var returnedUsers = result.Select(u => u.Username);

            Assert.Equal(expectedResultUsernames, returnedUsers);
        }
    }
}