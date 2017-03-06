// <copyright file="ServerApp.cs" company="eXtensoft, LLC">
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

    public class ServerAppMDG : SqlServerModelDataGateway<ServerApp>
    {

        #region local fields

        private const string ServerAppIdParamName = "@serverappid";
        private const string ServerIdParamName = "@serverid";
        private const string AppIdParamName = "@appid";
        private const string ZoneIdParamName = "@zoneid";
        private const string ScopeIdParamName = "@scopeid";
        private const string FolderpathParamName = "@folderpath";
        private const string BackupFolderpathParamName = "@backup";

        private const string DomainIdParamName = "@domainid";
        private const string UrlIdParamName = "@urlid";
        private const string SolutionIdParamName = "@solutionid";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, ServerApp model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [arc].[ServerApp] ([ServerId],[AppId],[ZoneId],[ScopeId],[DomainId],[Folderpath],[BackupFolderpath]) values (" + ServerIdParamName + 
                "," + AppIdParamName + "," + ZoneIdParamName + "," + ScopeIdParamName + "," + DomainIdParamName + "," + FolderpathParamName + "," + BackupFolderpathParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(ServerIdParamName, model.ServerId);
            cmd.Parameters.AddWithValue(AppIdParamName, model.AppId);
            cmd.Parameters.AddWithValue(ZoneIdParamName, model.ZoneId);
            cmd.Parameters.AddWithValue(ScopeIdParamName, model.ScopeId);
            if (model.DomainId > 0)
            {
                cmd.Parameters.AddWithValue(DomainIdParamName, model.DomainId);
            }
            else
            {
                cmd.Parameters.AddWithValue(DomainIdParamName, DBNull.Value);
            }
            cmd.Parameters.AddWithValue(FolderpathParamName,!String.IsNullOrWhiteSpace(model.Folderpath) ?(object)model.Folderpath : DBNull.Value);
            cmd.Parameters.AddWithValue(BackupFolderpathParamName,!String.IsNullOrWhiteSpace(model.BackupFolderpath) ?(object)model.BackupFolderpath : DBNull.Value);

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, ServerApp model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [arc].[ServerApp] set [ZoneId] = " + ZoneIdParamName + ",[ScopeId] = " + ScopeIdParamName + ",[DomainId] = " + DomainIdParamName +
                ",[Folderpath] = " + FolderpathParamName + ",[BackupFolderpath] = " + BackupFolderpathParamName + " where [ServerAppId] = " + ServerAppIdParamName;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(ZoneIdParamName, model.ZoneId);
            cmd.Parameters.AddWithValue(ScopeIdParamName, model.ScopeId);
            if (model.DomainId > 0)
            {
                cmd.Parameters.AddWithValue(DomainIdParamName, model.DomainId);
            }
            else
            {
                cmd.Parameters.AddWithValue(DomainIdParamName, DBNull.Value);
            }
            cmd.Parameters.AddWithValue(FolderpathParamName,!String.IsNullOrWhiteSpace(model.Folderpath) ?(object)model.Folderpath : DBNull.Value);
            cmd.Parameters.AddWithValue(BackupFolderpathParamName,!String.IsNullOrWhiteSpace(model.BackupFolderpath) ?(object)model.BackupFolderpath : DBNull.Value);
            cmd.Parameters.AddWithValue(ServerAppIdParamName,model.ServerAppId);

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [arc].[ServerApp] where [ServerId] = " + ServerIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ServerIdParamName, criterion.GetValue<int>("ServerId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "SELECT sa.ServerAppId, sa.ServerId, sa.AppId, sa.Folderpath, sa.BackupFolderpath, sa.ZoneId, sa.ScopeId, sa.DomainId, s.SecurityId, s.ExternalIP, s.InternalIP FROM [arc].ServerApp AS sa INNER JOIN [arc].Server AS s ON sa.ServerId = s.ServerId where sa.[ServerAppId] = " + ServerAppIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ServerAppIdParamName, criterion.GetValue<int>("ServerAppId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "SELECT sa.ServerAppId, sa.ServerId, sa.AppId, sa.Folderpath, sa.BackupFolderpath, sa.ZoneId, sa.ScopeId, sa.DomainId, s.SecurityId, s.ExternalIP, s.InternalIP FROM [arc].ServerApp AS sa INNER JOIN [arc].Server AS s ON sa.ServerId = s.ServerId ";
            if (criterion != null)
            {
                if (criterion.ContainsStrategy())
                {
                    string key = criterion.GetStrategyKey();
                    if (key.Equals("zone.region", StringComparison.OrdinalIgnoreCase))
                    {
                        bool b = false;
                        StringBuilder sb = new StringBuilder();
                        if (criterion.Contains("ZoneId"))
                        {
                            sb.Append(" sa.[ZoneId] = " + ZoneIdParamName);
                            cmd.Parameters.AddWithValue(ZoneIdParamName, criterion.GetValue<int>("ZoneId"));

                            b = true;
                        }
                        if (criterion.Contains("ScopeId"))
                        {
                            if (b)
                            {
                                sb.Append(" and");
                            }
                            sb.Append(" sa.[ScopeId] = " + ScopeIdParamName);
                            cmd.Parameters.AddWithValue(ScopeIdParamName, criterion.GetValue<int>("ScopeId"));
                            b = true;
                        }
                        if (b)
                        {
                            sql += " where" + sb.ToString();
                        }
                    }
                    else if (key.Equals("solution", StringComparison.OrdinalIgnoreCase))
                    {
                        sql += " where ( sa.[AppId] in (select AppId from SolutionApp where SolutionId = " + SolutionIdParamName + ")" +
                            " AND sa.[ZoneId] in (select ZoneId from SolutionZone where SolutionId = " + SolutionIdParamName + ")" +
                            " AND [ScopeId] in (select ScopeId from Solution where SolutionId = " + SolutionIdParamName + "))";

                        cmd.Parameters.AddWithValue(SolutionIdParamName, criterion.GetValue<int>("SolutionId"));
                    }
                }
                else if (criterion.Contains("ServerId"))
                {
                    sql += "where sa.[ServerId] = " + ServerIdParamName;
                    cmd.Parameters.AddWithValue(ServerIdParamName, criterion.GetValue<int>("ServerId"));
                }

            }
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

        public override void BorrowReader(SqlDataReader reader, List<ServerApp> list)
        {
            while (reader.Read())
            {
                var model = new ServerApp();
                model.ServerAppId = reader.GetInt32(reader.GetOrdinal("ServerAppId"));
                model.ServerId = reader.GetInt32(reader.GetOrdinal("ServerId"));
                model.AppId = reader.GetInt32(reader.GetOrdinal("AppId"));
                model.ZoneId = reader.GetInt32(reader.GetOrdinal("ZoneId"));
                model.ScopeId = reader.GetInt32(reader.GetOrdinal("ScopeId"));
                model.SecurityId = reader.GetInt32(reader.GetOrdinal("SecurityId"));
                if (!reader.IsDBNull(reader.GetOrdinal("DomainId")))
                {
                    model.DomainId = reader.GetInt32(reader.GetOrdinal("DomainId"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("Folderpath")))
                {
                    model.Folderpath = reader.GetString(reader.GetOrdinal("Folderpath"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("BackupFolderpath")))
                {
                    model.BackupFolderpath = reader.GetString(reader.GetOrdinal("BackupFolderpath"));
                }
                model.InternalIP = reader.GetString(reader.GetOrdinal("InternalIP"));
                model.ExternalIP = reader.GetString(reader.GetOrdinal("ExternalIP"));


                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
