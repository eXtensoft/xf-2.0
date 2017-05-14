namespace XF.Common.Special
{
    using System;
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
                    s = now.DayOfWeek.ToString().Substring(0,3);
                    break;
                case DateTimeSchemaOption.DayOfYear:
                    s = now.DayOfYear.ToString("000");
                    break;
                case DateTimeSchemaOption.HourOfDay:
                    s = now.Hour.ToString("HH");
                    break;
                case DateTimeSchemaOption.HourOfDayOfWeek:
                    s = String.Format("{0}{1}", (int)now.DayOfWeek, now.Hour.ToString("HH"));
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
        public static Guid ToSpecial(this Guid guid)
        {
            DateTime now = DateTime.Now;
            string token = guid.ToString();
            string s = String.Format("{0}{1}{2}{3}{4}{5}{6}",
                                    token.Substring(0, 11),
                                    now.ToString("MM"),
                                    token.Substring(13, 3),
                                    now.ToString("dd"),
                                    token.Substring(18, 4),
                                    (int)now.DayOfWeek,
                                    token.Substring(23, 13));
            return new Guid(s);
        }

        public static bool IsSpecial(this Guid guid)
        {
            bool b = false;
            string token = guid.ToString();
            DateTime now = DateTime.Now;

            try
            {
                Guid g = new Guid(token);
                string dayOfWeek = ((int)now.DayOfWeek).ToString();
                string dayOfMonth = now.ToString("dd");
                string month = now.ToString("MM");
                string monthCandidate = token.Substring(11, 2);
                string dateCandidate = token.Substring(16, 2);
                string dayCandidate = token.Substring(22, 1);
                b = (b == false) ? b : month.Equals(monthCandidate);
                b = (b == false) ? b : dayOfMonth.Equals(dateCandidate);
                b = (b == false) ? b : dayOfWeek.Equals(dayCandidate);
            }
            catch
            {
                b = false;
            }

            return b;
        }
    }


}
