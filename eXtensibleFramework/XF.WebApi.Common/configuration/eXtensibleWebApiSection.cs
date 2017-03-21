// <copyright company="Recorded Books, Inc" file="eXtensibleWebApiSection.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi.Config
{
    using System;
    using System.Configuration;

    public sealed class eXtensibleWebApiSection : ConfigurationSection
    {
        [ConfigurationProperty(ConfigConstants.LogSourceAttributeName, IsRequired = false)]
        public string LogSource
        {
            get { return (string)this[ConfigConstants.LogSourceAttributeName]; }
            set { this[ConfigConstants.LogSourceAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.LoggingAttributeName, IsRequired = true)]
        public string LoggingKey
        {
            get { return (string)this[ConfigConstants.LoggingAttributeName]; }
            set { this[ConfigConstants.LoggingAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ElementsElementName, IsRequired = true)]
        public eXtensibleWebApiElementCollection Elements
        {
            get { return this[ConfigConstants.ElementsElementName] as eXtensibleWebApiElementCollection ?? new eXtensibleWebApiElementCollection(); }
        }


        [ConfigurationProperty(ConfigConstants.BigDataUrlAttributeName, IsRequired = false)]
        public string BigDataUrl
        {
            get { return (string)this[ConfigConstants.BigDataUrlAttributeName]; }
            set { this[ConfigConstants.BigDataUrlAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ServiceTokenAttributeName, IsRequired = false)]
        public string ServiceToken
        {
            get { return (string)this[ConfigConstants.ServiceTokenAttributeName]; }
            set { this[ConfigConstants.ServiceTokenAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.MessageProviderFolder, IsRequired= false)]
        public string MessageProviderFolder
        {
            get 
            {
                object o = this[ConfigConstants.MessageProviderFolder];
                return (o != null && !String.IsNullOrWhiteSpace(o.ToString())) ? (string)o : XFWebApiConstants.Default.MessageProviderFolder;
            }
            set { this[ConfigConstants.MessageProviderFolder] = value; }
        }

        [ConfigurationProperty(ConfigConstants.CatchAllControllerId, IsRequired = false)]
        public string CatchAllControllerId
        {
            get
            {
                object o = this[ConfigConstants.CatchAllControllerId];
                if (o != null && !String.IsNullOrWhiteSpace(o.ToString()))
                {
                    return (string)o;
                }
                else
                {
                    string s = XFWebApiConstants.Default.CatchAllControllerId;
                    return s;
                }

            }
            set { this[ConfigConstants.CatchAllControllerId] = value; }
        }

        [ConfigurationProperty(ConfigConstants.MessageIdHeaderKey, IsRequired = false)]
        public string MessageIdHeaderKey
        {
            get
            {
                object o = this[ConfigConstants.MessageIdHeaderKey];
                if (o != null && !String.IsNullOrWhiteSpace(o.ToString()))
                {
                    return (string)o;
                }
                else
                {
                    string s = XFWebApiConstants.Default.MessageIdHeaderKey;
                    return s;
                }

            }
            set { this[ConfigConstants.CatchAllControllerId] = value; }
        }



        [ConfigurationProperty(ConfigConstants.EditRegistration, IsRequired = false)]
        public bool EditRegistration
        {
            get { return (this[ConfigConstants.EditRegistration] != null) ? (bool)this[ConfigConstants.EditRegistration] : false; }
            set { this[ConfigConstants.EditRegistration] = value; }
        }

    }

}
