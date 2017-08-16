// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class SQLCommand
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("command")]
        public string Command { get; set; }

        [XmlAttribute("type")]
        public string CommandType { get; set; }

        public List<Parameter> Parameters;
    }
}
