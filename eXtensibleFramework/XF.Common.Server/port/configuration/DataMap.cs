// <copyright company="eXtensible Solutions, LLC" file="DataMap.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Data;
    using System.Xml.Serialization;

    [Serializable]
    public class DataMap
    {
        [XmlAttribute("columnName")]
        public string ColumnName { get; set; }

        [XmlAttribute("dataType")]
        public DbType DataType { get; set; }

        [XmlAttribute("propertyName")]
        public string PropertyName { get; set; }

        [XmlAttribute("isNullable")]
        public bool IsNullable { get; set; }

        [XmlAttribute("isReadOnly")]
        public bool IsReadOnly { get; set; }

        public override string ToString()
        {
            string column = String.Format("ColumnName = {0}", ColumnName);
            string datatype = String.Format("DataType = {0}", DataType.ToString());
            string property = String.Format("PropertyName = {0}", PropertyName);
            return String.Format("{0}{1}{2}", column.PadRight(30), datatype.PadRight(30), property.PadRight(30));
        }
    }
}
