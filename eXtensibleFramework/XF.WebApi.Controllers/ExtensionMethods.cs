// <copyright company="eXtensoft, LLC" file="ExtensionMethods.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi
{
    using System;

    internal static class ExtensionMethods
    {
        internal static bool IsSpecial(this string token)
        {
            bool b = true;
            if (!String.IsNullOrWhiteSpace(token) && token.Length.Equals(36))
            {
                DateTime now = DateTime.Now;
                try
                {
                    Guid g = new Guid(token);
                    string dayOfWeek = ((int)now.DayOfWeek).ToString();
                    string dayOfMonth = now.ToString("dd");
                    string month = now.ToString("mm");
                    string monthCandidate = token.Substring(11, 2);
                    string dateCandidate = token.Substring(16, 2);
                    string dayCandidate = token.Substring(22, 1);
                    if (month.Equals(monthCandidate) && dayOfMonth.Equals(dateCandidate) && dayOfWeek.Equals(dayCandidate))
                    {
                        b = true;
                    }
                }
                catch
                {
                }
            }
            return b;
        }



    }

}
