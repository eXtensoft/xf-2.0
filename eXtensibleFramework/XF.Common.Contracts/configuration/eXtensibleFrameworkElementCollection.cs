// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Configuration;

    public sealed class eXtensibleFrameworkElementCollection : ConfigurationElementCollection
    {
        public eXtensibleFrameworkElement this[int index]
        {
            get { return base.BaseGet(index) as eXtensibleFrameworkElement; }
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
            return new eXtensibleFrameworkElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as eXtensibleFrameworkElement).Key;
        }

        public void Add(eXtensibleFrameworkElement element)
        {
            base.BaseAdd(element);
        }

        public eXtensibleFrameworkElementCollection()
        {
            eXtensibleFrameworkElement element = (eXtensibleFrameworkElement)CreateNewElement();
            base.BaseAdd(element);
        }

        public eXtensibleFrameworkElement GetForLoggingMode(string loggingModeKey)
        {
            bool b = false;
            eXtensibleFrameworkElement found = null;
            for (int i = 0; !b && i < this.Count; i++)
            {
                eXtensibleFrameworkElement element = this[i];
                if (!String.IsNullOrEmpty(element.Key) && element.Key.Equals(loggingModeKey, StringComparison.OrdinalIgnoreCase))
                {
                    found = element;
                    b = true;
                }
            }
            if (!b)
            {
                found = new eXtensibleFrameworkElement()
                {
                    Key = XFConstants.Config.DEFAULTKEY,
                    LoggingStrategy = XFConstants.Config.DefaultLoggingStrategy,
                    PublishSeverity = XFConstants.Config.DefaultPublishingSeverity
                };
            }
            return found;
        }

    }
}