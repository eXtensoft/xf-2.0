// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Configuration;

    public sealed class eXtensibleStrategyElement : ConfigurationElement
    {
        public eXtensibleStrategyElement()
        {
        }

        [ConfigurationProperty(ConfigConstants.StrategyNameAttributeName, IsRequired = true)]
        public string Name
        {
            get { return (string)this[ConfigConstants.StrategyNameAttributeName]; }
            set { this[ConfigConstants.StrategyNameAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyTypeAttributeName, IsRequired = false, DefaultValue = StrategyTypeOption.KeyPair)]
        public StrategyTypeOption StrategyType
        {
            get { return (StrategyTypeOption)this[ConfigConstants.StrategyTypeAttributeName]; }
            set { this[ConfigConstants.StrategyTypeAttributeName] = value.ToString(); }
        }

        [ConfigurationProperty(ConfigConstants.StrategyValueAttributeName, IsRequired = false)]
        public string StrategyValue
        {
            get { return (string)this[ConfigConstants.StrategyValueAttributeName]; }
            set { this[ConfigConstants.StrategyValueAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyResolutionAttributeName, IsRequired = true)]
        public string Resolution
        {
            get { return (string)this[ConfigConstants.StrategyResolutionAttributeName]; }
            set { this[ConfigConstants.StrategyResolutionAttributeName] = value; }
        }
    }
}