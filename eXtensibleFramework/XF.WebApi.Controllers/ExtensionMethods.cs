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
            }
            return b;
        }



    }


}
