using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentReservation.Application.Dtos
{
    public class HostDto
    {
        public string Id { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }
    }
}