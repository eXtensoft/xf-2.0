using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common
{
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
