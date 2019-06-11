using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Application
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

        public static TDestination Map<TDestination>(object source) where TDestination : new()
        {
            var destination = new TDestination();

            Map(source, destination);

            return destination;
        }

        public static void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            foreach (var srcProp in source.GetType().GetProperties())
            {
                var destProp = destination.GetType().GetProperty(srcProp.Name);
                if (destProp == null)
                {
                    continue;
                }
                destProp.SetValue(destination, srcProp.GetValue(source));
            }
        }
    }
}