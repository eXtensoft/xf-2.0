// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Configuration;

    public sealed class eXtensibleFrameworkElement : ConfigurationElement
    {
        public eXtensibleFrameworkElement()
        {
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("loggingStrategy", IsRequired = true)]
        public string LoggingStrategy
        {
            get { return (string)this["loggingStrategy"]; }
            set { this["loggingStrategy"] = value; }
        }

        [ConfigurationProperty("loggingMode", IsRequired = false)]
        public string LoggingMode
        {
            get
            {
                object o = this["loggingMode"];
                string s = o != null ? o.ToString() : LoggingModeOption.None.ToString();
                LoggingModeOption option;
                Enum.TryParse<LoggingModeOption>(s, true, out option);
                return option.ToString().ToLower();
            }
            set { this["loggingMode"] = value; }
        }

        [ConfigurationProperty("loggingSchema", IsRequired = false)]
        public string LoggingSchema
        {
            get
            {
                object o = this["loggingSchema"];
                string s = o != null ? o.ToString() : DateTimeSchemaOption.None.ToString();
                DateTimeSchemaOption option;
                Enum.TryParse<DateTimeSchemaOption>(s, true, out option);
                return option.ToString();
            }
            set { this["loggingSchema"] = value; }
        }


        [ConfigurationProperty("publishSeverity", IsRequired = true)]
        public string PublishSeverity
        {
            get { return (string)this["publishSeverity"]; }
            set
            {
                this["publishSeverity"] = value;
            }
        }

        [ConfigurationProperty("datastoreKey", IsRequired = false)]
        public string DatastoreKey
        {
            get { return (String)this["datastoreKey"]; }
            set { this["datastoreKey"] = value; }
        }

        [ConfigurationProperty("inform", IsRequired = false )]
        public bool Inform
        {
            get { return (bool)this["inform"]; }
            set { this["inform"] = value; }
        }
    }
}