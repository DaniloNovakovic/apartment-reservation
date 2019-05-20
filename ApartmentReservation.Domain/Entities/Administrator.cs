using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentReservation.Domain.Entities
{
    public class Administrator
    {
        public string Id { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }
    }
}