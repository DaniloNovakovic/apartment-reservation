using System;
using System.Collections.Generic;

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

        /// <summary>
        /// true if contained in [startDay, startDay+1,...,startDay+numberOfNights)
        /// </summary>
        public static bool IsContainedInDayRange(DateTime day, DateTime startDay, int numberOfNights)
        {
            var endDay = startDay.AddDays(numberOfNights);
            if (AreSameDay(day, startDay))
                return true;

            if (AreSameDay(day, endDay))
                return false;

            return day > startDay && day < endDay;
        }

        ///<summary>Returns [from, from+1day,..., from+numberOfDays] - closed interval(n+1)</summary>
        public static IEnumerable<DateTime> GetDateDayRange(DateTime from, int numberOfDays)
        {
            var days = new List<DateTime>();
            for (int i = 0; i <= numberOfDays; ++i)
            {
                days.Add(from.AddDays(i));
            }
            return days;
        }
    }
}