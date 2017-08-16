// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Db
{
    using System;
    using System.Data;
    using System.Xml;
    using System.Xml.Serialization;

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
