using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Domain.Entities;
using AutoMapper;

namespace ApartmentReservation.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Host, HostDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.User.Password))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id));

            CreateMap<CreateHostCommand, User>();
            CreateMap<CreateGuestCommand, User>();
        }
    }
}