// <copyright company="eXtensible Solutions, LLC" file="SOLCommand.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
