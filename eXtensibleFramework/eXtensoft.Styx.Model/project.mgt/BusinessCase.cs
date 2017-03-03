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
    public class BusinessCase
    {
        [DataMember]
        [XmlAttribute("id")]
        public string BusinessCaseId { get; set; }
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string ValueProposition { get; set; }
        [DataMember]
        public string ProblemStatement { get; set; }

    }
}
