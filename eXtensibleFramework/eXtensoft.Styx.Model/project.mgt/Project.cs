
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
    public class Project : Item
    {
        [DataMember]
        [XmlIgnore]
        public string ProjectId { get; set; }

        [DataMember]
        [XmlAttribute("group")]
        public string Group { get; set; }

        [DataMember]
        public string Title { get; set; }


        [DataMember]
        public BusinessCase Business { get; set; }

        [DataMember]
        public ProjectScope Scope { get; set; }

        [DataMember]
        public List<Feature> Features { get; set; }

        [DataMember]
        public List<Task> Tasks { get; set; }

    }
}
