// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.Common.BulkCopy
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Xml;

    public sealed class ListReaderWriter<T> where T : class, new()
    {
        #region static fields

        private static HashSet<Type> _ScalarTypes = new HashSet<Type>()
        {
            //reference types
            typeof(String),
            typeof(Byte[]),
            typeof(XmlDocument),
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
            typeof(DateTimeOffset),
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
            typeof(TimeSpan?),
            typeof(DateTimeOffset?),
        };



        #endregion

        private ListReaderWriterOptions _Options;

        private FieldCollection _Fields = new FieldCollection();

        private IEnumerable<T> _List;

        private string _Tablename = String.Empty;

        private IDictionary<string, string> _Mappings;

        public ListReaderWriter(IEnumerable<T> list)
        : this(list, ListReaderWriterOptions.Default)
        {

        }

        public ListReaderWriter(IEnumerable<T> list, ListReaderWriterOptions options)
            :this(list,options,""){}

        public ListReaderWriter(IEnumerable<T> list, ListReaderWriterOptions options, string destinationTablename)
            :this(list,options,destinationTablename,null){}

        public ListReaderWriter(IEnumerable<T> list, ListReaderWriterOptions options, string destinationTablename,IDictionary<string, string> mappings)
        {
            _List = list;
            _Options = options;
            _Tablename = destinationTablename;
            _Mappings = mappings;

            var q = from x in typeof(T).GetProperties()
                    where _ScalarTypes.Contains(x.PropertyType) && x.CanRead
                    select new Field<T>(x);
            List<Field<T>> l = new List<Field<T>>();
            foreach (var x in typeof(T).GetProperties())
            {
                if (_ScalarTypes.Contains(x.PropertyType) && x.CanRead)
                {
                    l.Add(new Field<T>(x));
                }
            }

            FieldCollection c = new FieldCollection();
            foreach (var item in q)
            {
                if (_Mappings != null && _Mappings.ContainsKey(item.Name))
                {
                    item.Target = _Mappings[item.Name];
                }
                else
                {
                    item.Target = item.Name;
                }
                _Fields.Add(item);
            }
        }


        public bool BulkInsert(SqlConnection cn)
        {
            return BulkInsert(cn, null);
        }

        public bool BulkInsert(SqlConnection cn, string destinationTablename)
        {
            return BulkInsert(cn, destinationTablename, null);
        }

        public bool BulkInsert(SqlConnection cn, string destinationTablename, IDictionary<string,string> mappings)
        {
            return BulkInsert(cn, destinationTablename, null, 10000);
        }

        public bool BulkInsert(SqlConnection cn, string destinationTablename, IDictionary<string,string> mappings, int batchSize = 10000)
        {
            bool b = false;

            try
            {
                var reader = _List.AsDataReader();
                using (cn)
                {
                    cn.Open();
                    SqlBulkCopy sbc = new SqlBulkCopy(cn, SqlBulkCopyOptions.Default, null);
                    if (mappings == null)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);
                            sbc.ColumnMappings.Add(columnName, columnName);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string sourceName = reader.GetName(i);
                            string destinationName = (mappings.ContainsKey(sourceName)) ? mappings[sourceName] : sourceName;
                            sbc.ColumnMappings.Add(sourceName, destinationName);
                        }
                    }
                    sbc.BatchSize = batchSize;
                    sbc.DestinationTableName = destinationTablename;
                    sbc.WriteToServer(reader);
                    sbc.Close();
                    b = true;
                }
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralException(ex, "Error in ExecuteBulkCopy", this.GetType().FullName);
            }




            return b;
        }



        public void ExecutProfile(int maxDistinctLimit)
        {
            foreach (var fld in _Fields)
            {
                fld.MaxDistinctLimit = maxDistinctLimit;
            }
            foreach (T t in _List)
            {
                foreach (var field in _Fields)
                {
                    field.Execute(t);
                }
            }
        }

        public string CreateTableDDL(decimal paddingFactor, string tableName, IDictionary<string,string> mappings)
        {
            StringBuilder insert = new StringBuilder();
            StringBuilder insertValues = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            _Tablename = !String.IsNullOrWhiteSpace(tableName) ? tableName : typeof(T).Name;
            _Mappings = mappings;
            
            sb.AppendLine(String.Format("CREATE TABLE [{0}].[{1}](","dbo",_Tablename));
            insert.AppendLine(String.Format("insert into [{0}].[{1}] (", "dbo", _Tablename));
            int i = 0;
            foreach (var item in _Fields)
            {
                string column = (mappings != null && mappings.ContainsKey(item.Name)) ? mappings[item.Name] : item.Name;

                string nullText = item.IsNullable ? "NULL" : "NOT NULL";
                sb.AppendLine(String.Format("\t[{0}] {1} {2},",column,item.ToColumn(paddingFactor),nullText));                
                if (i++ > 0)
                {
                    insert.Append(",");
                    insertValues.Append(",");
                }
                insert.AppendFormat("[{0}]",column);
                insertValues.AppendFormat("@{0}", column);
            }
            insert.Append(") values (");
            insert.AppendFormat("{0})", insertValues.ToString());

            sb.AppendLine(") ON [PRIMARY]");
            sb.AppendLine("GO");
            //sb.AppendLine(insert.ToString());

            return sb.ToString();
        }




        #region private classes



        private class FieldCollection : KeyedCollection<string,Field<T>>
        {
            protected override string GetKeyForItem(Field<T> item)
            {
                return item.Name;
            }
        }

        private class Field<T>
        {
            public int Index { get; set; }
            public Type FieldType { get; set; }
            public string Name { get; set; }
            public int HasData { get; set; }
            public int HasNoData { get; set; }
            public int DistinctCount { get; set; }
            public int MaxLength { get; set; }
            public int MaxDistinctLimit { get; set; }
            public ProfileItemCollection Items { get; set; }

            public SqlDbType SqlType { get; set; }
            public DbType DatabaseType { get; set; }
            public bool IsNullable
            {
                get
                {
                    return HasNoData > 0;
                }
            }
            public string Target { get; set; }
            public Nullable<int> TargetMaxLength { get; set; }

            Func<T, object> Accessor { get; set; }

            public Field(PropertyInfo info)
            {
                MethodInfo xmlconversion = typeof(Field<T>).GetMethod("ConvertToString");
                Name = info.Name;
                FieldType = info.PropertyType;
                SqlType = _TypeMaps[FieldType].SqlType;
                var param = Expression.Parameter(typeof(T), "obj");
                var member = Expression.PropertyOrField(param, info.Name);
                var obj = (!info.PropertyType.Name.Equals("XmlDocument")) ? Expression.Convert(member, typeof(object)) : Expression.Convert(member, typeof(object),xmlconversion);
                var lambda = Expression.Lambda<Func<T, object>>(obj, param);
                Accessor = lambda.Compile();
                Items = new ProfileItemCollection();
            }

            public static string XmlToString(XmlDocument xml)
            {
                if (xml.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    var declaration = (XmlDeclaration)xml.FirstChild;
                    declaration.Encoding = "UTF-16";
                }
                xml.DocumentElement.RemoveAttribute("xmlns:xsi");
                xml.DocumentElement.RemoveAttribute("xmlns:xsd");
                return xml.OuterXml;
            }

            public string ToColumn(decimal paddingFactor)
            {
                string s = String.Empty;
                switch (SqlType)
                {
                    case SqlDbType.DateTime2:
                        s = "[datetime2](7)";
                        break;
                    case SqlDbType.DateTimeOffset:
                        s = "[datetimeoffset](7)";
                        break;
                    case SqlDbType.Decimal:
                        s = "[decimal](18,0)";
                        break;
                                       
                    case SqlDbType.NText:
                        s = "[ntext]";
                        break;
                    case SqlDbType.Time:
                        s = "[time](7)";
                        break;
                    case SqlDbType.VarChar:
                    case SqlDbType.VarBinary:
                    case SqlDbType.NVarChar:
                    case SqlDbType.NChar:
                    case SqlDbType.Char:
                    case SqlDbType.Binary:
                        if (MaxLength > 0)
                        {
                            decimal max = (decimal)MaxLength * paddingFactor;
                            int maxlength = (int)max;
                            s = String.Format("[{0}]({1})", SqlType.ToString(), maxlength);
                        }
                        break;
                    case SqlDbType.Xml:
                    case SqlDbType.UniqueIdentifier:
                    case SqlDbType.TinyInt:
                    case SqlDbType.Timestamp:
                    case SqlDbType.Text:
                    case SqlDbType.Real:
                    case SqlDbType.SmallDateTime:
                    case SqlDbType.SmallInt:
                    case SqlDbType.SmallMoney:
                    case SqlDbType.Int:
                    case SqlDbType.Money:
                    case SqlDbType.Image:
                    case SqlDbType.Float:
                    case SqlDbType.DateTime:
                    case SqlDbType.Date:
                    case SqlDbType.Bit:
                    case SqlDbType.BigInt:
                    default:
                        s = String.Format("[{0}]",SqlType.ToString().ToLower());
                        break;
                }
                return s;
            }

            public void Execute(T item)
            {
                object o = Accessor.Invoke(item);
                if (o == null)
                {
                    HasNoData++;
                }
                else
                {
                    string s = o.ToString();
                    if (s.Length > MaxLength)
                    {
                        MaxLength = s.Length;
                    }
                    if (String.IsNullOrEmpty(s))
                    {
                        HasNoData++;
                    }
                    else if (MaxDistinctLimit == -1)
                    {
                        HasData++;
                    }
                    else
                    {
                        HasData++;
                        if (Items.Count <= MaxDistinctLimit)
                        {
                            if (!Items.Contains(s))
                            {
                                Items.Add(new ProfileItem() { Key = s });
                            }
                            Items[s].Count++;
                        }
                        else
                        {
                            Items.Clear();
                            MaxDistinctLimit = -1;
                        }
                    }
                }

            }


            private static IDictionary<System.Type, DbMap> _TypeMaps = new Dictionary<System.Type, DbMap>
            {
                {typeof(System.String),new DbMap{ SystemType = typeof(System.String), SqlType = SqlDbType.NVarChar}},
                {typeof(System.Byte),new DbMap{ SystemType = typeof(System.Byte), SqlType = SqlDbType.Binary}},
                {typeof(System.Int16),new DbMap{ SystemType = typeof(System.Int16), SqlType = SqlDbType.SmallInt}},
                {typeof(System.Int32),new DbMap{ SystemType = typeof(System.Int32), SqlType = SqlDbType.Int}},
                {typeof(System.Int64),new DbMap{ SystemType = typeof(System.Int64), SqlType = SqlDbType.BigInt}},
                {typeof(System.Single),new DbMap{ SystemType = typeof(System.Single), SqlType = SqlDbType.Real}},
                {typeof(System.Double),new DbMap{ SystemType = typeof(System.Double), SqlType = SqlDbType.Float}},
                {typeof(System.Decimal),new DbMap{ SystemType = typeof(System.Decimal), SqlType = SqlDbType.Decimal}},
                {typeof(System.DateTime),new DbMap{ SystemType = typeof(System.DateTime), SqlType = SqlDbType.DateTime}},
                {typeof(System.Guid),new DbMap{ SystemType = typeof(System.Guid), SqlType = SqlDbType.UniqueIdentifier}},
                {typeof(System.Boolean),new DbMap{ SystemType = typeof(System.Boolean), SqlType = SqlDbType.Bit}},
                {typeof(System.TimeSpan),new DbMap{ SystemType = typeof(System.TimeSpan), SqlType = SqlDbType.Time}},
                {typeof(System.DateTimeOffset),new DbMap{ SystemType = typeof(System.DateTimeOffset),SqlType = SqlDbType.DateTimeOffset}},
                {typeof(Nullable<System.DateTime>),new DbMap{SystemType = typeof(System.DateTime?), SqlType = SqlDbType.DateTime}},
                {typeof(System.Xml.XmlDocument),new DbMap{SystemType=typeof(System.Xml.XmlDocument), SqlType = SqlDbType.Xml}},               
            };
        }

        private class ProfileItem
        {
            public string Key { get; set; }
            public int Count { get; set; }
            public double Percent { get; set; }
            public double CumulativePercent { get; set; }
        }

        private class ProfileItemCollection : KeyedCollection<string, ProfileItem>
        {
            protected override string GetKeyForItem(ProfileItem item)
            {
                return item.Key;
            }
        }


        #endregion












    }
}
