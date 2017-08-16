// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
