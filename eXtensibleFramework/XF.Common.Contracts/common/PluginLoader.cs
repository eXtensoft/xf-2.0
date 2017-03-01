// <copyright file="PluginLoader.cs" company="Extensible Solutions LLC">
// Copyright © 2015 All Right Reserved
// </copyright>

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
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var paths = assemblies.Select(x => x.Location).ToArray();
            var refpaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, ".dll");
            var toLoad = refpaths.Where(x => !paths.Contains(x, StringComparer.InvariantCultureIgnoreCase)).ToList();
            foreach (var item in assemblies)
            {
                string[] arr = item.GetName().Name.ToLower().Split('.');
                if (!_CoreDlls.Contains(arr[0]) | arr.Length > 1 && arr[0].Equals("xf", StringComparison.OrdinalIgnoreCase) && arr[1].Equals("bigdata", StringComparison.OrdinalIgnoreCase))
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
