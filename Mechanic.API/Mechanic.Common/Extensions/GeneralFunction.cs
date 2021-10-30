using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanic.Common.Extensions
{
    public static class GeneralFunction
    {
        public static decimal ConvertDistanceToMiles(decimal distance, decimal distanceFactor)
        {
            return distance / distanceFactor;
        }

        public static DateTime ConvertToLocalTime(DateTime date, int timeZoneOffset, decimal serverTimeZone, decimal? dayLightSavingOffset = 1)
        {
            var currentDate = DateTime.Now;
            DateTime dayLightSavingStart = new DateTime(currentDate.Year, 3, 1);
            DateTime dayLightSavingEnd = new DateTime(currentDate.Year, 11, 1);
            dayLightSavingStart = dayLightSavingStart.AddDays(15 - Convert.ToDouble(dayLightSavingStart.DayOfWeek));
            dayLightSavingEnd = dayLightSavingEnd.AddDays(8 - Convert.ToDouble(dayLightSavingEnd.DayOfWeek));
            if ((currentDate >= dayLightSavingStart && currentDate <= dayLightSavingEnd && dayLightSavingOffset == 0))
                return date.AddHours(Convert.ToDouble(timeZoneOffset) - Convert.ToDouble(serverTimeZone) - 1);

            return date.AddHours(Convert.ToDouble(timeZoneOffset) - Convert.ToDouble(serverTimeZone));

        }
    }
}
