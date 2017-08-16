// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    public static class StringExtensionMethods
    {
        public static string Truncate(this string item, int maxLength)
        {
            return item.Truncate(maxLength, false);
        }

        public static string Truncate(this string item, int maxLength, bool useElipses)
        {
            if (!String.IsNullOrEmpty(item))
            {
                string s = item.Trim();
                int max = (s.Length > maxLength) ? maxLength : s.Length;
                if (useElipses)
                {
                    return String.Format("{0}...",s.Substring(0,(max-3)));
                }
                else
                {
                    return s.Substring(0,max);
                }                
            }
            else
            {
                return String.Empty;
            }

        }
    }
}
