using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Styx.ProjectManagement
{
    [DataContract]
    [Serializable]
    public class ListItem
    {
        [DataMember]
        [XmlAttribute("id")]
        public string Id { get; set; }
        [DataMember]
        [XmlAttribute("token")]
        public string Token { get; set; }
        [DataMember]
        [XmlAttribute("typ")]
        public string ListItemType { get; set; }
        [DataMember]
        [XmlAttribute("status")]
        public string Status { get; set; }
        [DataMember]
        [XmlAttribute("intVal")]
        public int IntVal { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Display { get; set; }
        [DataMember]
        public string DisplayAlt { get; set; }
        [DataMember]
        public string Grouping { get; set; }
        [DataMember]
        public int SortOrder { get; set; }
        [DataMember]
        public string Icon { get; set; }
        [DataMember]
        [XmlAttribute("masterId")]
        public string MasterId { get; set; }

    }
}
