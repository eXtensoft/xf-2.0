// <copyright company="eXtensible Solutions, LLC" file="ICache.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

    public interface ICache
    {
        bool Store(string key, object item);

        bool Store(string key, object item, TimeSpan timeSpan);

        object Retrieve(string key);

        T Retrieve<T>(string key);

        IEnumerable<T> Retrieve<T>(params string[] keys);

        IDictionary<string, object> Retrieve(params string[] keys);

        IEnumerable<T> RetrieveSet<T>(string key);

        void ClearItem(string key);

        void ClearAll();
    }
}
