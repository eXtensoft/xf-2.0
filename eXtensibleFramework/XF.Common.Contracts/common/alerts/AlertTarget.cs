// <copyright company="eXtensible Solutions, LLC" file="AlertTarget.cs">
// Copyright © 2015 All Right Reserved
// </copyright>


namespace XF.Common
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class AlertTarget
    {
        [DataMember]
        [XmlAttribute("key")]
        public string Key { get; set; }

        [DataMember]
        [XmlAttribute("operator")]
        public OperatorTypeOption Operator { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }

    }
}
