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
