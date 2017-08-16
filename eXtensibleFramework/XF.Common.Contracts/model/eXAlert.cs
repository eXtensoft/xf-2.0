// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    public class eXAlert : eXBase
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string[] Audiences { get; set; }

        [DataMember]
        public string[] Categories { get; set; }

        [DataMember]
        public string Importance { get; set; }

        [DataMember]
        public string Urgency { get; set; }

        [DataMember]
        public string NamedRecipient { get; set; }

        [DataMember]
        public string Topic { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Error { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public string Data { get; set; }

        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public int Month { get; set; }
        [DataMember]
        public int Day { get; set; }
        [DataMember]
        public string DayOfWeek { get; set; }
        [DataMember]
        public int Hour { get; set; }
        [DataMember]
        public int Minute { get; set; }

        [DataMember]
        public string[] Dispositions { get; set; }

        [DataMember]
        public string CurrentStatus { get; set; }

        public eXAlert() { }

        public eXAlert(List<TypedItem> list)
        {
            this.Items = list;
            this.Tds = DateTime.Now;
            CreatedAt = Tds.ToString(XFConstants.DateTimeFormat);
            string status = "none";
            string disposition = String.Format("{0}:{1}", status, DateTime.Now.ToString(XFConstants.DateTimeFormat));
            Dispositions = new string[] {disposition};
            CurrentStatus = status;
            if (list.ContainsKey<Guid>(XFConstants.EventWriter.MessageId))
            {
                MessageId = list.Get<Guid>(XFConstants.EventWriter.MessageId);
            }
            if (list.ContainsKey<string>(XFConstants.Alert.Message))
            {
                Message = list.Get<string>(XFConstants.Alert.Message);
            }
            if (list.ContainsKey<string>(XFConstants.Context.ZONE))
            {
                Zone = list.Get<string>(XFConstants.Context.ZONE);
            }
            if (list.ContainsKey<string>(XFConstants.Context.Application))
            {
                ApplicationKey = list.Get<string>(XFConstants.Context.Application);
            }
            if (list.ContainsKey<string>("event.alert.title"))
            {
                Title = list.Get<string>("event.alert.title");
            }
            if (list.ContainsKey<string>("event.alert.categories"))
            {
                string s = list.Get<string>("event.alert.categories");
                if (!String.IsNullOrEmpty(s))
                {
                    string[] t = s.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> items = new List<string>();
                    foreach (var item in t)
                    {
                        items.Add(item.Trim());
                    }
                    Categories = items.ToArray();
                }
            }

            if (list.ContainsKey<string>("event.alert.importance"))
            {
                Importance = list.Get<string>("event.alert.importance");
            }
            if (list.ContainsKey<string>("event.alert.urgency"))
            {
                Urgency = list.Get<string>("event.alert.urgency");
            }
            if (list.ContainsKey<string>("event.alert.namedtarget"))
            {
                NamedRecipient = list.Get<string>("event.alert.namedtarget");
            }

            if (list.ContainsKey<string>("event.alert.stacktrace"))
            {
                StackTrace = list.Get<string>("event.alert.stacktrace");
            }

            if (list.ContainsKey<string>("event.alert.error"))
            {
                Error = list.Get<string>("event.alert.error");
            }
            if (list.ContainsKey<string>("event.alert.source"))
            {
                Source = list.Get<string>("event.alert.source");
            }
            if (list.ContainsKey<string>("event.alert.topic"))
            {
                Topic = list.Get<string>("event.alert.topic");
            }

            if (list.ContainsKey<string>("event.alert.data"))
            {
                Data = list.Get<string>("event.alert.data");
            }

            if (list.ContainsKey("event.alert.targets"))
            {
                string s = list.Get<string>("event.alert.targets");
                if (!String.IsNullOrWhiteSpace(s))
                {
                    string[] t = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> items = new List<string>();
                    foreach (var item in t)
                    {
                        items.Add(item.Trim());
                    }
                    Audiences = items.ToArray();
                }
            }

            Year = Tds.Year;
            Month = Tds.Month;
            Day = Tds.Day;
            DayOfWeek = Tds.DayOfWeek.ToString();
            Hour = Tds.Hour;
            Minute = Tds.Minute;

        }

    }
}
