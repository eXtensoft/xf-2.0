// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{

    using System;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
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
