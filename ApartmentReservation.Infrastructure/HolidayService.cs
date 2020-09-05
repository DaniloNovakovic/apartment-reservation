using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;

namespace ApartmentReservation.Infrastructure
{
    public class Holiday : IHoliday
    {
        public int Day { get; set; }
        public int Month { get; set; }
    }

    public class HolidayService : IHolidayService
    {
        private readonly IHostingEnvironment env;

        public HolidayService(IHostingEnvironment env)
        {
            this.env = env;
        }

        public async Task<IEnumerable<IHoliday>> GetHolidaysAsync(CancellationToken cancellationToken = default)
        {
            string jsonFilePath = Path.Combine(this.env.WebRootPath, "holidays.json");
            if (!File.Exists(jsonFilePath))
            {
                Debug.WriteLine($"File on path `{jsonFilePath}` does not exist!");
                return new List<IHoliday>();
            }

            string jsonText = await File.ReadAllTextAsync(jsonFilePath, cancellationToken).ConfigureAwait(false);

            var jsonData = JObject.Parse(jsonText);
            var holidayJTokens = jsonData["holidays"].Children().ToList();

            return holidayJTokens.Select(jt => jt.ToObject<Holiday>()).ToList();
        }
    }
}