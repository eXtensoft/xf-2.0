// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    public static class PluginLoader
    {
        private static IList<string> _CoreDlls = new List<string>()
        {
            "mscorlib",
            "vshost32",
            "presentationcore",
            "presentationframework",
            "microsoft",
            "windowsbase",
            "windows",
            "system",
            "xf",
        };

        public static IEnumerable<T> LoadReferencedAssembly<T>()
        {
            List<T> list = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic).ToList();
            foreach (var item in assemblies)
            {
                string[] arr = item.GetName().Name.ToLower().Split('.');
                string x = arr[0];
                if (!_CoreDlls.Contains(arr[0]))
                {
                    try
                    {
                        Assembly assembly = Assembly.Load(item.FullName);
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (typeof(T).IsAssignableFrom(type) && type.GetConstructor(Type.EmptyTypes) != null)
                            {
                                try
                                {
                                    T t = (T)Activator.CreateInstance(type);
                                    list.Add(t);
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return list;
        }

        public static IEnumerable<T> LoadPlugin<T>()
        {
            return null;
        }

    }

}
