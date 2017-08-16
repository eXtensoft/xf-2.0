// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class ModelAction
    {
        [XmlAttribute("verb")]
        public ModelActionOption Verb { get; set; }

        [XmlAttribute("strategy")]
        public StrategyOption Strategy { get; set; }

        [XmlAttribute("sqlCommandKey")]
        public string SqlCommandKey { get; set; }

        public List<Switch> Switches { get; set; }
    }
}
