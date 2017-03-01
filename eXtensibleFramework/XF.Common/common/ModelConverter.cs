// <copyright company="eXtensible Solutions, LLC" file="ModelConverter.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ModelConverter
    {
        public static U ConvertTo<T, U>(T t) 
            where U : class, new() 
            where T : class, new()
        {
            U u = new U();
            foreach (var map in ResolveProperties<T,U>())
            {
                PropertyInfo tinfo = map.Item1;
                PropertyInfo uinfo = map.Item2;
                var sourceValue = tinfo.GetValue(t, null);
                uinfo.SetValue(u, sourceValue, null);
            }
            return u;
        }

        private static IList<Tuple<PropertyInfo, PropertyInfo>> ResolveProperties<T, U>()
            where U : class, new()
            where T : class, new()
        {
            return (from t in typeof(T).GetProperties()
                     from u in typeof(U).GetProperties()
                     where t.Name == u.Name &&
                     t.CanRead && u.CanRead &&
                     t.PropertyType.IsPublic && u.PropertyType.IsPublic &&
                     t.PropertyType == t.PropertyType && 
                    (
                        (t.PropertyType.IsValueType && u.PropertyType.IsValueType) ||
                        (t.PropertyType == typeof(string) && u.PropertyType == typeof(string))
                    )
                     select new Tuple<PropertyInfo,PropertyInfo>(t,u)
                     ).ToList();
        }
    }

    //public static class Ext
    //{
    //    public static T To<T>(this IDictionary<string,object> d) where T : class, new()
    //    {
    //        T t = new T();
    //        string fullName = t.GetType().FullName;
    //        string name = t.GetType().Name;
    //        var properties = t.GetType().GetProperties();
    //        foreach (PropertyInfo propInfo in properties)
    //        {
    //            string n = propInfo.Name;
    //            if (d.ContainsKey(n))
    //            {
    //                object o = d[n];
    //                if (propInfo.PropertyType.IsPublic && propInfo.CanWrite && propInfo.PropertyType.Equals(typeof(object)))
    //                {
    //                    propInfo.SetValue(t, o);
    //                }
    //            }
    //        }

    //        return t;
    //    }

    //    public static T From<T>(this List<TypedItem> list) where T : class, new()
    //    {
    //        T t = new T();
    //        foreach (var info in t.GetType().GetProperties())
    //        {
    //            string n = info.Name;
    //            if (list.ContainsKey<string>(n))
    //            {
    //                info.SetValue(t, list.Get(n));
    //                list.Get<string>(n);
    //            }
    //        }
    //        return t;
    //    }

    //    public static List<TypedItem> ToTypedItems<T>(this T t) where T : class
    //    {
    //        List<TypedItem> list = new List<TypedItem>();
    //        string fullName = t.GetType().FullName;

    //        foreach (var info in t.GetType().GetProperties())
    //        {
    //            if (info.PropertyType.IsPublic && info.CanRead)
    //            {
    //                if ((info.PropertyType.IsValueType || info.PropertyType == typeof(string)))
    //                {
    //                    TypedItem item = new TypedItem(info.Name, info.GetValue(t));
    //                    list.Add(item);
    //                }
    //            }
    //        }
    //        return list;
    //    }
    //}


}
