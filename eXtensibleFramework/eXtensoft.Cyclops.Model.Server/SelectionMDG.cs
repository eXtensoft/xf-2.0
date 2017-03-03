// <copyright file="Selection.cs" company="eXtensoft, LLC">
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

    public class SelectionMDG : SqlServerModelDataGateway<Selection>
    {

        #region local fields

        private const string SelectionIdParamName = "@selectionid";
        private const string DisplayParamName = "@display";
        private const string TokenParamName = "@token";
        private const string SortParamName = "@sort";
        private const string GroupIdParamName = "@groupid";
        private const string MasterIdParamName = "@masterid";
        private const string IconParamName = "@icon";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, Selection model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [dbo].[Selection] ( [Display],[Token],[Sort],[GroupId],[MasterId],[Icon] ) values (" + DisplayParamName + 
                "," + TokenParamName + "," + SortParamName + "," + GroupIdParamName + "," + MasterIdParamName + "," + IconParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( DisplayParamName, model.Display );
            cmd.Parameters.AddWithValue( TokenParamName, model.Token );
            cmd.Parameters.AddWithValue( SortParamName, model.Sort );
            cmd.Parameters.AddWithValue( GroupIdParamName, model.GroupId );
            cmd.Parameters.AddWithValue( MasterIdParamName, model.MasterId );
            cmd.Parameters.AddWithValue(IconParamName, !String.IsNullOrEmpty(model.Icon)? (object)model.Icon : DBNull.Value);

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, Selection model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [dbo].[Selection] set [Display] = " + DisplayParamName + " , [Token] = " + TokenParamName + " , [Sort] = " + SortParamName + " , [GroupId] = " + GroupIdParamName + " , [MasterId] = " + MasterIdParamName  + ",[Icon] = " + IconParamName + " where [SelectionId] = " + SelectionIdParamName ;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SelectionIdParamName, model.SelectionId);
            cmd.Parameters.AddWithValue(DisplayParamName, model.Display);
            cmd.Parameters.AddWithValue(TokenParamName, model.Token);
            cmd.Parameters.AddWithValue(SortParamName, model.Sort);
            cmd.Parameters.AddWithValue(GroupIdParamName, model.GroupId);
            cmd.Parameters.AddWithValue(MasterIdParamName, model.MasterId);
            cmd.Parameters.AddWithValue(IconParamName, !String.IsNullOrEmpty(model.Icon) ? (object)model.Icon : DBNull.Value);
            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [dbo].[Selection] where [SelectionId] = " + SelectionIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( SelectionIdParamName, criterion.GetValue<int>("SelectionId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select s.[SelectionId], s.[Display], s.[Token], s.[Sort], s.[GroupId], s.[MasterId],s.[Icon],COALESCE(COALESCE(s.Icon, (select [t].Icon from [dbo].[Selection] as [t] where [t].[SelectionId] = [s].[MasterId])) ,'default.icon.png') as SecondaryIcon from [dbo].[Selection]  as [s] where [s].[SelectionId] = " + SelectionIdParamName ;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( SelectionIdParamName, criterion.GetValue<int>("SelectionId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select s.[SelectionId], s.[Display], s.[Token], s.[Sort], s.[GroupId], s.[MasterId],s.[Icon],COALESCE(COALESCE(s.Icon, (select [t].Icon from [dbo].[Selection] as [t] where [t].[SelectionId] = [s].[MasterId])) ,'default.icon.png') as SecondaryIcon from [dbo].[Selection]  as [s]";
            cmd.CommandText = sql;

            return cmd;
        }
        public override SqlCommand GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select s.SelectionId as Id,[Display],COALESCE(COALESCE(s.Icon, (select [t].Icon from [dbo].[Selection] as [t] where [t].[SelectionId] = [s].[MasterId])) ,'default.icon.png') as [DisplayAlt], [SelectionId] as IntVal from dbo.Selection as s ";

            cmd.CommandText = sql;

            return cmd;
        }

        #endregion SqlCommand overrides

        #region Borrower overrides

        public override void BorrowReader(SqlDataReader reader, List<Selection> list)
        {

            while (reader.Read())
            {
               bool hasIcon = reader.FieldExists("SecondaryIcon");
                var model = new Selection();
                model.SelectionId = reader.GetInt32(reader.GetOrdinal("SelectionId"));
                model.Display = reader.GetString(reader.GetOrdinal("Display"));
                model.Token = reader.GetString(reader.GetOrdinal("Token"));
                model.Sort = reader.GetInt32(reader.GetOrdinal("Sort"));
                if (!reader.IsDBNull(reader.GetOrdinal("GroupId")))
                {
                    model.GroupId = reader.GetInt32(reader.GetOrdinal("GroupId"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("MasterId")))
                {
                    model.MasterId = reader.GetInt32(reader.GetOrdinal("MasterId"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("Icon")))
                {
                    model.Icon = reader.GetString(reader.GetOrdinal("Icon"));
                }
                if (hasIcon && !reader.IsDBNull(reader.GetOrdinal("SecondaryIcon")))
                {
                    model.SecondaryIcon = reader.GetString(reader.GetOrdinal("SecondaryIcon"));
                }
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
