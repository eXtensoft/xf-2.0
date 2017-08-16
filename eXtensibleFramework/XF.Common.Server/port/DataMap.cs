// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Discovery
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class DataMap
    {
        [XmlAttribute("databaseType")]
        public string DatabaseType { get; set; }

        [XmlAttribute("dbType")]
        public System.Data.DbType DataAccessType { get; set; }

        [XmlAttribute("readerAccessor")]
        public string DataReaderAccessor { get; set; }

        [XmlAttribute("clrType")]
        public string ClrType
        {
            get
            {
                return SystemType.FullName;
            }
            set
            {
                SystemType = Type.GetType(value);
            }

        }
        [XmlIgnore]
        public Type SystemType { get; set; }

    }
}
