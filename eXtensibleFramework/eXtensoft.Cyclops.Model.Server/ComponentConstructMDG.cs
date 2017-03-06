// <copyright file="ComponentConstruct.cs" company="eXtensoft, LLC">
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

    public class ComponentConstructMDG : SqlServerModelDataGateway<ComponentConstruct>
    {

        #region local fields

        private const string ComponentConstructIdParamName = "@componentconstructid";
        private const string ComponentIdParamName = "@componentid";
        private const string ConstructIdParamName = "@constructid";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, ComponentConstruct model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [arc].[ComponentConstruct] (  ) values (" +  "foo" + ")";

            cmd.CommandText = sql;


            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, ComponentConstruct model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [arc].[ComponentConstruct]";


            cmd.CommandText = sql;


            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [arc].[ComponentConstruct] where [ComponentId] = " + ComponentIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ComponentIdParamName, criterion.GetValue<int>("ComponentId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ComponentConstructId], [ComponentId], [ConstructId] from [arc].[ComponentConstruct] where [ComponentId] = " + ComponentIdParamName ;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ComponentIdParamName, criterion.GetValue<int>("ComponentId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ComponentConstructId], [ComponentId], [ConstructId] from [arc].[ComponentConstruct] where [ComponentId] = " + ComponentIdParamName ;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ComponentIdParamName, criterion.GetValue<int>("ComponentId") );

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

        public override void BorrowReader(SqlDataReader reader, List<ComponentConstruct> list)
        {
            while (reader.Read())
            {
                var model = new ComponentConstruct();
                model.ComponentConstructId = reader.GetInt32(reader.GetOrdinal("ComponentConstructId"));
                model.ComponentId = reader.GetInt32(reader.GetOrdinal("ComponentId"));
                model.ConstructId = reader.GetInt32(reader.GetOrdinal("ConstructId"));
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
