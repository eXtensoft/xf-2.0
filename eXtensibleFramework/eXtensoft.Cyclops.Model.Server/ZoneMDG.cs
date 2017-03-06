// <copyright file="Zone.cs" company="eXtensoft, LLC">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Data;
    using System.Data.SqlClient;
    using XF.Common;
    using XF.DataServices;

    public class ZoneMDG : SqlServerModelDataGateway<Zone>
    {

        #region local fields

        private const string ZoneIdParamName = "@zoneid";
        private const string NameParamName = "@name";
        private const string AliasParamName = "@alias";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, Zone model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [arc].[Zone] ( [Name],[Alias] ) values (" + NameParamName + "," + AliasParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, Zone model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [arc].[Zone] set [Name] = " + NameParamName + " , [Alias] = " + AliasParamName  + " where [ZoneId] = " + ZoneIdParamName ;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [arc].[Zone] where [ZoneId] = " + ZoneIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ZoneIdParamName, criterion.GetValue<int>("ZoneId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ZoneId], [Name], [Alias] from [arc].[Zone] where [ZoneId] = " + ZoneIdParamName ;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ZoneIdParamName, criterion.GetValue<int>("ZoneId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ZoneId], [Name], [Alias] from [arc].[Zone] ";
            cmd.CommandText = sql;

            return cmd;
        }
        public override SqlCommand GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "";

            cmd.CommandText = sql;

            return cmd;
        }

        #endregion SqlCommand overrides

        #region Borrower overrides

        public override void BorrowReader(SqlDataReader reader, List<Zone> list)
        {
            while (reader.Read())
            {
                var model = new Zone();
                model.ZoneId = reader.GetInt32(reader.GetOrdinal("ZoneId"));
                model.Name = reader.GetString(reader.GetOrdinal("Name"));
                model.Alias = reader.GetString(reader.GetOrdinal("Alias"));
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
