// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Switch
    {
        [XmlAttribute("criteriaKey")]
        public string CriteriaKey { get; set; }

        [XmlAttribute("dataType")]
        public string DataType { get; set; }

        public List<Case> Cases { get; set; }
    }
}
