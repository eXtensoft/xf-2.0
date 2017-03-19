

namespace XF.Common.Contracts
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    public class ConfigurationModule
    {
        [ImportMany(typeof(IConfigurationProvider))]
        public List<IConfigurationProvider> Providers { get; set; }

    }
}
