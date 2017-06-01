// <copyright company="Recorded Books, Inc" file="eXtensibleWebApiElement.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi.Config
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XF.Common;

    public class eXtensibleWebApiElement : ConfigurationElement
    {
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
                Enum.TryParse<LoggingModeOption>(s,true,out option);
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

        [ConfigurationProperty("datastoreKey", IsRequired = false)]
        public string DatastoreKey
        {
            get { return (String)this["datastoreKey"]; }
            set { this["datastoreKey"] = value; }
        }


    }

}
