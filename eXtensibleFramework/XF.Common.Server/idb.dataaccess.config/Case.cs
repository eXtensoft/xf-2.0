

namespace XF.Common.Db
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class Case
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("dBCommandKey")]
        public string CommandKey { get; set; }

    }
}
