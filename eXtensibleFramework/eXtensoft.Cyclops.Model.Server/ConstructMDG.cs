// <copyright file="Construct.cs" company="eXtensoft, LLC">
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

    public class ConstructMDG : SqlServerModelDataGateway<Construct>
    {

        #region local fields

        private const string ConstructIdParamName = "@constructid";
        private const string ConstructTypeIdParamName = "@constructtypeid";
        private const string ScopeIdParamName = "@scopeid";
        private const string NameParamName = "@name";
        private const string AliasParamName = "@alias";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, Construct model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [dbo].[Construct] ( [ConstructTypeId],[ScopeId],[Name],[Alias] ) values (" + ConstructTypeIdParamName + "," + ScopeIdParamName + "," + NameParamName + "," + AliasParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ConstructTypeIdParamName, model.ConstructTypeId );
            cmd.Parameters.AddWithValue( ScopeIdParamName, model.ScopeId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, Construct model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [dbo].[Construct] set [ConstructTypeId] = " + ConstructTypeIdParamName + " , [ScopeId] = " + ScopeIdParamName + " , [Name] = " + NameParamName + " , [Alias] = " + AliasParamName  + " where [ConstructId] = " + ConstructIdParamName ;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ConstructTypeIdParamName, model.ConstructTypeId );
            cmd.Parameters.AddWithValue( ScopeIdParamName, model.ScopeId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [dbo].[Construct] where [ConstructId] = " + ConstructIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ConstructIdParamName, criterion.GetValue<int>("ConstructId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ConstructId], [ConstructTypeId], [ScopeId], [Name], [Alias] from [dbo].[Construct] where [ConstructId] = " + ConstructIdParamName ;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ConstructIdParamName, criterion.GetValue<int>("ConstructId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ConstructId], [ConstructTypeId], [ScopeId], [Name], [Alias] from [dbo].[Construct] ";
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

        public override void BorrowReader(SqlDataReader reader, List<Construct> list)
        {
            while (reader.Read())
            {
                var model = new Construct();
                model.ConstructId = reader.GetInt32(reader.GetOrdinal("ConstructId"));
                model.ConstructTypeId = reader.GetInt32(reader.GetOrdinal("ConstructTypeId"));
                model.ScopeId = reader.GetInt32(reader.GetOrdinal("ScopeId"));
                model.Name = reader.GetString(reader.GetOrdinal("Name"));
                model.Alias = reader.GetString(reader.GetOrdinal("Alias"));
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
