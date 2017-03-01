// <copyright company="eXtensible Solutions, LLC" file="AlertSubscriber.cs">
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
    [XmlRoot(ElementName="Subscriber")]
    public class AlertSubscriber
    {
        public AlertSubscriber()
        {
            BeginAt = DateTime.Now;
        }

        //[DataMember]
        //[XmlAttribute("id")]
        //public string SubscriberId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [XmlElement(ElementName="Alert")]
        public List<AlertInterest> Interests { get; set; }

        [DataMember]
        [XmlAttribute("begin")]
        public DateTime BeginAt { get; set; }

    }
}
