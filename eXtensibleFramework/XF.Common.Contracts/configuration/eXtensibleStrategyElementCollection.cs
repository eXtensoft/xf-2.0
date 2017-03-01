// <copyright company="eXtensible Solutions, LLC" file="eXtensibleStrategyElementCollection.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;
    using System.Configuration;

    public sealed class eXtensibleStrategyElementCollection : ConfigurationElementCollection
    {
        public eXtensibleStrategyElement this[int index]
        {
            get { return base.BaseGet(index) as eXtensibleStrategyElement; }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new eXtensibleStrategyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as eXtensibleStrategyElement).Name;
        }

        public void Add(eXtensibleStrategyElement element)
        {
            base.BaseAdd(element);
        }

        public eXtensibleStrategyElementCollection()
        {
            eXtensibleStrategyElement element = (eXtensibleStrategyElement)CreateNewElement();
            base.BaseAdd(element);
        }

        public List<eXtensibleStrategyElement> GetForStrategyType(StrategyTypeOption option)
        {
            List<eXtensibleStrategyElement> list = new List<eXtensibleStrategyElement>();
            for (int i = 0; i < this.Count; i++)
            {
                eXtensibleStrategyElement strategy = this[i];
                if (strategy.StrategyType.Equals(option))
                {
                    list.Add(strategy);
                }
            }
            return list;
        }
    }
}