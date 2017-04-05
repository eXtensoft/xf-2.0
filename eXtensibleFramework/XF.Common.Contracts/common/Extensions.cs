namespace XF.Common.Special
{
    using System;
    public static class Extensions
    {
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
