using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common
{
    public class PropertyCollection 
    {
        private bool _IsCaseSensitive;

        private TypedItemCollection _Items = new TypedItemCollection();

        public void Set(string key, object o)
        {
            if (!String.IsNullOrWhiteSpace(key) && o != null)
            {
                string internalKey = _IsCaseSensitive ? key.Trim() :  key.Trim().ToLower();
                _Items.AddItem(internalKey, o);                
            }
        }

        public bool TryGet<T>(string key, out T t, T defaultValue)
        {
            bool b = true;
            t = defaultValue;
            if (!String.IsNullOrWhiteSpace(key))
            {
                string internalKey = _IsCaseSensitive ? key.Trim() : key.Trim().ToLower();
                if (_Items.Contains(internalKey) && _Items[internalKey].Value is T)
                {
                    t = (T)_Items[internalKey].Value;
                    b = true;
                }
            }
            return b;
        }


        public bool TryGet<T>(string key, out T t)
        {            
            bool b = false;
            t = default(T);
            if (!String.IsNullOrWhiteSpace(key))
            {
                string internalKey = _IsCaseSensitive ? key.Trim() : key.Trim().ToLower();
                if (_Items.Contains(internalKey) && _Items[internalKey].Value is T)
                {
                    t = (T)_Items[internalKey].Value;
                    b = true;
                }
            }
            return b;
        }

        public PropertyCollection()
        {

        }
        public PropertyCollection(bool isCaseSensitive = true)
        {
            _IsCaseSensitive = isCaseSensitive;
        }


    }
}
