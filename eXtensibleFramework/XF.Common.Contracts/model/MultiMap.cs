// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MultiMap
    {
        public Type T { get; private set; }

        public IEnumerable<string> Keys { get; private set; }

        public string Key { get; private set; }
        public MultiMap(Type type, string key, params string[] keys)
        {
            T = type;
            Key = key;
            Keys = new List<string>(keys);
        }
    }

}
