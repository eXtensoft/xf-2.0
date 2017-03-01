// <copyright company="eXtensible Solutions, LLC" file="eXtensibleStrategySection.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Configuration;

    public sealed class eXtensibleStrategySection : ConfigurationSection
    {
        [ConfigurationProperty(ConfigConstants.ContextAttributeName, IsRequired = true)]
        public string Context
        {
            get { return (string)this[ConfigConstants.ContextAttributeName]; }
            set { this[ConfigConstants.ContextAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyResolutionAttributeName, IsRequired = true, DefaultValue = "demo")]
        public string DefaultResolution
        {
            get { return (string)this[ConfigConstants.StrategyResolutionAttributeName]; }
            set { this[ConfigConstants.StrategyResolutionAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ResolverTypeAttributeName, IsRequired = false)]
        public string ResolverType
        {
            get { return (string)this[ConfigConstants.ResolverTypeAttributeName]; }
            set { this[ConfigConstants.ResolverTypeAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyCollectionAttributeName, IsRequired = true)]
        public eXtensibleStrategyElementCollection Strategies
        {
            get { return this[ConfigConstants.StrategyCollectionAttributeName] as eXtensibleStrategyElementCollection ?? new eXtensibleStrategyElementCollection(); }
        }


    }
}