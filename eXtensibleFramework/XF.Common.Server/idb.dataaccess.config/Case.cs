// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Db
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class Case
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("dBCommandKey")]
        public string CommandKey { get; set; }

    }
}
