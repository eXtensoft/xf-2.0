using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Data.Linq;
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
            IConfigurationProvider provider = null;
            if (CanLoadPlugin(out provider) && UsePlugin())
            {
            }
            if (provider == null)
            {
                provider =  new SystemConfigurationProvider();
            }            
            return provider;
        }

        private static bool CanLoadPlugin(out IConfigurationProvider provider)
        {
            bool b = false;
            provider = null;
            if (Directory.Exists(eXtensibleConfig.ConfigurationProviderPlugins))
            {
                var files = Directory.GetFiles(eXtensibleConfig.ConfigurationProviderPlugins,"*ConfigurationProvider*.dll");
                for (int i = 0;!b && i < files.Count(); i++)
                {
                    var file = files[i];
                    try
                    {
                        FileInfo info = new FileInfo(file);
                        List<string> folderpaths = new List<string>();
                        folderpaths.Add(info.Directory.FullName);
                        ConfigurationModule module = null;
                        ModuleLoader<ConfigurationModule> loader = new ModuleLoader<ConfigurationModule>();
                        loader.Folderpaths = folderpaths;
                        if (loader.Load(out module) && module.Providers != null && module.Providers.Count > 0)
                        {
                            var x = module.Providers[0];
                            var s = x.GetType();
                        }
                        //Assembly assembly = Assembly.Load(info.FullName);
                        //foreach (Type type in assembly.GetTypes())
                        //{
                        //    if (typeof(IConfigurationProvider).IsAssignableFrom(type) && type.GetConstructor(Type.EmptyTypes) != null)
                        //    {
                        //        try
                        //        {
                        //            provider = (IConfigurationProvider)Activator.CreateInstance(type);
                        //            b = true;
                        //            break;
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            string activationMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        //            IEventWriter writer = new EventLogWriter();
                        //            writer.WriteError(activationMessage, SeverityType.Critical, "ConfigurationProvider");
                        //        }
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        IEventWriter writer = new EventLogWriter();
                        writer.WriteError(message, SeverityType.Critical, "ConfigurationProvider");
                    }
                }
            }
            return b;
        }

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

    }
}
