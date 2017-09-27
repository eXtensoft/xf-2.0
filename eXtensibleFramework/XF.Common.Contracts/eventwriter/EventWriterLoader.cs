// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using XF.Common.Contracts;

    public static class EventWriterLoader
    {
        public static IEventWriter Load()
        {
            IEventWriter writer = null;
            switch (eXtensibleConfig.LoggingStrategy)
            {
                case LoggingStrategyOption.None:
                    break;
                case LoggingStrategyOption.Output:
                    writer = new DebugEventWriter();
                    break;

                case LoggingStrategyOption.CommonServices:
                    writer = new CommonServicesWriter();
                    break;
                case LoggingStrategyOption.WindowsEventLog:
                    writer = new EventLogWriter();
                    break;
                case LoggingStrategyOption.Silent:
                    writer = new EventLogWriter();
                    break;
                case LoggingStrategyOption.XFTool:
                    writer = new XFToolWriter();
                    break;
                case LoggingStrategyOption.Plugin:
                    writer = PluginLoader.LoadReferencedAssembly<IEventWriter>().FirstOrDefault() as IEventWriter;
                    break;
                case LoggingStrategyOption.Datastore:
                    writer = LoadDatastoreEventWriter();
                    break;
                default:
                    break;
            }


            return writer;
        }

        private static IEventWriter LoadDatastoreEventWriter()
        {
            IEventWriter provider = null;

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
                EventWriterModule module = null;
                ModuleLoader<EventWriterModule> loader = new ModuleLoader<EventWriterModule>() { Folderpaths = list };
                if (loader.Load(out module) && module.Providers != null && module.Providers.Count > 0)
                {
                    provider = module.Providers.Find(x => !x.GetType().Equals(typeof(DatastoreEventWriter)));
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                IEventWriter writer = new EventLogWriter();
                writer.WriteError(message, SeverityType.Critical, "EventWriterModule");
            }

            if (provider == null)
            {
                provider = new DatastoreEventWriter();
            }

            return provider;
        }

    }

}
