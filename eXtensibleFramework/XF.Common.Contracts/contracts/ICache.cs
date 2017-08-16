// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
