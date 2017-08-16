// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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