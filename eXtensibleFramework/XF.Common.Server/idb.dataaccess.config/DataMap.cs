// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Db
{
    using System;
    using System.Data;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class DataMap
    {
        [XmlAttribute("dataType")]
        public DbType DataType { get; set; }

        [XmlAttribute("column")]
        public string Column { get; set; }

        [XmlAttribute("property")]
        public string Property { get; set; }

        [XmlAttribute("isNullable")]
        public bool IsNullable { get; set; }

        public override string ToString()
        {
            string column = String.Format("Column = {0}", Column);
            string datatype = String.Format("DataType = {0}", DataType.ToString());
            string property = String.Format("Property = {0}", Property);
            return String.Format("{0}{1}{2}", column.PadRight(25), datatype.PadRight(20), property.PadRight(25));
        }
    }
}
