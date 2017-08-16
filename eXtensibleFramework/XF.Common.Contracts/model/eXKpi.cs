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
    public class eXKpi : eXBase
    {
        [DataMember]
        public string AppContextInstance { get; set; }

        [DataMember]
        public string SessionId { get; set; }

        [DataMember]
        public string Src { get; set; }

        [DataMember]
        public string ScopeId { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public string Activity { get; set; }

        [DataMember]
        public List<string> Data { get; set; }

        [DataMember]
        public int Elapsed { get; set; }

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

        public eXKpi() { }

        public eXKpi(List<TypedItem> list)
        {
            this.Items = list;
            this.Tds = DateTime.Now;
            CreatedAt = Tds.ToString(XFConstants.DateTimeFormat);
            if (list.ContainsKey<Guid>(XFConstants.EventWriter.MessageId))
            {
                MessageId = list.Get<Guid>(XFConstants.EventWriter.MessageId);
            }
            if (list.ContainsKey<string>("key"))
            {
                Key = list.Get<string>("key");
            }
            if (list.ContainsKey<string>("group"))
            {
                Group = list.Get<string>("group");
            }
            if (list.ContainsKey<string>("scopeid"))
            {
                ScopeId = list.Get<string>("scopeid");
            }
            if (list.ContainsKey<string>("sessionid"))
            {
                SessionId = list.Get<string>("sessionid");
            }
            if (list.ContainsKey<string>("activity"))
            {
                Activity = list.Get<string>("activity");
            }
            if (list.ContainsKey<string>(XFConstants.EventWriter.Message))
            {
                Message = list.Get<string>(XFConstants.EventWriter.Message);
            }
            if (list.ContainsKey<string>(XFConstants.Context.ZONE))
            {
                Zone = list.Get<string>(XFConstants.Context.ZONE);
            }
            if (list.ContainsKey<string>(XFConstants.Context.Application))
            {
                ApplicationKey = list.Get<string>(XFConstants.Context.Application);
            }
            if (list.ContainsKey<string>("src"))
            {
                Src = list.Get<string>("src");
            }
            if (list.ContainsKey<string>("name"))
            {
                Key = list.Get<string>("name");
            }
            if (list.ContainsKey<string>("data"))
            {
                string s = list.Get<string>("data");
                string[] t = s.Split(new char[] { ';' });
                Data = new List<string>(t);
            }

            if (list.ContainsKey<int>("elapsed"))
            {
                Elapsed = list.Get<int>("elapsed");
            }


            if (list.ContainsKey<string>(XFConstants.EventWriter.Message))
            {
                Message = list.Get<string>(XFConstants.EventWriter.Message);
            }
            if (list.ContainsKey<string>(XFConstants.Context.ZONE))
            {
                Zone = list.Get<string>(XFConstants.Context.ZONE);
            }
            if (list.ContainsKey<string>(XFConstants.Context.Application))
            {
                ApplicationKey = list.Get<string>(XFConstants.Context.Application);
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
