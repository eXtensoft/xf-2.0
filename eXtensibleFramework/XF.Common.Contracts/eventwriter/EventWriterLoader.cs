// <copyright company="eXtensible Solutions, LLC" file="EventWriterLoader.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
