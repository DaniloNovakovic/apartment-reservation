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
            this.AddGlobalIgnore("IsDeleted");

            this.CreateAmenityMaps();
            this.CreateGuestMaps();
            this.CreateHostMaps();
            this.CreateUserMaps();
        }

        private void CreateAmenityMaps()
        {
            this.CreateMap<Amenity, AmenityDto>();
        }

        private void CreateGuestMaps()
        {
            this.CreateMap<Guest, GuestDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.User.Password))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.User.RoleName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id));

            this.CreateMap<CreateGuestCommand, User>();
        }

        private void CreateHostMaps()
        {
            this.CreateMap<Host, HostDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.User.Password))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.User.RoleName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id));

            this.CreateMap<CreateHostCommand, User>();
        }

        private void CreateUserMaps()
        {
            this.CreateMap<User, UserDto>();
            this.CreateMap<UserDto, User>();
        }
    }
}