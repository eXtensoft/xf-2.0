// <copyright company="eXtensible Solutions, LLC" file="Case.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Case
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("sqlCommandKey")]
        public string SqlCommandKey { get; set; }
    }
}
