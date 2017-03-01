

namespace XF.Common.Db
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class Model
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("modelType")]
        public string modelType { get; set; }

        public List<ModelAction> ModelActions { get; set; }

        public List<DbCommand> Commands { get; set; }

        public List<DataMap> DataMaps { get; set; }

    }
}
