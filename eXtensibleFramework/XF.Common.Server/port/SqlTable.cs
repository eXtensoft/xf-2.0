// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Discovery
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public class SqlTable
    {
        public string Catalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }

        public List<SqlColumn> Columns { get; set; }

        public SqlTable(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                Catalog = row.ItemArray[0].ToString();
                TableSchema = row.ItemArray[1].ToString();
                TableName = row.ItemArray[2].ToString();
                Columns = new List<SqlColumn>();
                foreach (DataRow item in dataTable.Rows)
                {
                    Columns.Add(new SqlColumn(item));
                }
            }
        }
        public SqlTable(SqlDataReader reader)
        {
            int i = 0;
            while (reader.Read())
            {
                if (i == 0)
                {
                    Catalog = reader[0].ToString();
                    TableSchema = reader[1].ToString();
                    TableName = reader[2].ToString();
                    Columns = new List<SqlColumn>();
                    i++;
                }
                Columns.Add(new SqlColumn(reader));
            }
        }

        public SqlTable(string catalog, string viewSchema, string viewName, DataTable dt)
        {
            if (dt != null)
            {
                Catalog = catalog;
                TableSchema = viewSchema;
                TableName = viewName;
                Columns = new List<SqlColumn>();
            }
            foreach (DataRow row in dt.Rows)
            {
                SqlColumn c = new SqlColumn()
                {
                    ColumnName = row["ColumnName"].ToString(),
                    OrdinalPosition = (int)row["ColumnOrdinal"],
                    Datatype = row["DataTypeName"].ToString(),
                    MaxLength = (int)row["ColumnSize"]
                };
                Columns.Add(c);
            }
        }

        internal bool HasIntegerPrimaryKey()
        {
            bool b = false;
            List<SqlColumn> found = (Columns.Where(x => x.IsPrimaryKey)).ToList();
            if (found != null && found.Count > 0)
            {
                b = !found[0].Datatype.ToString().Equals("uniqueidentifier", StringComparison.OrdinalIgnoreCase);
            }
            return b;
        }

        public IEnumerable<SqlColumn> GetNonPrimaryKeyFields()
        {
            return Columns.Where(x => !x.IsPrimaryKey).ToList();
        }

        public IEnumerable<SqlColumn> GetNonPrimaryKeyFields(params string[] excludedFields)
        {
            string[] exclusions = (from field in excludedFields
                                   select field.ToString().ToLower()).ToArray();

            return GetNonPrimaryKeyFields().Where(x => x.NameNotIn(exclusions)).ToList();
        }

        public string GetPrimaryKeyParam()
        {
            string s = String.Empty;
            List<SqlColumn> found = (Columns.Where(x => x.IsPrimaryKey)).ToList();
            if (found != null && found.Count > 0)
            {
                s = String.Format("{0}", found[0].ToSqlParam());
            }
            return s;
        }
        public string GetPrimaryKeyField()
        {
            string s = String.Empty;
            List<SqlColumn> found = (Columns.Where(x => x.IsPrimaryKey)).ToList();
            if (found != null && found.Count > 0)
            {
                s = found[0].ColumnName;
            }
            return s;
        }


        public string GetPrimaryKeyName()
        {
            List<SqlColumn> found = Columns.Where(x => x.IsPrimaryKey == true).ToList();
            return (found.Count > 0) ? found[0].ColumnName : String.Empty;
        }

        public string CreateTextCommand()
        {
            StringBuilder sb = new StringBuilder();
            if (Columns.Count > 1)
            {
                sb.AppendLine(String.Format("select\t{0}", Columns[0].ColumnName));
                for (int i = 1; i < Columns.Count; i++)
                {
                    sb.AppendLine(String.Format("\t,{0}", Columns[i].ColumnName));
                }
                sb.AppendLine(String.Format("from\t{0}.{1}", TableSchema, TableName));
            }
            else
            {
                sb.AppendLine(String.Format("select * from {0}.{1}", TableSchema, TableName));
            }


            return sb.ToString();
        }


    }
}
