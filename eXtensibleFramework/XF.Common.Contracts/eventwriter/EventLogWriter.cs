// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Open command prompt as Administrator, then type:
    /// eventcrete /ID 1 /L Application /T Information /SO eXtensoft.Log /D "test log"
    /// press [Enter]
    /// </summary>
    public class EventLogWriter : EventWriterBase
    {
        private const string LogName = "Application";
        private EventTypeOption[] passthroughs = new EventTypeOption[] { EventTypeOption.Status, EventTypeOption.Event, EventTypeOption.Task, EventTypeOption.Alert, EventTypeOption.Kpi, EventTypeOption.Custom };

        protected override void Publish(EventTypeOption eventType, List<TypedItem> properties)
        {
            bool b = false;
            if (passthroughs.Contains(eventType))
            {
                b = true;
            }
            TraceEventTypeOption option = TraceEventTypeOption.Verbose;
            StringBuilder sb = new StringBuilder();
            foreach (var prop in properties)
            {
                if (prop.Value != null)
                {
                    if (prop.Key.Equals(XFConstants.EventWriter.ErrorSeverity) && Enum.TryParse<TraceEventTypeOption>(prop.Value.ToString(), out option))
                    {
                        b = true;
                    }
                    sb.AppendLine(String.Format("{0}\t:\t{1}", prop.Key, prop.Value.ToString()));                     
                }              
            }

            if (!EventLog.SourceExists(eXtensibleConfig.LogSource))
            {
                try
                {
                    EventLog.CreateEventSource(eXtensibleConfig.LogSource, LogName);

                }
                catch (Exception)
                {
                    b = false;
                }
                    
            }
            if (b)
            {
                if (option == TraceEventTypeOption.Critical | option == TraceEventTypeOption.Error)
                {
                    Publish(EventLogEntryType.Error, sb.ToString());
                }
                else if (option == TraceEventTypeOption.Warning)
                {
                    Publish(EventLogEntryType.Warning, sb.ToString());
                        
                }
                else
                {
                    Publish(EventLogEntryType.Information, sb.ToString());
                }
            }

        }

        private void Publish(EventLogEntryType entryType, string message)
        {
            try
            {
                EventLog.WriteEntry(eXtensibleConfig.LogSource, message, entryType, 1);
            }
            catch (Exception)
            {
                
                //throw;
            }
            
        }

        protected override void Publish(eXMetric metric)
        {
            Publish(EventLogEntryType.Information,metric.ToString());
        }
    }
}
