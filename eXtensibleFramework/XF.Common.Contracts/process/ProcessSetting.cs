
namespace XF.Quality
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class ProcessSetting
    {
        [XmlAttribute("ctx")]
        public string Context { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement]
        public string ApiRoot { get; set; }

        [XmlElement("Datastore")]
        public List<DatastoreSetting> Datastores { get; set; }
    }
}
