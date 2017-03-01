// <copyright company="eXtensible Solutions LLC" file="AlertWriter.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;


    public class AlertWriter
    {
        #region local fields

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
            typeof(Guid),
        };

        private Dictionary<string, object> properties = new Dictionary<string, object>();
        private HashSet<string> hs = new HashSet<string>();

        #endregion

        #region properties

        public AlertAudiences Audiences { get; set; }

        public AlertCategories Categories { get; set; }

        public ScaleOption Urgency { get; set; }

        public ScaleOption Importance { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public string Error { get; set; }

        public string Stacktrace { get; set; }

        public string NamedRecipient { get; set; }

        public string Topic { get; set; }


        #endregion

        public AlertWriter(string source,string title, string message)
        {
            Source = source;
            Title = title;
            Message = message;
        }

        public AlertWriter(string source, string title, string message, Exception ex)
        {
            Source = source;
            Title = title;
            Message = message;
            Error = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
            Stacktrace = ex.StackTrace;
        }

        public void Add(string key, object value)
        {
            if (hs.Add(key) && typeWhitelist.Contains(value.GetType()))
            {
                properties.Add(key, value);
            }
        }

        #region stateless methods

        public static void Alert(string title, string message, ScaleOption urgency, ScaleOption importance)
        {
            Alert(title,message,urgency,importance,null);
        }

        public static void Alert(string title, string message, string[] categories, ScaleOption urgency, ScaleOption importance, params string[] targets)
        {
            Alert(title, message, categories, urgency, importance, null, targets);
        }

        public static void Alert(string title, string message, ScaleOption urgency, ScaleOption importance, IDictionary<string,object> properties)
        {
            Alert(title, message, new string[] { "general" }, urgency, importance, properties,"common");
        }

        public static void Alert(string title, string message, string[] categories, ScaleOption urgency, ScaleOption importance, IDictionary<string,object> properties, params string[] targets)
        {
            //EventWriter.Alert(title, message, categories, urgency, importance, properties, targets);
            var props = Writer.EnsureProperties(properties);
            props.Add(XFConstants.EventWriter.EventType, EventTypeOption.Alert);
            props.Add(XFConstants.Alert.Title, title);
            props.Add(XFConstants.Alert.Message, message);
            props.Add(XFConstants.Alert.Categories, categories.ToDelimited('|'));
            props.Add(XFConstants.Alert.Urgency, urgency.ToString());
            props.Add(XFConstants.Alert.Importance,importance.ToString());
            props.Add(XFConstants.Alert.Targets, targets.ToDelimited('|'));
            props.Add(XFConstants.Alert.CreatedAt, DateTime.Now.ToString(XFConstants.DateTimeFormat));

            List<TypedItem> list = Writer.Convert(props);
            EventWriter.Write(EventTypeOption.Alert, list); 
        }

        #endregion

        public void SendAlert()
        {
            if (IsValid())
            {
                var props = Writer.EnsureProperties(properties);
                props.Add(XFConstants.EventWriter.EventType, EventTypeOption.Alert);
                props.Add(XFConstants.Alert.Title, Title);
                props.Add(XFConstants.Alert.Message, Message);
                props.Add(XFConstants.Alert.Categories, Categories.ToString());
                props.Add(XFConstants.Alert.Urgency, Urgency.ToString());
                props.Add(XFConstants.Alert.Importance,Importance.ToString());
                props.Add(XFConstants.Alert.Targets, Audiences.ToString());
                props.Add(XFConstants.Alert.Source, Source);
                props.Add(XFConstants.Alert.CreatedAt, DateTime.Now.ToString(XFConstants.DateTimeFormat));
                if (!String.IsNullOrEmpty(Error))
                {
                    props.Add(XFConstants.Alert.Error, Error);
                }
                if (!String.IsNullOrWhiteSpace(Stacktrace))
                {
                    props.Add(XFConstants.Alert.StackTrace, Stacktrace);
                
                }
                if (!String.IsNullOrWhiteSpace(NamedRecipient))
                {
                    props.Add(XFConstants.Alert.NamedTarget, NamedRecipient);
                }
                if (!String.IsNullOrWhiteSpace(Topic))
                {
                    props.Add(XFConstants.Alert.Topic, Topic);
                }
                List<TypedItem> list = Writer.Convert(props);
                EventWriter.Write(EventTypeOption.Alert, list);                
            }
            
        }


        private bool IsValid()
        {
            bool b = true;
            b = b ? !String.IsNullOrWhiteSpace(Title) : false;
            b = b ? !String.IsNullOrWhiteSpace(Message) : false;
            b = b ? !String.IsNullOrWhiteSpace(Source) : false;
            return b;
        }

    }
}
