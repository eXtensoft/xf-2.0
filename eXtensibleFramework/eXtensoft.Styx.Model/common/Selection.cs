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
    public sealed class Selection
    {
        [DataMember]
        public int SelectionId { get; set; }
        [DataMember]
        public string Display { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public int Sort { get; set; }
        [DataMember]
        public int GroupId { get; set; }
        [DataMember]
        public int MasterId { get; set; }
        [DataMember]
        public string Icon { get; set; }
    }
}
