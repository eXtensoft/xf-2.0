

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class eXtensibleIdentity
    {
        public string Key { get; set; }


        private List<TypedItem> _Items = new List<TypedItem>();

        public bool TryGetValue<T>(string key, out T t)
        {
            bool b = false;
            t = default(T);
            for (int i = 0; !b && i < _Items.Count; i++)
            {
                if (_Items[i].Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    TypedItem item = _Items[i];// _Items.First(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
                    try
                    {
                        t = (T)item.Value;
                        b = true;
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                    }
                }
            }
            return b;
        }

        public void SetValue(string key, object o)
        {
            var found = _Items.Find(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (found != null)
            {
                _Items.Remove(found);

            }
            _Items.Add(new TypedItem(key, o));
        }

    }
}
