using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common
{
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
                case EventTypeOption.Status:
                case EventTypeOption.Task:
                case EventTypeOption.Alert:
                case EventTypeOption.Kpi:
                case EventTypeOption.None:
                case EventTypeOption.Event:
                default:
                    SqlServerEventWriter.Post(properties);
                    break;
            }
        }

        protected override void Publish(eXMetric metric)
        {
            throw new NotImplementedException();
        }
    }
}
