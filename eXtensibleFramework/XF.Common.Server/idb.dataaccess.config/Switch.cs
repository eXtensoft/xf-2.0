

namespace XF.Common.Db
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using System.Xml;

    [Serializable]
    public class Switch
    {
        [XmlAttribute("criterionKey")]
        public string CriterionKey { get; set; }

        [XmlAttribute("dataType")]
        public string DataType { get; set; }

        public List<Case> Cases { get; set; }

    }
}
