using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XF.WebApi.Core
{
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
