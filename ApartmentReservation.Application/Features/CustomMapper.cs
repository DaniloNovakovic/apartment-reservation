using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Application.Features
{
    internal static class CustomMapper
    {
        public static void Map(IUser src, IUserRoleLogical dest, bool isDeleted = false)
        {
            dest.User.FirstName = src.FirstName ?? dest.User.FirstName;
            dest.User.LastName = src.LastName ?? dest.User.LastName;
            dest.User.Password = src.Password ?? dest.User.Password;
            dest.User.Gender = src.Gender ?? dest.User.Gender;
            dest.User.RoleName = src.RoleName;
            dest.User.IsDeleted = isDeleted;
            dest.IsDeleted = isDeleted;
        }

        public static void Map(IUser src, IUserRoleLogical dest, string roleName, bool isDeleted = false)
        {
            Map(src, dest, isDeleted);
            dest.User.RoleName = roleName;
        }
    }
}