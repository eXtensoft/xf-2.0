// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
    public class DbCommand
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("type")]
        public string CommandType { get; set; }

        [XmlElement]
        public string CommandText { get; set; }

        public List<Parameter> Parameters;

    }
}
