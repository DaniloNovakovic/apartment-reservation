using ApartmentReservation.Application.Features.Reservations.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Application.Interfaces
{
    public interface ICostCalculator
    {
        Task<double> CalculateTotalCostAsync(GetTotalCostQuery request, CancellationToken cancellationToken);
    }
}
