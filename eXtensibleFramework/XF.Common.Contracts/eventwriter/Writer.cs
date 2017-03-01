// <copyright company="eXtensible Solutions LLC" file="WriterBase.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;

    public static class Writer
    {

        internal static IDictionary<string, object> EnsureProperties(IDictionary<string, object> properties)
        {
            if (properties == null)
            {
                properties = eXtensibleConfig.GetProperties();
                //properties = new Dictionary<string, object>();
            }
            return properties;
        }

        internal static List<TypedItem> Convert(IDictionary<string, object> properties)
        {
            List<TypedItem> list = new List<TypedItem>();
            HashSet<string> hs = new HashSet<string>();
            foreach (var prop in properties)
            {
                string key = prop.Key;
                if (hs.Add(key))
                {
                    list.Add(new TypedItem(key, prop.Value));
                }                
            }
            foreach (var prop in eXtensibleConfig.GetProperties())
            {
                if (hs.Add(prop.Key))
                {
                    list.Add(new TypedItem(prop.Key, prop.Value));
                }
            }
            return list;
        }
    }
}
