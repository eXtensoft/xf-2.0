// <copyright company="eXtensible Solutions LLC" file="eXStatus.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class eXStatus : eXBase
    {

        [DataMember]
        public string ModelId { get; set; }

        [DataMember]
        public string ModelType { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Description { get; set; }

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

        public eXStatus() { }

        public eXStatus(List<TypedItem> list)
        {
            this.Items = list;
            this.Tds = DateTime.Now;
            Year = Tds.Year;
            Month = Tds.Month;
            Day = Tds.Day;
            DayOfWeek = Tds.DayOfWeek.ToString();
            Hour = Tds.Hour;
            Minute = Tds.Minute;
            CreatedAt = Tds.ToString(XFConstants.DateTimeFormat);
            if (list.ContainsKey<Guid>(XFConstants.EventWriter.MessageId))
            {
                MessageId = list.Get<Guid>(XFConstants.EventWriter.MessageId);
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
            if (list.ContainsKey<string>(XFConstants.Status.Description))
            {
                Description = list.Get<string>(XFConstants.Status.Description);
            }
            if (list.ContainsKey<string>(XFConstants.Status.ModelId))
            {
                ModelId = list.Get<string>(XFConstants.Status.ModelId);
            }
            if (list.ContainsKey<string>(XFConstants.Status.ModelName))
            {
                ModelName = list.Get<string>(XFConstants.Status.ModelName);
            }
            if (list.ContainsKey<string>(XFConstants.Status.ModelType))
            {
                ModelType = list.Get<string>(XFConstants.Status.ModelType);
            }
            if (list.ContainsKey<string>(XFConstants.Status.StatusText))
            {
                Status = list.Get<string>(XFConstants.Status.StatusText);
            }


        }
    }
}
