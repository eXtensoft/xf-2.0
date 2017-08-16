// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
