using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Styx.ProjectManagement
{
    [KnownType(typeof(Project))]
    [XmlInclude(typeof(Project))]
    [DataContract]
    [Serializable]
    public class Item
    {
        public int ItemId { get; set; }

        public Guid ItemGuid { get; set; }

        public string GroupName { get; set; }

        public string GroupToken { get; set; }

        public string ItemTypeToken { get; set; }

        public string ItemToken { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public string CreatedBy { get; set; }

        public bool HasNotes { get; set; }
    }
}
