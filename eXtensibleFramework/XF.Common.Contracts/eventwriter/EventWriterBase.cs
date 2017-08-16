// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

    public abstract class EventWriterBase : IEventWriter
    {
        protected static IList<Type> typeWhitelist = new List<Type>{
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
            //typeof(DateTimeOffset),
            typeof(Guid),
        };

        #region abstract methods

        protected abstract void Publish(EventTypeOption eventType, List<TypedItem> properties);
        protected abstract void Publish(eXMetric metric);

        #endregion

        #region interface methods

        void IEventWriter.WriteError(object errorMessage, SeverityType severity)
        {
            WriteError(errorMessage, severity);
        }

        void IEventWriter.WriteError(object errorMessage, SeverityType severity, string errorCategory)
        {
            WriteError(errorMessage, severity, errorCategory);
        }

        void IEventWriter.WriteError(object errorMessage, SeverityType severity, string errorCategory, IDictionary<string, object> properties)
        {
            WriteError(errorMessage, severity, errorCategory, properties);
        }

        void IEventWriter.WriteEvent(string eventMessage, IDictionary<string, object> properties)
        {
            WriteEvent(eventMessage, properties);
        }

        void IEventWriter.WriteEvent(string eventMessage, string eventCategory, IDictionary<string, object> properties)
        {
            WriteEvent(eventMessage, eventCategory, properties);
        }

        void IEventWriter.WriteEvent<T>(ModelActionOption modelAction, IDictionary<string, object> properties)
        {
            WriteEvent<T>(modelAction, properties);
        }

        void IEventWriter.WriteEvent<T>(ModelActionOption modelAction, object modelId, IDictionary<string, object> properties)
        {
            WriteEvent<T>(modelAction, modelId, properties);
        }

        void IEventWriter.WriteEvent<T>(ModelActionOption modelAction, T t, IDictionary<string, object> properties)
        {
            WriteEvent<T>(modelAction, t, properties);
        }

        void IEventWriter.WriteStatus(string modelType, object modelId, string modelStatus, string modelName)
        {
            WriteStatus(modelType, modelId, modelStatus, modelName);
        }

        void IEventWriter.WriteStatus(string modelType, object modelId, string modelStatus, string modelName, IDictionary<string, object> properties)
        {
            WriteStatus(modelType, modelId, modelStatus, modelName, properties);
        }

        void IEventWriter.WriteStatus(string modelType, object modelId, string modelStatus, string modelName, DateTimeOffset statusEffective)
        {
            WriteStatus(modelType, modelId, modelStatus, modelName, statusEffective);
        }

        void IEventWriter.WriteStatus(string modelType, object modelId, string modelStatus, string modelName, DateTimeOffset statusEffective, IDictionary<string, object> properties)
        {
            WriteStatus(modelType, modelId, modelStatus, modelName, statusEffective, properties);
        }

        void IEventWriter.WriteMetric(eXMetric item)
        {
            LocalPublish(item);
        }
        void IEventWriter.WriteTask(string taskMessage, IDictionary<string, object> properties)
        {
            WriteTask(taskMessage, properties);
        }

        void IEventWriter.Write(EventTypeOption option, List<TypedItem> properties)
        {
            Publish(option, properties);
        }

        #endregion

        #region local implementations

        private void WriteError(object errorMessage, SeverityType severity)
        {
            WriteError(errorMessage, severity, eXtensibleConfig.DefaultLoggingCategory);
        }

        private void WriteError(object errorMessage, SeverityType severity, string errorCategory)
        {
            WriteError(errorMessage, severity, errorCategory, null);
        }

        private void WriteError(object errorMessage, SeverityType severity, string errorCategory, IDictionary<string, object> properties)
        {
            var props = EnsureProperties(properties);
            props.Add(XFConstants.EventWriter.Message, errorMessage.ToString());
            props.Add(XFConstants.EventWriter.ErrorSeverity, severity.ToString());
            props.Add(XFConstants.EventWriter.Category, errorCategory);
            props.Add(XFConstants.EventWriter.EventType, EventTypeOption.Error);
            LocalPublish(EventTypeOption.Error,props);  
        }

        private void WriteEvent(string eventMessage, IDictionary<string, object> properties)
        {
            var props = EnsureProperties(properties);
            props.Add(XFConstants.EventWriter.Message, eventMessage);
            props.Add(XFConstants.EventWriter.EventType, EventTypeOption.Event);
            LocalPublish(EventTypeOption.Event, props);
        }

        private void WriteEvent(string eventMessage, string eventCategory, IDictionary<string, object> properties)
        {
            var props = EnsureProperties(properties);
            props.Add(XFConstants.EventWriter.Category, eventCategory);
            WriteEvent(eventMessage, props);
        }

        private void WriteEvent<T>(ModelActionOption modelAction, IDictionary<string, object> properties) where T : class, new()
        {
            var props = EnsureProperties(properties);
            string modelname = GetModelName<T>();
            props.Add(XFConstants.EventWriter.ModelAction, modelAction);
            props.Add(XFConstants.EventWriter.EventType, EventTypeOption.Event);

            LocalPublish(EventTypeOption.Event,props);
        }

        private void WriteEvent<T>(ModelActionOption modelAction, object modelId, IDictionary<string, object> properties) where T : class, new()
        {
            var props = EnsureProperties(properties);
            props.Add(XFConstants.EventWriter.ModelId, modelId.ToString());
            WriteEvent<T>(modelAction, props);
        }

        private void WriteEvent<T>(ModelActionOption modelAction, T t, IDictionary<string, object> properties) where T : class, new()
        {
            var props = EnsureProperties(properties);
            props.Add(XFConstants.EventWriter.Model, t.ToString());
            WriteEvent<T>(modelAction, props);
        }

        private void WriteTask(string taskMessage, IDictionary<string, object> properties)
        {
            var props = EnsureProperties(properties);
            props.Add(XFConstants.EventWriter.Message, taskMessage);
            props.Add(XFConstants.EventWriter.EventType, EventTypeOption.Task);
            LocalPublish(EventTypeOption.Task, props);
        }

        private void WriteStatus(string modelType, object modelId, string modelStatus, string modelName)
        {
            WriteStatus(modelType, modelId, modelStatus, modelName, DateTime.Now);
        }

        private void WriteStatus(string modelType, object modelId, string modelStatus, string modelName, IDictionary<string, object> properties)
        {
            var props = EnsureProperties(properties);
            props.Add(XFConstants.Status.ModelType, modelType);
            props.Add(XFConstants.Status.ModelId, modelId.ToString());
            props.Add(XFConstants.Status.StatusText, modelStatus);
            props.Add(XFConstants.Status.ModelName, modelName);
            props.Add(XFConstants.EventWriter.EventType, EventTypeOption.Status);
            LocalPublish(EventTypeOption.Status,props);
        }


        private void WriteStatus(string modelType, object modelId, string modelStatus, string modelName, DateTimeOffset statusEffective)
        {
            WriteStatus(modelType, modelId, modelStatus, modelName, statusEffective, new Dictionary<string, object>());
        }

        private void WriteStatus(string modelType, object modelId, string modelStatus, string modelName, DateTimeOffset statusEffective, IDictionary<string, object> properties)
        {
            var props = EnsureProperties(properties);
            props.Add(XFConstants.EventWriter.Effective, statusEffective);
            WriteStatus(modelType, modelId, modelStatus, modelName, props);
        }


        #endregion

        public static IDictionary<string,object> EnsureProperties(IDictionary<string,object> properties)
        {            
            if (properties == null)
            {
                properties = new Dictionary<string, object>();
            }
            return properties;
        }

        #region helper methods

        private void LocalPublish(eXMetric item)
        {
            Publish(item);
        }



        internal void LocalPublish(EventTypeOption option, IDictionary<string,object> properties)
        {
            if (!properties.ContainsKey("xf-id"))
            {
                Guid g = eXtensiblePrincipal.GetCallerId();
                properties.Add("xf-id", g);
            }
            List<TypedItem> list = Convert(properties);
                       
            //new Action<EventTypeOption, List<TypedItem>>(Publish).BeginInvoke(option, list,null,null);
            // Retained for debugging purposes
            Publish(option, list);
        }



        internal static List<TypedItem> Convert(IDictionary<string,object> properties)
        {
            List<TypedItem> list = new List<TypedItem>();
            HashSet<string> hs = new HashSet<string>();
            foreach (var prop in properties)
            {
                if (!String.IsNullOrWhiteSpace(prop.Key) && prop.Value != null)
                {
                    string key = prop.Key;
                    if (hs.Add(key))
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
                                list.Add(new TypedItem(key,s));
                                b = true;
                            }
                            catch {}

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



        private static string GetModelName<T>()
        {
            T t = Activator.CreateInstance<T>();
            return t.GetType().FullName;
        }

        #endregion


    }
}
