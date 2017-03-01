

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
    public class DbConfig
    {
        [XmlAttribute("appKey")]
        public string AppContextKey { get; set; }

        private List<Model> _Models = new List<Model>();
        public List<Model> Models
        {
            get { return _Models; }
            set { _Models = value; }
        }
    }
}
