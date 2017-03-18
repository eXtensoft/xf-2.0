using System;
using System.Collections.Generic;
using System.Linq;


namespace XF.Common.Contracts
{
    internal static class ConfigurationProviderLoader
    {
        public static IConfigurationProvider Load()
        {
            // if provider exists in filesystem, then check ConfigurationProvider.AppSettings for key
            // if no provider exists, then  using the provider is default unles the key says otherwise
            //

            return  new SystemConfigurationProvider();
        }
    }
}
