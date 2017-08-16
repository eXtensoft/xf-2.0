// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
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
