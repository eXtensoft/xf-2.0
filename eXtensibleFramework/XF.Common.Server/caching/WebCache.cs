// <copyright company="eXtensible Solutions LLC" file="WebCache.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Caching;

    public class WebCache : ICache
    {
        private System.Web.Caching.Cache _Cache;

        #region constructors

        public WebCache()
        {
            _Cache = (HttpContext.Current == null) ? HttpRuntime.Cache : HttpContext.Current.Cache;
        }

        #endregion


        #region interface implementations

        bool ICache.Store(string key, object item)
        {
            DateTime expiry = DateTime.Now.AddHours(1);
            return LocalStore(key, item, expiry);
        }

        bool ICache.Store(string key, object item, TimeSpan timeSpan)
        {
            DateTime expiry = DateTime.Now.AddMilliseconds(timeSpan.TotalMilliseconds);
            return LocalStore(key, item, expiry);
        }

        object ICache.Retrieve(string key)
        {
            return LocalRetrieve(key);
        }

        T ICache.Retrieve<T>(string key)
        {
            return LocalRetrieve<T>(key);
        }

        IDictionary<string, object> ICache.Retrieve(params string[] keys)
        {
            return LocalRetrieve(keys);
        }

        IEnumerable<T> ICache.Retrieve<T>(params string[] keys)
        {
            List<T> list = new List<T>();
            foreach (var key in keys)
            {
                list.Add(LocalRetrieve<T>(key));
            }
            return list;
        }

        IEnumerable<T> ICache.RetrieveSet<T>(string key)
        {
            throw new NotImplementedException();
        }

        void ICache.ClearItem(string key)
        {
            LocalClearItem(key);
        }

        void ICache.ClearAll()
        {
            foreach (DictionaryEntry item in _Cache)
            {
                LocalClearItem(item.Key.ToString());
            }
        }

        #endregion


        #region helper methods

        private IDictionary<string,object> LocalRetrieve(params string[] keys)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (string key in keys)
            {

                if (!String.IsNullOrWhiteSpace(key))
                {
                    object o = _Cache.Get(key);
                    if (o != null)
                    {
                        d.Add(key, o);
                    }
                }
            }
            return d;
        }

        private IEnumerable<T> LocalRetrieveSet<T>(string key)
        {
            List<T> list = new List<T>();
            if (!String.IsNullOrWhiteSpace(key))
            {
                var found = _Cache.Get(key);
                if (found is List<T>)
                {
                    list = (List<T>)found;
                }
            }
            return list;            
        }

        private void LocalClearItem(string key)
        {
            if (!String.IsNullOrWhiteSpace(key))
            {
                _Cache.Remove(key);
            } 
        }

        private bool LocalStore(string key, object item, DateTime expiry)
        {           
            bool b = false;
            if (!String.IsNullOrEmpty(key) && item != null)
            {
                _Cache.Add(key, item, null, expiry, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                b = true;
            }
            return b;
        }

        private object LocalRetrieve(string key)
        {
            object o = null;
            if (!String.IsNullOrWhiteSpace(key))
            {
                o = _Cache.Get(key);
            }
            return o;
        }
        private T LocalRetrieve<T>(string key)
        {
            T t = default(T);
            if (!String.IsNullOrWhiteSpace(key))
            {
                var found = _Cache.Get(key);
                if (found is T)
                {
                    t = (T)found;
                }
            }
            return t;
        }

        private bool TryGetFromContext<T>(string key, out T t)
        {
            bool b = false;
            t = default(T);
            if(HttpContext.Current != null && HttpContext.Current.Items != null && HttpContext.Current.Items.Contains(key))
            {
                var found = HttpContext.Current.Items[key];
                if (found is T)
                {
                    t = (T)found;
                    b = true;
                }
            }
            return b;
        }



        #endregion




    }
}
