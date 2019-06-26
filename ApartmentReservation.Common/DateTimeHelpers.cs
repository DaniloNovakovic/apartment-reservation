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

        public static IEnumerable<DateTime> GetDateDayRange(DateTime fromDate, DateTime toDate)
        {
            var daysRange = new List<DateTime>();
            for (var currDate = fromDate; !IsDayAfter(currDate, toDate); currDate = currDate.AddDays(1))
            {
                daysRange.Add(currDate);
            }
            return daysRange;
        }

        public static bool IsBeforeToday(DateTime date)
        {
            return IsDayBefore(date, DateTime.Now);
        }

        public static bool IsAfterToday(DateTime date)
        {
            return IsDayAfter(date, DateTime.Now);
        }

        public static bool IsDayBefore(DateTime day, DateTime beforeWhen)
        {
            return !AreSameDay(day, beforeWhen) && day < beforeWhen;
        }

        public static bool IsDayAfter(DateTime day, DateTime afterWhen)
        {
            return !AreSameDay(day, afterWhen) && day > afterWhen;
        }

        public static DateTime StartOfWeek(DateTime date, DayOfWeek startOfWeek)
        {
            int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday
                || date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}