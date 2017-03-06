// <copyright file="AppComponent.cs" company="eXtensoft, LLC">
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

    public class AppComponentMDG : SqlServerModelDataGateway<AppComponent>
    {

        #region local fields

        private const string AppComponentIdParamName = "@appcomponentid";
        private const string AppIdParamName = "@appid";
        private const string ComponentIdParamName = "@componentid";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, AppComponent model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [arc].[AppComponent] ( [AppId],[ComponentId] ) values (" + AppIdParamName + "," + ComponentIdParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppIdParamName, model.AppId );
            cmd.Parameters.AddWithValue( ComponentIdParamName, model.ComponentId );

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, AppComponent model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [arc].[AppComponent] set [AppId] = " + AppIdParamName + " , [ComponentId] = " + ComponentIdParamName  + " where [AppComponentId] = " + AppComponentIdParamName ;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppIdParamName, model.AppId );
            cmd.Parameters.AddWithValue( ComponentIdParamName, model.ComponentId );

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [arc].[AppComponent] where [AppComponentId] = " + AppComponentIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppComponentIdParamName, criterion.GetValue<int>("AppComponentId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [AppComponentId], [AppId], [ComponentId] from [arc].[AppComponent] where [AppComponentId] = " + AppComponentIdParamName ;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppComponentIdParamName, criterion.GetValue<int>("AppComponentId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [AppComponentId], [AppId], [ComponentId] from [arc].[AppComponent] where [AppId] = " + AppIdParamName ;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppIdParamName, criterion.GetValue<int>("AppId") );

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

        public override void BorrowReader(SqlDataReader reader, List<AppComponent> list)
        {
            while (reader.Read())
            {
                var model = new AppComponent();
                model.AppComponentId = reader.GetInt32(reader.GetOrdinal("AppComponentId"));
                model.AppId = reader.GetInt32(reader.GetOrdinal("AppId"));
                model.ComponentId = reader.GetInt32(reader.GetOrdinal("ComponentId"));
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
