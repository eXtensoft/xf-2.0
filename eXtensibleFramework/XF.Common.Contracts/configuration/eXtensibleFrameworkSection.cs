// <copyright company="eXtensible Solutions, LLC" file="eXtensibleFrameworkSection.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Configuration;

    public sealed class eXtensibleFrameworkSection : ConfigurationSection
    {
       
        [ConfigurationProperty(ConfigConstants.ZoneAttributeName, IsRequired = true)]
        public string Zone
        {
            get { return (string)this[ConfigConstants.ZoneAttributeName]; }
            set { this[ConfigConstants.ZoneAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ContextAttributeName, IsRequired = false)]
        public string Context
        {
            get { return (string)this[ConfigConstants.ContextAttributeName]; }
            set { this[ConfigConstants.ContextAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.InstanceIdAttributeName, IsRequired = false)]
        public string InstanceIdentifier
        {
            get { return (string)this[ConfigConstants.InstanceIdAttributeName]; }
            set { this[ConfigConstants.InstanceIdAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.DataFolderpathAttributeName, IsRequired = false)]
        public string DataFolderpath
        {
            get { return (string)this[ConfigConstants.DataFolderpathAttributeName]; }
            set { this[ConfigConstants.DataFolderpathAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.IsAsyncAttributeName, IsRequired = false)]
        public bool IsAsync
        {
            get { return (bool)this[ConfigConstants.IsAsyncAttributeName]; }
            set { this[ConfigConstants.IsAsyncAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.UserIdParamNameAttributName, IsRequired = false)]
        public string UserIdentityParamName
        {
            get { return (string)this[ConfigConstants.UserIdParamNameAttributName]; }
            set { this[ConfigConstants.UserIdParamNameAttributName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.CaptureMetricsAttributeName, IsRequired = false)]
        public bool CaptureMetrics
        {
            get  { return (this[ConfigConstants.CaptureMetricsAttributeName] != null) ?  (bool)this[ConfigConstants.CaptureMetricsAttributeName] : false; }
            set { this[ConfigConstants.CaptureMetricsAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.LogSourceAttributeName, IsRequired = false)]
        public string LogSource
        {
            get { return (string)this[ConfigConstants.LogSourceAttributeName]; }
            set { this[ConfigConstants.LogSourceAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.LoggingCategoryAttributeName, IsRequired = false)]
        public string DefaultLoggingCategory
        {
            get { return (string)this[ConfigConstants.LoggingCategoryAttributeName]; }
            set { this[ConfigConstants.LoggingCategoryAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.LoggingAttributeName, IsRequired = true)]
        public string LoggingKey
        {
            get { return (string)this[ConfigConstants.LoggingAttributeName]; }
            set { this[ConfigConstants.LoggingAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ElementsElementName, IsRequired = true)]
        public eXtensibleFrameworkElementCollection Elements
        {
            get { return this[ConfigConstants.ElementsElementName] as eXtensibleFrameworkElementCollection ?? new eXtensibleFrameworkElementCollection(); }
        }

        [ConfigurationProperty(ConfigConstants.ApiDomainElementName, IsRequired = false)]
        public string ApiDomain
        {
            get { return (string)this[ConfigConstants.ApiDomainElementName]; }
            set { this[ConfigConstants.ApiDomainElementName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ApiEndpointErrorsAttributeName, IsRequired = false)]
        public string ApiErrorsEndpoint
        {
            get { return (string)this[ConfigConstants.ApiEndpointErrorsAttributeName]; }
            set { this[ConfigConstants.ApiEndpointErrorsAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ApiEndpointEventsAttributeName, IsRequired = false)]
        public string ApiEventsEndpoint
        {
            get { return (string)this[ConfigConstants.ApiEndpointEventsAttributeName]; }
            set { this[ConfigConstants.ApiEndpointEventsAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ApiEndpointStatiiAttributeName, IsRequired = false)]
        public string ApiStatiiEndpoint
        {
            get { return (string)this[ConfigConstants.ApiEndpointStatiiAttributeName]; }
            set { this[ConfigConstants.ApiEndpointStatiiAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ApiEndpointMetricsAttributeName, IsRequired = false)]
        public string ApiMetricsEndpoint
        {
            get { return (string)this[ConfigConstants.ApiEndpointMetricsAttributeName]; }
            set { this[ConfigConstants.ApiEndpointMetricsAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ApiEndpointAlertsAttributeName, IsRequired = false)]
        public string ApiAlertsEndpoint
        {
            get { return (string)this[ConfigConstants.ApiEndpointAlertsAttributeName]; }
            set { this[ConfigConstants.ApiEndpointAlertsAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ApiEndpointTasksAttributeName, IsRequired = false)]
        public string ApiTasksEndpoint
        {
            get { return (string)this[ConfigConstants.ApiEndpointTasksAttributeName]; }
            set { this[ConfigConstants.ApiEndpointTasksAttributeName] = value; }
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

        [ConfigurationProperty(ConfigConstants.HandleAlerts, IsRequired = false)]
        public bool HandleAlerts
        {
            get
            {
                bool b = false;
                object o = this[ConfigConstants.HandleAlerts];
                if (o != null && Boolean.TryParse(o.ToString(), out b))
                {
                    return b;
                }
                else
                {
                    return false;
                }
            }
            set { this[ConfigConstants.HandleAlerts] = value; }
        }
    }
}