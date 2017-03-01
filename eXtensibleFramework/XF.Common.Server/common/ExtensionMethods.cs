// <copyright company="Extensible Solutions LLC" file="ExtensionMethods.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    public static class ExtensionMethods
    {



        public static string ToShortString(this ICriterion criterion)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();
            if (criterion != null && criterion.Items != null)
            {
                foreach (var item in criterion.Items)
                {
                    if (i++ > 0)
                    {
                        sb.Append(";");
                    }
                    sb.AppendFormat("{0}:{1}",item.Key, item.Value.ToString());
                }                
            }
            else
            {
                sb.Append("ICriterion={x:Null}");
            }
            return sb.ToString();
        }
        public static List<string> ToListOfString(this string target)
        {
            string[] t = target.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>(t);
            return list;
        }


    }

}
