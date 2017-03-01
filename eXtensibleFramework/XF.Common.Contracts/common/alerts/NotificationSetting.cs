// <copyright company="eXtensible Solutions, LLC" file="NotificationSetting.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{

    using System;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class NotificationSetting
    {
        [DataMember]
        [XmlAttribute("method")]
        public CommunicationTypeOption Method { get; set; }

        [DataMember]
        [XmlText]
        public string Text { get; set; }

    }
}
