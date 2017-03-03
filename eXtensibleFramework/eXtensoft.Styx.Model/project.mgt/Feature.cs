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
    public class Feature
    {
        [DataMember]
        [XmlAttribute("id")]
        public string FeatureId { get; set; }
     

        [DataMember]
        [XmlAttribute("scope")]
        public string Scope { get; set; }

        [DataMember]
        public FeatureTypeOption FeatureType { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
