using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Users.Queries
{
    public class GetAllUsersQueryHandlerTests : InMemoryContextTestBase
    {
        private const string MaleGuestUsername = "maleGuest";
        private const string FemaleGuestUsername = "femaleGuest";
        private const string FemaleAdminUsername = "femaleAdmin";
        private List<User> dbUsers;
        private readonly GetAllUsersQueryHandler sut;

        public GetAllUsersQueryHandlerTests()
        {
            this.sut = new GetAllUsersQueryHandler(this.Context);
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
                    Gender = Genders.Male,
                    IsDeleted = true
                },
                new User()
                {
                    Username = MaleGuestUsername,
                    Password = "guest",
                    RoleName = RoleNames.Guest,
                    Gender = Genders.Male
                },
                new User()
                {
                    Username = FemaleGuestUsername,
                    Password = "guest",
                    RoleName = RoleNames.Guest,
                    Gender = Genders.Female
                },
                new User()
                {
                    Username = FemaleAdminUsername,
                    Password = "admin",
                    RoleName = RoleNames.Administrator,
                    Gender = Genders.Female
                }
            };

            this.Context.Users.AddRange(this.dbUsers);
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task NoFilter_ReturnsUndeletedUsers()
        {
            var expectedResultUsernames = new List<string> { MaleGuestUsername, FemaleGuestUsername, FemaleAdminUsername };

            var result = await this.sut.Handle(new GetAllUsersQuery(), CancellationToken.None).ConfigureAwait(false);
            var returnedUsers = result.Select(u => u.Username);

            Assert.Equal(expectedResultUsernames, returnedUsers);
        }

        [Fact]
        public async Task WhenFilteredByGender_ReturnUndeletedByGender()
        {
            var expectedResultUsernames = new List<string> { FemaleGuestUsername, FemaleAdminUsername };
            var input = new GetAllUsersQuery() { Gender = Genders.Female };

            var result = await this.sut.Handle(input, CancellationToken.None).ConfigureAwait(false);
            var returnedUsers = result.Select(u => u.Username);

            Assert.Equal(expectedResultUsernames, returnedUsers);
        }

        [Fact]
        public async Task WhenFilteredByRoleName_ReturnUndeletedByRoleName()
        {
            var expectedResultUsernames = new List<string> { MaleGuestUsername, FemaleGuestUsername };
            var input = new GetAllUsersQuery() { RoleName = RoleNames.Guest };

            var result = await this.sut.Handle(input, CancellationToken.None).ConfigureAwait(false);
            var returnedUsers = result.Select(u => u.Username);

            Assert.Equal(expectedResultUsernames, returnedUsers);
        }

        [Fact]
        public async Task WhenFilteredByUsername_ReturnUndeletedByUsername()
        {
            var expectedResultUsernames = new List<string> { MaleGuestUsername };
            var input = new GetAllUsersQuery() { Username = MaleGuestUsername };

            var result = await this.sut.Handle(input, CancellationToken.None).ConfigureAwait(false);
            var returnedUsers = result.Select(u => u.Username);

            Assert.Equal(expectedResultUsernames, returnedUsers);
        }

        [Fact]
        public async Task WhenFilteredByGenderAndRoleName_ReturnUndeletedByGenderAndRoleName()
        {
            var expectedResultUsernames = new List<string> { FemaleGuestUsername };
            var input = new GetAllUsersQuery() { Gender = Genders.Female, RoleName = RoleNames.Guest };

            var result = await this.sut.Handle(input, CancellationToken.None).ConfigureAwait(false);
            var returnedUsers = result.Select(u => u.Username);

            Assert.Equal(expectedResultUsernames, returnedUsers);
        }
    }
}