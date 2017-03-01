using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common
{
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
