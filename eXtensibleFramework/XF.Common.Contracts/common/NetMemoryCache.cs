// <copyright company="eXtensible Solutions, LLC" file="NetMemoryCache.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;

    public sealed class NetMemoryCache : ICache
    {
        #region local fields

        private MemoryCache _Cache;

        private int slidingMinutes = 5;

        #endregion local fields

        #region constructors

        public NetMemoryCache()
        {
            slidingMinutes = 5;
            _Cache = MemoryCache.Default;
        }

        public NetMemoryCache(int defaultSlidingMinutes)
        {
            if (defaultSlidingMinutes > slidingMinutes)
            {
                slidingMinutes = defaultSlidingMinutes;
            }
            _Cache = MemoryCache.Default;
        }

        #endregion constructors

        #region base class overrides

        public bool Store(string key, object item)
        {
            return _Cache.Add(new CacheItem(key, item), new CacheItemPolicy()
            {
                Priority = CacheItemPriority.Default,
                SlidingExpiration = TimeSpan.FromMinutes(slidingMinutes)
            });
        }

        public bool Store(string key, object item, TimeSpan timeSpan)
        {
            return _Cache.Add(new CacheItem(key, item), new CacheItemPolicy()
            {
                Priority = CacheItemPriority.Default,
                SlidingExpiration = timeSpan
            });
        }

        public object Retrieve(string key)
        {
            return LocalRetrieve(key);
        }

        public T Retrieve<T>(string key)
        {
            object o = _Cache.Get(key);
            if (o is T)
            {
                return (T)o;
            }
            else
            {
                return default(T);
            }
        }

        public IDictionary<string, object> Retrieve(params string[] keys)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (string key in keys)
            {
                object o = LocalRetrieve(key);
            }
            return d;
        }

        public IEnumerable<T> RetrieveSet<T>(string key)
        {
            List<T> list = new List<T>();

            List<string> setOfKeys = Retrieve<List<string>>(key);
            if (setOfKeys != null && setOfKeys.Count > 0)
            {
                var result = Retrieve(setOfKeys.ToArray());
                foreach (var item in result.Values)
                {
                    list.Add((T)item);
                }
            }
            return list;
        }

        public void ClearItem(string key)
        {
            _Cache.Remove(key);
        }

        public void ClearAll()
        {
            
        }

        #endregion base class overrides

        #region helper methods

        private object LocalRetrieve(string key)
        {
            MemoryCache cache = MemoryCache.Default;
            return cache.Get(key);
        }

        #endregion helper methods

        bool ICache.Store(string key, object item)
        {
            return Store(key, item);
        }

        bool ICache.Store(string key, object item, TimeSpan timeSpan)
        {
            return Store(key, item, timeSpan);
        }

        object ICache.Retrieve(string key)
        {
            return Retrieve(key);
        }

        T ICache.Retrieve<T>(string key)
        {
            return Retrieve<T>(key);
        }

        IEnumerable<T> ICache.Retrieve<T>(params string[] keys)
        {
            List<T> list = new List<T>();
            foreach (string key in keys)
            {
                var found = Retrieve<T>(key);
                if (found != null)
                {
                    list.Add(found);
                }
            }
            return list;
        }

        IDictionary<string, object> ICache.Retrieve(params string[] keys)
        {
            throw new NotImplementedException();
        }

        IEnumerable<T> ICache.RetrieveSet<T>(string key)
        {
            return RetrieveSet<T>(key);
        }

        void ICache.ClearItem(string key)
        {
            ClearItem(key);
        }

        void ICache.ClearAll()
        {
            ClearAll();
        }
    }
}
