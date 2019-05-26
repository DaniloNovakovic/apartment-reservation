using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{

    internal class HostRole : Role
    {
        private IUnitOfWork unitOfWork;

        public HostRole(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override IEnumerable<Claim> GenerateClaims(User user)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<User> GetUser(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}