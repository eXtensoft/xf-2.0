// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Db
{
    using System;
    using System.Collections.Generic;
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
