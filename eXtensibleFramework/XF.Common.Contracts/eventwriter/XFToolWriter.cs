// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
