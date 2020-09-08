﻿namespace ApartmentReservation.Application.Dtos
{
    public class CommentDto
    {
        public long Id { get; set; }

        public long GuestId { get; set; }
        public string GuestUsername { get; set; }

        public byte Rating { get; set; }

        public string Text { get; set; }

        public bool Approved { get; set; }
    }
}