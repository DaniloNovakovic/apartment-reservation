﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Guests.Commands
{
    public class CreateGuestCommand : UserDto, IRequest
    {
    }

    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public CreateGuestCommandHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var dbGuest = await context.Guests.Include(g => g.User)
                .SingleOrDefaultAsync(g => g.User.Username == request.Username).ConfigureAwait(false);

            if (dbGuest == null)
            {
                await this.AddNewGuestAsync(request, cancellationToken).ConfigureAwait(false);
            }
            else if (dbGuest.IsDeleted || dbGuest.User.IsDeleted)
            {
                CustomMap(request, dbGuest);
            }
            else
            {
                throw new AlreadyCreatedException($"User '{dbGuest.UserId}' already exists!");
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void CustomMap(CreateGuestCommand src, Guest dest)
        {
            dest.User.FirstName = src.FirstName ?? dest.User.FirstName;
            dest.User.LastName = src.LastName ?? dest.User.LastName;
            dest.User.Password = src.Password ?? dest.User.Password;
            dest.User.Gender = src.Gender ?? dest.User.Gender;
            dest.User.RoleName = RoleNames.Guest;
            dest.User.IsDeleted = false;
            dest.IsDeleted = false;
        }

        private async Task AddNewGuestAsync(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestToAdd = new Guest()
            {
                User = this.mapper.Map<User>(request)
            };

            guestToAdd.User.IsDeleted = false;
            guestToAdd.IsDeleted = false;
            guestToAdd.User.RoleName = RoleNames.Guest;

            await this.context.Guests.AddAsync(guestToAdd, cancellationToken).ConfigureAwait(false);
        }
    }
}