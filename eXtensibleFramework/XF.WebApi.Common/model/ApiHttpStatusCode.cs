// <copyright company="eXtensoft, LLC" file="ApiHttpStatusCode.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Runtime.Serialization;

    [DataContract]
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
