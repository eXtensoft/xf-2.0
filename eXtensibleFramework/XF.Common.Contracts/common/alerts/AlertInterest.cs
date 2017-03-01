// <copyright company="eXtensible Solutions, LLC" file="AlertInterest.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class AlertInterest
    {
        [XmlElement(ElementName="Target")]
        public List<AlertTarget> Targets { get; set; }

        public NotificationSetting Notification { get; set; }

        [XmlAttribute("lifespan")]
        public int LifespanInHours { get; set; }

        [XmlAttribute("suspension")]
        public SuspensionModeOption Suspension { get; set; }

        [XmlAttribute("begin")]
        public DateTime Begin { get; set; }
    }

}
