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

    public static class InlineSqlCommandGenerator
    {
        private static IDictionary<ModelActionOption, Func<SqlConnection,SqlTable, ICriterion, SqlCommand>> maps = new Dictionary<ModelActionOption, Func<SqlConnection,SqlTable, ICriterion, SqlCommand>>()
        {
            {ModelActionOption.Get,Get},
            {ModelActionOption.GetAll,GetAll},
            {ModelActionOption.GetAllProjections,GetAllProjections},
            {ModelActionOption.Delete,Delete},
            {ModelActionOption.Put,Patch},            
        };

        public static SqlCommand Generate(SqlConnection cn, SqlTable table, ModelActionOption option, ICriterion criterion)
        {
            return maps[option](cn, table, criterion);
        }

        public static SqlCommand Generate<T>(SqlConnection cn, SqlTable table, ModelActionOption option, T t)
        {
            SqlCommand cmd = null;
            if (option.Equals(ModelActionOption.Post))
            {
                cmd = Post<T>(cn, table, t);
            }
            else if (option.Equals(ModelActionOption.Put))
            {
                cmd = Put<T>(cn, table, t);
            }
            return cmd;
        }

        private static SqlCommand Get(SqlConnection cn, SqlTable table, ICriterion criterion)
        {
            int i = 0;
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            foreach (var item in table.Columns)
            {
                if (i++ > 0)
                {
                    sb.Append(",");
                }
                sb.AppendFormat("[{0}]", item.ColumnName);
            }
            sb.AppendFormat(" from [{0}].[{1}]", table.TableSchema, table.TableName);
            if (criterion != null && criterion.Items != null && criterion.Items.Count() > 0)
            {
                sb.Append(" where ");
                i = 0;
                foreach (var item in criterion.Items)
                {
                    var found = table.Columns.Find(x => x.ColumnName.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                    if (found != null)
                    {
                        string paramName = String.Format("@{0}", item.Key.ToLower());
                        if (i++ > 0)
                        {
                            sb.Append(" and ");
                        }
                        sb.AppendFormat("[{0}] = {1}", found.ColumnName, paramName);
                        cmd.Parameters.AddWithValue(paramName, item.Value);
                    }


                }
            }
            cmd.CommandText = sb.ToString();
            return cmd;
        }
        
        private static SqlCommand GetAll(SqlConnection cn, SqlTable table, ICriterion criterion)
        {
            int i = 0;
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            foreach (var item in table.Columns)
            {
                if (i++ > 0)
                {
                    sb.Append(",");
                }
                sb.AppendFormat("[{0}]", item.ColumnName);
            }
            sb.AppendFormat(" from [{0}].[{1}]", table.TableSchema, table.TableName);
            if (criterion != null && criterion.Items != null && criterion.Items.Count() > 0)
            {
                sb.Append(" where ");
                i = 0;
                foreach (var item in criterion.Items)
                {
                    var found = table.Columns.Find(x => x.ColumnName.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                    if (found != null)
                    {
                        string paramName = String.Format("@{0}", item.Key.ToLower());
                        if (i++ > 0)
                        {
                            sb.Append(" and ");
                        }
                        sb.AppendFormat("[{0}] = {1}", found.ColumnName, paramName);
                        cmd.Parameters.AddWithValue(paramName, item.Value);
                    }
                    

                }
            }
            cmd.CommandText = sb.ToString();
            return cmd;
        }

        private static SqlCommand GetAllProjections(SqlConnection cn, SqlTable table, ICriterion criterion)
        {
            return null;
        }
        
        private static SqlCommand Delete(SqlConnection cn, SqlTable table, ICriterion criterion)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from [{0}].[{1}]", table.TableSchema, table.TableName);
            if (criterion != null && criterion.Items != null && criterion.Items.Count() > 0)
            {
                sb.Append(" where ");
                int i = 0;
                foreach (var item in criterion.Items)
                {
                    var found = table.Columns.Find(x => x.ColumnName.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                    if (found != null)
                    {
                        string paramName = String.Format("@{0}", item.Key.ToLower());
                        if (i++ > 0)
                        {
                            sb.Append(" and ");
                        }
                        sb.AppendFormat("[{0}] = {1}", found.ColumnName, paramName);
                        cmd.Parameters.AddWithValue(paramName, item.Value);
                    }
                }
            }
            cmd.CommandText = sb.ToString();
            return cmd;
        }
       
        private static SqlCommand Patch(SqlConnection cn, SqlTable table, ICriterion criterion)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sb = new StringBuilder();
            string where = String.Empty;
            sb.AppendFormat("update [{0}].[{1}] set ", table.TableSchema, table.TableName);
            if (criterion != null && criterion.Items != null && criterion.Items.Count() > 0)
            {
                int i = 0;
                foreach (var item in criterion.Items)
                {
                    var found = table.Columns.Find(x => x.ColumnName.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                    if(found != null)
                    {
                        string paramName = String.Format("@{0}", item.Key.ToLower());

                        if (found.IsPrimaryKey)
                        {
                            where = String.Format(" where {0} = {1}", found.ColumnName, paramName);
                        }
                        else
                        {                            
                            if (i++ > 0)
                            {
                                sb.Append(",");
                            }
                            sb.AppendFormat("[{0}] = {1}", found.ColumnName, paramName);                            
                        }
                        cmd.Parameters.AddWithValue(paramName, item.Value);


                    }
                }
            }
            sb.Append(where);
            cmd.CommandText = sb.ToString();
            return cmd;
        }

        private static SqlCommand Post<T>(SqlConnection cn, SqlTable table, T t)
        {
            var infos = t.GetType().GetProperties();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into [{0}].[{1}] (", table.TableSchema, table.TableName);
            StringBuilder sbValues = new StringBuilder();
            sbValues.Append(") values (");
            int i = 0;
            foreach (var item in table.GetNonPrimaryKeyFields())
            {
                var info = infos.FirstOrDefault(x => x.Name.Equals(item.ColumnName, StringComparison.OrdinalIgnoreCase));
                if (info != null)
                {
                    if (info.PropertyType.Equals(typeof(DateTime)) && !String.IsNullOrWhiteSpace(item.DefaultValue))
                    {
                        
                    }
                    else
                    {
                        string paramName = String.Format("@{0}", item.ColumnName.ToLower());
                        if (i++ > 0)
                        {
                            sb.Append(",");
                            sbValues.Append(",");
                        }
                        sb.AppendFormat("[{0}]", item.ColumnName);
                        sbValues.Append(paramName);
                        cmd.Parameters.AddWithValue(paramName, info.GetValue(t, null));
                    }

                }
            }
            sbValues.Append(")");
            sb.Append(sbValues.ToString());
            cmd.CommandText = sb.ToString();
            return cmd;
        }

        private static SqlCommand Put<T>(SqlConnection cn, SqlTable table, T t)
        {
            var infos = t.GetType().GetProperties();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update [{0}].[{1}] set ");
            int i = 0;
            foreach (var item in table.GetNonPrimaryKeyFields())
            {
                var info = infos.FirstOrDefault(x => x.Name.Equals(item.ColumnName, StringComparison.OrdinalIgnoreCase));
                if (info != null && !item.IsPrimaryKey )
                {
                    string paramName = String.Format("@{0}", item.ColumnName.ToLower());
                    if (i++ > 0)
                    {
                        sb.Append(",");
                    }
                    sb.AppendFormat("[{0}] = {1}", item.ColumnName, paramName);
                    cmd.Parameters.AddWithValue(paramName, info.GetValue(t, null));
                }
            }
            cmd.CommandText = sb.ToString();
            return cmd;
        }

    }
}
