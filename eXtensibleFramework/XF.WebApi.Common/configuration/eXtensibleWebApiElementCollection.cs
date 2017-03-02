// <copyright company="Recorded Books, Inc" file="eXtensibleWebApiElementCollection.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi.Config
{
    using System;
    using System.Configuration;
    using XF.Common;

    public sealed class eXtensibleWebApiElementCollection : ConfigurationElementCollection
    {
        public eXtensibleWebApiElement this[int index]
        {
            get { return base.BaseGet(index) as eXtensibleWebApiElement; }
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
            return new eXtensibleWebApiElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as eXtensibleWebApiElement).Key;
        }

        public void Add(eXtensibleWebApiElement element)
        {
            base.BaseAdd(element);
        }

        public eXtensibleWebApiElementCollection()
        {
            eXtensibleWebApiElement element = (eXtensibleWebApiElement)CreateNewElement();
            base.BaseAdd(element);
        }

        public eXtensibleWebApiElement GetForLoggingMode(string loggingModeKey)
        {
            bool b = false;
            eXtensibleWebApiElement found = null;
            for (int i = 0; !b && i < this.Count; i++)
            {
                eXtensibleWebApiElement element = this[i];
                if (!String.IsNullOrEmpty(element.Key) && element.Key.Equals(loggingModeKey, StringComparison.OrdinalIgnoreCase))
                {
                    found = element;
                    b = true;
                }
            }
            if (!b)
            {
                found = new eXtensibleWebApiElement()
                {
                    Key = XFConstants.Config.DEFAULTKEY,
                    LoggingStrategy = XFConstants.Config.DefaultLoggingStrategy,
                };
            }
            return found;
        }

    }
}