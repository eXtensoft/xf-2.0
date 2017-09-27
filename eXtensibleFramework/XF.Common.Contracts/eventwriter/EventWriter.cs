// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;


    public static class EventWriter
    {
        private static IList<Type> typeWhitelist = new List<Type>{
            typeof(bool),
            typeof(char),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(string),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(Guid),
        };

        private static object sync = new object();
        private static volatile IEventWriter writer;

        public static IEventWriter Writer
        {
            get
            {
                if (writer == null)
                {
                    lock(sync)
                    {
                        if (writer == null)
                        {
                            try
                            {
                                writer = EventWriterLoader.Load();
                            }
                            catch (Exception ex)
                            {
                                IEventWriter writer = new EventLogWriter();
                                string m = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                var props = eXtensibleConfig.GetProperties();
                                props.Add("location", "EventWriter.line.50");
                                writer.WriteError(m, SeverityType.Critical, "EventWriter", props);
                            }
                        }
                    }
                }
                return writer;
            }
        }

        public static void Inform(string message)
        {
            Inform(message,eXtensibleConfig.GetProperties());
        }

        public static void Inform(string message, IDictionary<string,object> properties)
        {
            properties.Add(XFConstants.EventWriter.Message, message);
            List<TypedItem> items = Convert(properties);
            Writer.Write(EventTypeOption.Inform, items);
        }

        public static void WriteError(object errorMessage, SeverityType severity)
        {
            Writer.WriteError(errorMessage, severity);  
        }

        public static void WriteError(object errorMessage, SeverityType severity, string errorCategory)
        {
            Writer.WriteError(errorMessage, severity, errorCategory); 
        }

        public static void WriteError(object errorMessage, SeverityType severity, string errorCategory, IDictionary<string, object> properties)
        {
            Writer.WriteError(errorMessage, severity, errorCategory, properties);           
        }
        

        public static void WriteEvent(string eventMessage, IDictionary<string, object> properties)
        {
            Writer.WriteEvent(eventMessage, properties);
        }

        public static void WriteEvent(string eventMessage, string eventCategory, IDictionary<string, object> properties)
        {
            Writer.WriteEvent(eventMessage, eventCategory, properties);
        }

        public static void WriteEvent<T>(ModelActionOption modelAction, IDictionary<string, object> properties) where T : class, new()
        {
            Writer.WriteEvent<T>(modelAction, properties);
        }

        public static void WriteEvent<T>(ModelActionOption modelAction, object modelId, IDictionary<string, object> properties) where T : class, new()
        {
            Writer.WriteEvent<T>(modelAction, modelId, properties);
        }
        
        public static void WriteEvent<T>(ModelActionOption modelAction, T t, IDictionary<string, object> properties) where T : class, new()
        {
            Writer.WriteEvent<T>(modelAction, t, properties);
        }

        public static void WriteTask(string taskMessage, IDictionary<string, object> properties)
        {
            Writer.WriteTask(taskMessage, properties);
        }
       
        public static void WriteStatus(string modelType, object modelId, string modelStatus, string modelName)
        {
            Writer.WriteStatus(modelType, modelId, modelStatus, modelName);                 
        }

        public static void WriteStatus(string modelType, object modelId, string modelStatus, string modelName, IDictionary<string, object> properties)
        {
            Writer.WriteStatus(modelType, modelId, modelStatus,modelName, properties);
        }

        public static void WriteStatus(string modelType, object modelId, string modelStatus, string modelName, DateTimeOffset statusEffective)
        {
            Writer.WriteStatus(modelType, modelId, modelStatus, modelName, statusEffective);

        }

        public static void WriteStatus(string modelType, object modelId, string modelStatus,string modelName, DateTimeOffset statusEffective, IDictionary<string, object> properties)
        {
            Writer.WriteStatus(modelType, modelId, modelStatus,modelName, statusEffective, properties);
        }


        public static void Write(EventTypeOption option, List<TypedItem> properties)
        {
            Writer.Write(option, properties); 
        }

        public static void WriteMetric(eXMetric item)
        {
            Writer.WriteMetric(item); 
        }

        private static List<TypedItem> Convert(IDictionary<string, object> properties)
        {
            List<TypedItem> list = new List<TypedItem>();
            HashSet<string> hs = new HashSet<string>();

            foreach (var prop in properties)
            {
                if (!String.IsNullOrEmpty(prop.Key) && prop.Value != null )
                {
                    string key = prop.Key;
                    if (!key.Equals(XFConstants.EventWriter.EventType) && hs.Add(key))
                    {
                        if (typeWhitelist.Contains(prop.Value.GetType()) | prop.Value.GetType().BaseType.Equals(typeof(Enum)))
                        {
                            list.Add(new TypedItem(key, prop.Value));
                        }
                        else
                        {
                            bool b = false;
                            try
                            {
                                string s = GenericSerializer.XmlStringFromItem(prop.Value);
                                list.Add(new TypedItem(key, s));
                                b = true;
                            }
                            catch { }

                            if (!b)
                            {
                                list.Add(new TypedItem(key, prop.ToString()));
                            }
                        }
                    }
                }
            }
            return list;
        }

    }

}
