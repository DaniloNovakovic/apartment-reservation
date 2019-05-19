using System;
using System.Collections.Generic;
using System.Text;
using ApartmentReservation.Domain.Enumerations;

namespace ApartmentReservation.Domain.Entities
{
    public class User
    {
        public string UserId { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public RoleType Role { get; set; }
    }
}