// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    public sealed class Infoset
    {
        [XmlElement("Item")]
        [DataMember]
        public List<TypedItem> Items { get; set; }

        #region constructors

        public Infoset()
        {
            Items = new List<TypedItem>();
        }

        public Infoset(string key, object value)
        {
            Items = new List<TypedItem>() { new TypedItem(key, value) };
        }

        #endregion constructors

        public void AddItem(string key, object value)
        {
            if (Items == null)
            {
                Items = new List<TypedItem>();
            }
            Items.Add(new TypedItem(key, value));
        }

        public T GetValue<T>(string key)
        {
            T t = default(T);
            bool b = false;

            if (Items != null)
            {
                for (int i = 0; !b && i < Items.Count; i++)
                {
                    if (Items[i].Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        TypedItem item = Items.First(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

                        if (item != null)
                        {
                            try
                            {
                                t = (T)item.Value;
                            }
                            catch
                            { }
                        }
                        b = true;
                    }
                }
            }
            return t;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Items == null)
            {
                sb.AppendLine("\tInfoset:Empty");
            }
            else
            {
                sb.AppendLine("\tInfoset:");
                foreach (var item in Items)
                {
                    sb.AppendLine(String.Format("\t\t{0}", item.ToString()));
                }
            }
            return sb.ToString();
        }
    }
}
