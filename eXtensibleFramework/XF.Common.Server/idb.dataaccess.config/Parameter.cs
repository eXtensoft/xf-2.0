

namespace XF.Common.Db
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.Xml.Serialization;
    using System.Xml;

    [Serializable]
    public class Parameter
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("dataType")]
        public DbType DataType { get; set; }

        [XmlAttribute("mode")]
        public string Mode { get; set; }

        [XmlAttribute("allowsNull")]
        public bool AllowsNull { get; set; }

        [XmlAttribute("target")]
        public string Target { get; set; }
    }
}
