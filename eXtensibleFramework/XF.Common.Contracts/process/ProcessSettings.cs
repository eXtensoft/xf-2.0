

namespace XF.Quality
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [Serializable]
    public class ProcessSettings
    {
        [XmlElement("Setting")]
        public List<ProcessSetting> Settings { get; set; }        
    }
}
