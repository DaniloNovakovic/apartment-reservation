namespace ApartmentReservation.Common.Constants
{
    public enum ReservationState
    {
        Created,
        Denied,
        Withdrawn,
        Accepted,
        Completed
    }

    public static class ReservationStates
    {
        public const string Created = "Created";
        public const string Denied = "Denied";
        public const string Withdrawn = "Withdrawn";
        public const string Accepted = "Accepted";
        public const string Completed = "Completed";
    }
}