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
            IEventWriter writer = new EventLogWriter();

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
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                writer.WriteError(errorMessage, SeverityType.Critical, "ConfigurationProvider");
            }

            if (provider == null)
            {
                provider = new SystemConfigurationProvider();
            }

            var props = eXtensibleConfig.GetProperties();
            string message = String.Format("{0}: {1} (loaded @ {2}", "Configuration Provider", provider.GetType().FullName, DateTime.Now.ToString("G"));
            writer.WriteError(message, SeverityType.Critical, "configuration", props);

            return provider;
        }

    }
}
