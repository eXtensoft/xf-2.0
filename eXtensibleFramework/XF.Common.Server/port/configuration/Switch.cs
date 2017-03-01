// <copyright company="eXtensible Solutions, LLC" file="Switch.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
