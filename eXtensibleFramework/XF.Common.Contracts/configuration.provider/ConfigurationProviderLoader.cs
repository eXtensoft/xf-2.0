// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.Common.Contracts
{

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    internal static class ConfigurationProviderLoader
    {

        public static IConfigurationProvider Load()
        {
            IConfigurationProvider provider = null;

            List<string> list = new List<string>()
            {
                AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"bin"),
            };
            if (Directory.Exists(eXtensibleConfig.ConfigurationProviderPlugins))
            {
                list.Add(eXtensibleConfig.ConfigurationProviderPlugins);
            }
            try
            {
                ConfigurationModule module = null;
                ModuleLoader<ConfigurationModule> loader = new ModuleLoader<ConfigurationModule>() { Folderpaths = list };
                if (loader.Load(out module) && module.Providers != null && module.Providers.Count > 0)
                {
                    provider = module.Providers.Find(x=>!x.GetType().Equals(typeof(SystemConfigurationProvider)));
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                IEventWriter writer = new EventLogWriter();
                writer.WriteError(message, SeverityType.Critical, "ConfigurationProvider");
            }

            if (provider == null)
            {
                provider = new SystemConfigurationProvider();
            }

            return provider;
        }

    }
}
