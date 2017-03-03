// <copyright file="SolutionApp.cs" company="eXtensoft, LLC">
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

    public class SolutionAppMDG : SqlServerModelDataGateway<SolutionApp>
    {

        #region local fields

        private const string SolutionAppIdParamName = "@solutionappid";
        private const string SolutionIdParamName = "@solutionid";
        private const string AppIdParamName = "@appid";
        private const string SortParamName = "@sort";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, SolutionApp model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [dbo].[SolutionApp] ( [SolutionId],[AppId],[Sort] ) values (" + SolutionIdParamName + "," + AppIdParamName + "," + SortParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionIdParamName, model.SolutionId);
            cmd.Parameters.AddWithValue(AppIdParamName, model.AppId);
            cmd.Parameters.AddWithValue(SortParamName, model.Sort);

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, SolutionApp model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [dbo].[SolutionApp] set [SolutionId] = " + SolutionIdParamName + " , [AppId] = " + AppIdParamName + " , [Sort] = " + SortParamName + " where [SolutionAppId] = " + SolutionAppIdParamName;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionIdParamName, model.SolutionId);
            cmd.Parameters.AddWithValue(AppIdParamName, model.AppId);
            cmd.Parameters.AddWithValue(SortParamName, model.Sort);

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [dbo].[SolutionApp] where [SolutionAppId] = " + SolutionAppIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionAppIdParamName, criterion.GetValue<int>("SolutionAppId"));

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [SolutionAppId], [SolutionId], [AppId], [Sort] from [dbo].[SolutionApp] where [SolutionAppId] = " + SolutionAppIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionAppIdParamName, criterion.GetValue<int>("SolutionAppId"));

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [SolutionAppId], [SolutionId], [AppId], [Sort] from [dbo].[SolutionApp] where [SolutionId] = " + SolutionIdParamName;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue(SolutionIdParamName, criterion.GetValue<int>("SolutionId"));
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

        public override void BorrowReader(SqlDataReader reader, List<SolutionApp> list)
        {
            while (reader.Read())
            {
                var model = new SolutionApp();
                model.SolutionAppId = reader.GetInt32(reader.GetOrdinal("SolutionAppId"));
                model.SolutionId = reader.GetInt32(reader.GetOrdinal("SolutionId"));
                model.AppId = reader.GetInt32(reader.GetOrdinal("AppId"));
                model.Sort = reader.GetInt32(reader.GetOrdinal("Sort"));
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}

