using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentReservation.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public Apartment Apartment { get; set; }

        public string ApartmentId { get; set; }

        public Guest Guest { get; set; }

        public string GuestId { get; set; }

        public byte Rating { get; set; }

        public string Text { get; set; }
    }
}