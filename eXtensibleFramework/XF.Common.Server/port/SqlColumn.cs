

namespace XF.Common.Discovery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;
    using System.Xml.Serialization;

    public class SqlColumn
    {
        public string ColumnName { get; set; }

        public int OrdinalPosition { get; set; }

        public string DefaultValue { get; set; }

        public bool IsNullible { get; set; }

        public string Datatype { get; set; }

        public int MaxLength { get; set; }

        public bool IsComputed { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsForeignKey { get; set; }

        public bool IsSelected { get; set; }

        private static string[] _TextTypes = { "varchar", "nvarchar", "ntext", "char" };

        [XmlIgnore]
        public string ToDisplay { get { return ToString(); } }
        public SqlColumn()
        {
            IsSelected = true;
        }
        public SqlColumn(DataRow row)
        {
            ColumnName = row.ItemArray[3].ToString();
            OrdinalPosition = (int)row.ItemArray[4];
            DefaultValue = row.ItemArray[5].ToString();
            IsNullible = (bool)row.ItemArray[6];
            Datatype = row.ItemArray[7].ToString();
            MaxLength = (int)row.ItemArray[8];
            IsComputed = (row.ItemArray[9] == DBNull.Value) ? false : (bool)row.ItemArray[9];
            IsIdentity = (row.ItemArray[11] == DBNull.Value) ? false : (bool)row.ItemArray[11];
            IsPrimaryKey = (bool)row.ItemArray[13];
            IsForeignKey = (bool)row.ItemArray[14];
            IsSelected = true;
        }
        public SqlColumn(SqlDataReader reader)
        {
            ColumnName = reader.GetString(3);
            OrdinalPosition = reader.GetInt32(4);
            if (reader[5] != DBNull.Value)
            {
                DefaultValue = reader.GetString(5);
            }
            IsNullible = reader.GetBoolean(6);
            Datatype = reader.GetString(7);
            if (reader[8] != DBNull.Value)
            {
                MaxLength = reader.GetInt32(8);
            }

            IsComputed = (reader[9] == DBNull.Value) ? false : reader.GetBoolean(9);
            IsIdentity = (reader[11] == DBNull.Value) ? false : reader.GetBoolean(11);
            IsPrimaryKey = reader.GetBoolean(13);
            IsForeignKey = reader.GetBoolean(14);
            IsSelected = true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(ColumnName + " (");
            if (IsPrimaryKey)
            {
                sb.Append("PK, ");
            }
            sb.Append(Datatype.ToString());
            if (_TextTypes.Contains(Datatype.ToLower()))
            {
                string max = (MaxLength == -1) ? "MAX" : MaxLength.ToString();
                sb.Append(String.Format("({0})", max));
            }
            sb.Append((IsNullible) ? ", null)" : ", not null)");
            return sb.ToString();
        }

        public string ToSprocDatatype()
        {
            StringBuilder sb = new StringBuilder(Datatype.ToString());
            if (_TextTypes.Contains(Datatype.ToLower()))
            {
                string max = (MaxLength == -1) ? "MAX" : MaxLength.ToString();
                sb.Append(String.Format("({0})", max));
            }
            //if (IsNullible)
            //{
            //    sb.Append(" = NULL");
            //}
            return sb.ToString();
        }

        public bool NameNotIn(string[] excludedNames)
        {
            return !excludedNames.Contains(ColumnName.ToLower());
        }

        public string ToSqlParam()
        {
            string format = String.Empty;
            if (IsNullible)
            {
                format = "@{0} {1} = NULL";
            }
            else
            {
                format = "@{0} {1}";
            }
            return String.Format(format, ColumnName, Datatype.ToString().ToUpper());
        }
        public string ToReaderType()
        {
            //string t = Datatype.ToLower();
            //string s = Datatype;
            //if (t.Contains("date"))
            //{
            //    s = "DateTime";
            //}
            return DataMapProvider.TranslateToSqlAssessor(Datatype);
        }

    }
}
