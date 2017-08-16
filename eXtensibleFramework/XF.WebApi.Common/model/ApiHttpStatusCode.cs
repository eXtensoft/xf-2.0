// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    [Serializable]
    public sealed class ApiHttpStatusCode
    {
        [DataMember]
        [XmlAttribute("code")]
        public int Code { get; set; }

        [DataMember]
        [XmlAttribute("name")]
        public string Name { get; set; }


        [DataMember]
        [XmlAttribute("available")]
        public bool IsAvailable { get; set; }

        [DataMember]
        [XmlAttribute("success")]
        public bool IsSuccess { get; set; }
        [DataMember]
        public string Summary { get; set; }

    }

}
