using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XF.Common
{
    [Serializable]
    public class DbMap
    {
        [XmlAttribute("databaseType")]
        public string DatabaseType { get; set; }

        [XmlAttribute("dbType")]
        public System.Data.DbType DataAccessType { get; set; }

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
        [XmlAttribute("sqlType")]
        public SqlDbType SqlType { get; set; }


        [XmlIgnore]
        public Type SystemType { get; set; }

    }

    public class DbMapCollection : KeyedCollection<System.Type,DbMap>
    {

        protected override Type GetKeyForItem(DbMap item)
        {
            return item.SystemType;
        }

        private static HashSet<Type> _ScalarTypes = new HashSet<Type>()
        {
            //reference types
            typeof(String),
            typeof(Byte[]),
            //value types
            typeof(Byte),
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),
            typeof(DateTime),
            typeof(Guid),
            typeof(Boolean),
            typeof(TimeSpan),
            //nullable value types
            typeof(Byte?),
            typeof(Int16?),
            typeof(Int32?),
            typeof(Int64?),
            typeof(Single?),
            typeof(Double?),
            typeof(Decimal?),
            typeof(DateTime?),
            typeof(Guid?),
            typeof(Boolean?),
            typeof(TimeSpan?)            
        };
    }


}
