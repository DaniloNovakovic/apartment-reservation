using System;

namespace ApartmentReservation.Common
{
    public static class DateTimeHelpers
    {
        public static bool AreSameDay(DateTime firstDate, DateTime secondDate)
        {
            return firstDate.Year == secondDate.Year
                && firstDate.Month == secondDate.Month
                && firstDate.Day == secondDate.Day;
        }

        public static string FormatToYearMonthDayString(DateTime date, char separator = '/')
        {
            int[] arr = new[] { date.Year, date.Month, date.Day };
            return string.Join(separator, arr);
        }
    }
}