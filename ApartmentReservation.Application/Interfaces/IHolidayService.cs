using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Application.Interfaces
{
    public interface IHolidayService
    {
        Task<IEnumerable<IHoliday>> GetHolidaysAsync(CancellationToken cancellationToken = default);
    }
}