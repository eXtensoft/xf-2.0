// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    public class ListItem
    {
        [XmlAttribute("group")]
        [DataMember]
        public string Group { get; set; }

        [XmlAttribute("tds")]
        [DataMember]
        public DateTime Tds { get; set; }

        [XmlElement]
        [DataMember]
        public string Display { get; set; }

        [XmlElement("Item")]
        [DataMember]
        public List<TypedItem> Items { get; set; }

        public ListItem()
        {
            Items = new List<TypedItem>();
        }
    }
}
