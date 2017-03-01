// <copyright company="eXtensible Solutions LLC" file="eXTask.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class eXTask : eXBase
    {
        [DataMember]
        public string TaskId { get; set; }

        [DataMember]
        public string MasterId { get; set; }

        [DataMember]
        public string TaskType { get; set; }

        [DataMember]
        public string TaskName { get; set; }

        [DataMember]
        public string TaskOutcome { get; set; }

        [DataMember]
        public DateTimeOffset Start { get; set; }
        [DataMember]
        public double Elapsed { get; set; }

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


        public eXTask() { }

        public eXTask(List<TypedItem> list)
        {
            this.Items = list;
            this.Tds = DateTime.Now;
            CreatedAt = Tds.ToString(XFConstants.DateTimeFormat);
            this.Message = "Task";
            if (list.ContainsKey<Guid>(XFConstants.EventWriter.MessageId))
            {
                MessageId = list.Get<Guid>(XFConstants.EventWriter.MessageId);
            }
            if (list.ContainsKey<string>(XFConstants.Context.ZONE))
            {
                Zone = list.Get<string>(XFConstants.Context.ZONE);
            }
            if (list.ContainsKey<string>(XFConstants.Context.Application))
            {
                ApplicationKey = list.Get<string>(XFConstants.Context.Application);
            }
            if (list.ContainsKey<string>(XFConstants.EventWriter.Message))
            {
                Message = list.Get<string>(XFConstants.EventWriter.Message);
            }
            if (list.ContainsKey<DateTimeOffset>("metric.event.start"))
            {
                Start = list.Get<DateTimeOffset>("metric.event.start");
            }
            if (list.ContainsKey<double>("metric.event.elapsed"))
            {
                Elapsed = list.Get<double>("metric.event.elapsed");
            }
            if (list.ContainsKey<string>(XFConstants.TaskWriter.TaskId))
            {
                TaskId = list.Get<string>(XFConstants.TaskWriter.TaskId);
            }
            if (list.ContainsKey<string>(XFConstants.TaskWriter.TaskMasterId))
            {
                MasterId = list.Get<string>(XFConstants.TaskWriter.TaskMasterId);
            }
            if (list.ContainsKey<string>(XFConstants.TaskWriter.TaskType))
            {
                TaskType = list.Get<string>(XFConstants.TaskWriter.TaskType);
            }
            if (list.ContainsKey<string>(XFConstants.TaskWriter.TaskName))
            {
                TaskName = list.Get<string>(XFConstants.TaskWriter.TaskName);
            }
            if (list.ContainsKey<string>("event.outcome"))
            {
                TaskOutcome = list.Get<string>("event.outcome");
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
