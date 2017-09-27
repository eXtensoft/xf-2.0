// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

    public class DatastoreEventWriter : EventWriterBase
    {
        protected override void Publish(EventTypeOption eventType, List<TypedItem> properties)
        {
            switch (eventType)
            {
                case EventTypeOption.Error:
                    eXError error = new eXError(properties);
                    SqlServerEventWriter.Post(error);
                    break;
                case EventTypeOption.Alert:
                    SqlServerEventWriter.PostAlert(properties);
                    break;
                case EventTypeOption.Status:
                case EventTypeOption.Task:
                case EventTypeOption.Kpi:
                case EventTypeOption.None:
                case EventTypeOption.Event:
                default:
                    SqlServerEventWriter.PostList(properties);
                    break;
            }
        }


        protected override void Publish(eXMetric metric)
        {

        }
    }
}
