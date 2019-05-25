using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Domain.Entities;
using AutoMapper;

namespace ApartmentReservation.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Host, HostDto>();
        }
    }
}