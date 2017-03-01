// <copyright company="eXtensible Solutions, LLC" file="TypedItem.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    [Serializable]
    public class TypedItem
    {
        [DataMember]
        [XmlAttribute("key")]
        public string Key { get; set; }

        [DataMember]
        [XmlAttribute("domain")]
        public string Domain { get; set; }

        [DataMember]
        [XmlAttribute("scope")]
        public string Scope { get; set; }

        [IgnoreDataMember]
        [XmlIgnore]
        public DateTimeOffset Tds { get; set; }

        [DataMember]
        [XmlAttribute("txt")]
        public string Text { get; set; }

        [DataMember]
        [XmlAttribute("op")]
        public OperatorTypeOption Operator { get; set; }

        [DataMember]
        public object Value { get; set; }



        #region constructors

        public TypedItem()
        {
        }

        public TypedItem(string key, object value)
        :this(key,value, OperatorTypeOption.EqualTo){}

        public TypedItem(string key, object value, OperatorTypeOption op)
        {
            Key = key;
            Value = value;
            Tds = DateTime.Now;
            Operator = op;
        }

        #endregion constructors

        public override string ToString()
        {
            string valueString = Value != null ? Value.ToString() : "{x:Null}";
            string keyString = !String.IsNullOrEmpty(Key) ? Key : "noKey";
            string valueType = Value != null ? Value.GetType().Name : "noType";
            return String.Format("{0}<{1}> : {2}", keyString, valueType, valueString);
        }

        [IgnoreDataMember]
        [XmlIgnore]
        public string Display
        {
            get { return ToString(); }
        }

    }
}
