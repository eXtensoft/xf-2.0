// <copyright company="eXtensible Solutions LLC" file="XFToolWriter.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    public class XFToolWriter : EventWriterBase
    {


        protected override void Publish(EventTypeOption eventType, List<TypedItem> properties)
        {
            PublishLocal(eventType, properties);
        }

        protected override void Publish(eXMetric metric)
        {
            PublishLocal(EventTypeOption.None, metric.Items.ToList());
        }

        private void PublishLocal(EventTypeOption eventType, List<TypedItem> properties)
        {
            try
            {
                string pipeName = "pipeXF";
                EndpointAddress endpoint = new EndpointAddress(String.Format("{0}/{1}",XFConstants.PipeUrl,pipeName));
                IEventService proxy = ChannelFactory<IEventService>.CreateChannel(new NetNamedPipeBinding(), endpoint);
                proxy.Write(eventType, properties);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}
