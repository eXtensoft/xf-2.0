// <copyright company="eXtensible Solutions, LLC" file="Criterion.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
   
    public class Criterion : ICriterion
    {
        #region constructors

        public Criterion()
        {
            Items = new List<TypedItem>();
        }

        public Criterion(int value)
        {
            Items = new List<TypedItem>() { new TypedItem("Id", value) };
        }

        public Criterion(string value)
        {
            Items = new List<TypedItem>() { new TypedItem("Id", value) };
        }

        public Criterion(string key, int value)
        {
            Items = new List<TypedItem>() { new TypedItem(key, value) };
        }

        public Criterion(object value)
        {
            Items = new List<TypedItem>() { new TypedItem("Id", value) };
        }

        public Criterion(string key, object value)
        {
            Items = new List<TypedItem>() { new TypedItem(key, value) };
        }

        public Criterion(string key, object value, OperatorTypeOption option)
        {
            Items = new List<TypedItem>(){ new TypedItem(key,value){ Operator = option }};
        }

        #endregion constructors

        public T GetValue<T>(string key)
        {
            T t = default(T);
            bool b = false;

            if (Items != null)
            {
                List<TypedItem> items = Items.ToList();
                for (int i = 0; !b && i < items.Count; i++)
                {
                    if (items[i].Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        TypedItem item = Items.First(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

                        if (item != null)
                        {
                            try
                            {
                                t = (T)item.Value;
                            }
                            catch (Exception ex)
                            {
                                string s = ex.Message;
                            }
                        }
                        b = true;
                    }
                }
            }
            return t;           
        }

        public object GetValue(string key)
        {
            bool b = false;
            object o = null;

            if (Items != null)
            {
                List<TypedItem> items = Items.ToList();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        TypedItem item = Items.First(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

                        if (item != null)
                        {
                            try
                            {
                                o = item.Value;
                            }
                            catch (Exception ex)
                            {
                                string s = ex.Message;
                            }
                        }
                        b = true;
                    }                    
                }
            }

            return b;
        }

        private List<TypedItem> _Items = new List<TypedItem>();
        [DataMember]
        public IEnumerable<TypedItem> Items
        {
            get { return _Items; }
            set { _Items = value.ToList(); }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            for (int i = 0; i < _Items.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(";");
                }
                sb.Append(_Items[i].ToString());
            }
            sb.Append("]");
            return sb.ToString();
        }

        public void Add(string key, object value)
        {
            Add(key, value, OperatorTypeOption.EqualTo);
        }

        public void Add(string key, object value, OperatorTypeOption option)
        {
            if (_Items == null)
            {
                _Items = new List<TypedItem>();
            }
            _Items.Add(new TypedItem(key, value,option));
        }



        public static ICriterion GenerateStrategy(string switchKey)
        {
            Criterion criteria = new Criterion();
            if (criteria != null)
            {
                criteria.AddItem(XFConstants.Application.StrategyKey, switchKey);
            }
            return criteria as ICriterion;
        }
   
    }
}
