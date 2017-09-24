using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Common;

namespace XF.WebApi.EventWriter.SqlServer
{
    public static class Extensions
    {
        public static string ToSchema(this DateTime now, DateTimeSchemaOption option, string defaultSchema = "log")
        {
            string s = !String.IsNullOrWhiteSpace(defaultSchema) ? defaultSchema.Trim() : "log";

            switch (option)
            {
                case DateTimeSchemaOption.MonthOfYear:
                    s = now.Date.ToString("MMM");
                    break;
                case DateTimeSchemaOption.WeekOfYear:
                    s = now.Date.WeekOfYear().ToString("000");
                    break;
                case DateTimeSchemaOption.DayOfWeek:
                    s = now.DayOfWeek.ToString().Substring(0, 3);
                    break;
                case DateTimeSchemaOption.DayOfYear:
                    s = now.DayOfYear.ToString("000");
                    break;
                case DateTimeSchemaOption.HourOfDay:
                    s = now.ToString("HH");
                    break;
                case DateTimeSchemaOption.HourOfDayOfWeek:
                    s = String.Format("{0}{1}", now.ToString("HH"), (int)now.DayOfWeek);
                    break;
                case DateTimeSchemaOption.None:
                default:
                    break;
            }

            return s.ToLower();
        }
        public static int WeekOfYear(this DateTime now)
        {
            int day = (int)now.DayOfWeek;
            if (--day < 0)
            {
                day = 6;
            }
            int weekNumber = (now.AddDays(3 - day).DayOfYear - 1) / 7 + 1;
            return weekNumber;
        }
    }

}
