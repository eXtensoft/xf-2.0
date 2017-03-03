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
    public class EstimateValue
    {
        [DataMember]
        [XmlAttribute("id")]
        public string EstimateValueId { get; set; }
        [DataMember]
        public string Scale { get; set; }
        [DataMember]
        public int Val { get; set; }          
        [DataMember]
        public DateTime CreatedOn { get; set; }
        [DataMember]
        public string CreatedByRole { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
    }
}
