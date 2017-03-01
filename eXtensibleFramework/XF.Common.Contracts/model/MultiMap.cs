// <copyright company="eXtensoft, LLC" file="MultiMap.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

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
