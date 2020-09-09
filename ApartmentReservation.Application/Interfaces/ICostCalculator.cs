using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Application.Interfaces
{
    public class GetTotalCostArgs
    {
        public long ApartmentId { get; set; }

        public int NumberOfNights { get; set; }
        public DateTime StartDate { get; set; }
    }

    public interface ICostCalculator
    {
        Task<double> CalculateTotalCostAsync(GetTotalCostArgs request, CancellationToken cancellationToken);
    }
}
