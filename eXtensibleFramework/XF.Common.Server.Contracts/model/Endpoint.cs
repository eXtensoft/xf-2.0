// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace XF.WebApi.Core
{

    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class Endpoint
    {
        [XmlAttribute("sortOrder")]
        public int SortOrder { get; set; }
        [XmlAttribute("version")]
        public int Version { get; set; }
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("Whitelist")]
        public string WhitelistPattern { get; set; }
        [XmlElement("Pattern")]
        public string RoutePattern { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlIgnore]
        public bool IsFirst { get; set; }
        [XmlIgnore]
        public bool IsLast { get; set; }
    }
}
