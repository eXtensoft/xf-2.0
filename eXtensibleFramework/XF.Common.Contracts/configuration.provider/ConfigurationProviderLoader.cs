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
        #region elide
        //public static IConfigurationProvider Load()
        //{
        //    // if provider exists in filesystem, then check ConfigurationProvider.AppSettings for key
        //    // if no provider exists, then  using the provider is default unles the key says otherwise
        //    //
        //    IConfigurationProvider provider = null;
        //    if (CanLoadPlugin(out provider) && UsePlugin())
        //    {                
        //    }
        //    if (provider == null)
        //    {
        //        provider =  new SystemConfigurationProvider();
        //    }

        //    if (eXtensibleConfig.Inform)
        //    {
        //        var props = eXtensibleConfig.GetProperties();
        //        EventWriter.WriteError(String.Format("Loaded ConfigurationProvider: {0}", provider.GetType().Name), SeverityType.Information, "Configuration", props);
        //    }

        //    return provider;
        //}

        //private static bool CanLoadPlugin(out IConfigurationProvider provider)
        //{
        //    bool b = false;
        //    provider = null;
        //    if (Directory.Exists(eXtensibleConfig.ConfigurationProviderPlugins))
        //    {
        //        var files = Directory.GetFiles(eXtensibleConfig.ConfigurationProviderPlugins,"*ConfigurationProvider*.dll");
        //        for (int i = 0;!b && i < files.Count(); i++)
        //        {
        //            var file = files[i];
        //            try
        //            {
        //                FileInfo info = new FileInfo(file);
        //                List<string> folderpaths = new List<string>();
        //                folderpaths.Add(info.Directory.FullName);
        //                ConfigurationModule module = null;
        //                ModuleLoader<ConfigurationModule> loader = new ModuleLoader<ConfigurationModule>();
        //                loader.Folderpaths = folderpaths;
        //                if (loader.Load(out module) && module.Providers != null && module.Providers.Count > 0)
        //                {
        //                   provider = module.Providers[0];
        //                    if (provider != null)
        //                    {
        //                        b = true;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //                IEventWriter writer = new EventLogWriter();
        //                writer.WriteError(message, SeverityType.Critical, "ConfigurationProvider");
        //            }
        //        }
        //    }
        //    return b;
        //}
        #endregion

        private static bool UsePlugin()
        {
            bool b = true;
            bool usePlugin;
            string usePluginCandidate = ConfigurationManager.AppSettings["use-configuration-provider"];
            if (!String.IsNullOrWhiteSpace(usePluginCandidate) && Boolean.TryParse(usePluginCandidate, out usePlugin))
            {
                b = usePlugin;
            }
            return b;
        }

        public static IConfigurationProvider Load()
        {
            IConfigurationProvider provider = null;
            if (UsePlugin())
            {
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
                        //provider = module.Providers.Find(x=>!x.GetType().Equals(typeof(SystemConfigurationProvider)));
                        if (module.Providers != null && module.Providers.Count > 0)
                        {
                            provider = module.Providers[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    IEventWriter writer = new EventLogWriter();
                    writer.WriteError(message, SeverityType.Critical, "ConfigurationProvider");
                }
            }

            if (provider == null)
            {
                provider = new SystemConfigurationProvider();
            }

            return provider;
        }

    }
}
