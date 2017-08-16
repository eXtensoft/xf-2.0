// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Linq;

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
                    writer = new DatastoreEventWriter();
                    break;
                default:
                    break;
            }
            //return new EventLogWriter() as IEventWriter;

            return writer;
        }
    }

}
