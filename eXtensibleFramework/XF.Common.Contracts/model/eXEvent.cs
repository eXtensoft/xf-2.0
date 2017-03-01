// <copyright company="eXtensible Solutions LLC" file="eXEvent.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class eXEvent : eXBase
    {
        [DataMember]
        public string AppContextInstance { get; set; }

        [DataMember]
        public string SessionId { get; set; }

        [DataMember]
        public string PrimaryScope { get; set; }
        [DataMember]
        public string SecondaryScope { get; set; }
        [DataMember]
        public string EventOutcome { get; set; }
        [DataMember]
        public string UseCase { get; set; }
        [DataMember]
        public DateTimeOffset Start { get; set; }
        [DataMember]
        public double Elapsed { get; set; }
        [DataMember]
        public int ResultsetCount { get; set; }
        [DataMember]
        public string Uri { get; set; }
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

        public eXEvent() { }

        public eXEvent(List<TypedItem> list)
        {
            this.Items = list;
            this.Tds = DateTime.Now;
            CreatedAt = Tds.ToString(XFConstants.DateTimeFormat);
            if (list.ContainsKey<Guid>(XFConstants.EventWriter.MessageId))
            {
                MessageId = list.Get<Guid>(XFConstants.EventWriter.MessageId);
            }
            if (list.ContainsKey<string>("sessionid"))
            {
                SessionId = list.Get<string>("sessionid");
            }
            if (list.ContainsKey<string>(XFConstants.Context.PrimaryScope))
            {
                PrimaryScope = list.Get<string>(XFConstants.Context.PrimaryScope);
            }
            if (list.ContainsKey<string>(XFConstants.Context.SecondaryScope))
            {
                SecondaryScope = list.Get<string>(XFConstants.Context.SecondaryScope);
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
            if (list.ContainsKey<string>("xf.bigdata.usecase"))
            {
                UseCase = list.Get<string>("xf.bigdata.usecase");
            }
            if (list.ContainsKey<DateTimeOffset>("metric.event.start"))
            {
                Start = list.Get<DateTimeOffset>("metric.event.start");
            }
            if (list.ContainsKey<double>("metric.event.elapsed"))
            {
                Elapsed = list.Get<double>("metric.event.elapsed");
            }
            if (list.ContainsKey<int>("usecase.resultset.count"))
            {
                ResultsetCount = list.Get<int>("usecase.resultset.count");
            }
            if (list.ContainsKey<string>("request.uri"))
            {
                Uri = list.Get<string>("request.uri");
            }

            if (list.ContainsKey<string>("event.outcome"))
            {
                EventOutcome = list.Get<string>("event.outcome");
            }
            if (list.ContainsKey<Guid>("app.context.instance"))
            {
                AppContextInstance = list.Get<Guid>("app.context.instance").ToString();
            }
            if (list.ContainsKey<string>("app.context.instance"))
            {
                AppContextInstance = list.Get<string>("app.context.instance").ToString();
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
