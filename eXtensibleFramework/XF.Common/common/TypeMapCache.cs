// <copyright company="eXtensible Solutions, LLC" file="TypeMapCache.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TypeMapCache : ITypeMapCache
    {
        private List<string> folderpaths;

        private volatile Dictionary<string, List<Type>> _TypeCache;

        private object _lockObject = new object();

        internal int Count
        {
            get
            {
                return _TypeCache.Count;
            }
        }


        void ITypeMapCache.Initialize()
        {
            Initialize();
        }

        public TypeMapCache() { }

        public TypeMapCache(IEnumerable<string> folderPaths)
        {
            folderpaths = folderPaths.ToList();
        }

        Type ITypeMapCache.ResolveType<T>()
        {
            return this.ResoveType<T>();
        }

        Type ITypeMapCache.ResolveType(string key)
        {
            return ResolveType(key);
        }

        private void Initialize()
        {
            if (_TypeCache == null)
            {
                lock (_lockObject)
                {
                    if (_TypeCache == null)
                    {
                        DiscoverTypes();
                    }
                }
            }
        }

        private void DiscoverTypes()
        {
            if (eXtensibleConfig.Inform)
            {
                EventWriter.Inform(String.Format("{0}|{1}|Typecache is discovering pluggable types...",System.AppDomain.CurrentDomain.FriendlyName,"type-cache"));
            }
            TypeMapContainer container = null;
            ModuleLoader<TypeMapContainer> loader = new ModuleLoader<TypeMapContainer>();
            if (folderpaths != null && folderpaths.Count > 0)
            {
                loader.Folderpaths = folderpaths;
            }
            _TypeCache = new Dictionary<string, List<Type>>();
            if (loader.Load(out container))
            {                
                foreach (var item in container.TypeMaps)
                {
                    string key = String.IsNullOrWhiteSpace(item.Domain) ? item.KeyType.FullName : item.Domain;

                    if (!_TypeCache.ContainsKey(key))
                    {
                        _TypeCache.Add(key, new List<Type>());
                    }

                    _TypeCache[key].Add(item.TypeResolution);

                    if (eXtensibleConfig.Inform && !item.TypeResolution.ToString().Contains("XF."))
                    {
                        string name = System.AppDomain.CurrentDomain.FriendlyName;
                        string message = String.Format("{0}|{1}|Typecache associated {2} with {3}",name,"type-cache", key, item.TypeResolution);
                        EventWriter.Inform(message);
                    }
                }
            }            
        }

        private Type ResoveType<T>() where T : class, new()
        {
            string modeltypename = Activator.CreateInstance<T>().GetType().FullName;
            return ResolveType(modeltypename);
        }

        private Type ResolveType(string key)
        {
            if (_TypeCache.ContainsKey(key))
            {
                return _TypeCache[key][0];
            }
            else
            {
                return null;
            }
        }

    }
}
