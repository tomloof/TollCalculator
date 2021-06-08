using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nager.Date;

namespace TollCalculator.Handler
{
    public static class TollCalculatorHandler
    {
        public static Boolean IsTollFeeDate(DateTime date)
        {
            if (DateSystem.IsPublicHoliday(date, CountryCode.SE) || DateSystem.IsWeekend(date, CountryCode.SE))
            {
                return false;
            }
            return true;
        }

        public static int GetTollFee(DateTime date)
        {
            int hour = date.Hour;
            int minute = date.Minute;

            if ((hour == 6 && minute >= 0 && minute <= 29) ||
               (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) ||
               (hour == 18 && minute >= 0 && minute <= 29))
            {
                return 8;
            }
            else if ((hour == 6 && minute >= 30 && minute <= 59) ||
                (hour == 8 && minute >= 0 && minute <= 29) ||
                (hour == 15 && minute >= 0 && minute <= 29) ||
                (hour == 17 && minute >= 0 && minute <= 59))
            {
                return 13;
            }
            else if ((hour == 7 && minute >= 0 && minute <= 59) ||
                (hour == 15 && minute >= 0 || hour == 16 && minute <= 59))
            {
                return 18;
            }
            else
                return 0;
        }
    }
}
