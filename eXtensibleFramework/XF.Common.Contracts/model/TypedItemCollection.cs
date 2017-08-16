// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Collections.ObjectModel;

    internal class TypedItemCollection : KeyedCollection<string,TypedItem>
    {
        protected override string GetKeyForItem(TypedItem item)
        {
            return item.Key;
        }

        public void AddItem(string key, object o)
        {
            if (Contains(key))
            {
                Remove(key);
            }
            Add(new TypedItem(key, o));
        }

    }
}
