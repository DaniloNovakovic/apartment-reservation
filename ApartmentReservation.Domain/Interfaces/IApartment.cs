using System.Collections.Generic;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Domain.Interfaces
{
    public interface IApartment
    {
        long Id { get; set; }
        string ActivityState { get; set; }
        IEnumerable<Amenity> Amenities { get; }
        string ApartmentType { get; set; }
        string CheckInTime { get; set; }
        string CheckOutTime { get; set; }
        ICollection<Comment> Comments { get; set; }
        ICollection<ForRentalDate> ForRentalDates { get; set; }
        Host Host { get; set; }
        ICollection<Image> Images { get; set; }
        Location Location { get; set; }
        int NumberOfGuests { get; set; }
        int NumberOfRooms { get; set; }
        double PricePerNight { get; set; }
        ICollection<Reservation> Reservations { get; set; }
        string Title { get; set; }
    }
}