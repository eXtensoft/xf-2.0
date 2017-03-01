

namespace XF.Common.Db
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using XF.Common;

    [Serializable]
    public class ModelAction
    {
        [XmlAttribute("verb")]
        public ModelActionOption Verb { get; set; }

        [XmlAttribute("dbCommandKey")]
        public string DbCommandKey { get; set; }

        public List<Switch> Switches { get; set; }

    }
}
