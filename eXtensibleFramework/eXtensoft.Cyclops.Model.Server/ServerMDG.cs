// <copyright file="Server.cs" company="eXtensoft, LLC">
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

    public class ServerMDG : SqlServerModelDataGateway<Server>
    {

        #region local fields

        private const string ServerIdParamName = "@serverid";
        private const string OperatingSystemIdParamName = "@operatingsystemid";
        private const string HostPlatformIdParamName = "@hostplatformid";
        private const string SecurityIdParamName = "@securityid";
        private const string NameParamName = "@name";
        private const string AliasParamName = "@alias";
        private const string DescriptionParamName = "@description";
        private const string ExternalIPParamName = "@externalip";
        private const string InternalIPParamName = "@internalip";
        private const string TagsParamName = "@tags";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, Server model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [arc].[Server] ( [OperatingSystemId],[HostPlatformId],[SecurityId],[Name],[Alias],[Description],[ExternalIP],[InternalIP],[Tags] ) values (" + OperatingSystemIdParamName + "," + HostPlatformIdParamName + "," + SecurityIdParamName + "," + NameParamName + "," + AliasParamName + "," + DescriptionParamName + "," + ExternalIPParamName + "," + InternalIPParamName + "," + TagsParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( OperatingSystemIdParamName, model.OperatingSystemId );
            cmd.Parameters.AddWithValue( HostPlatformIdParamName, model.HostPlatformId );
            cmd.Parameters.AddWithValue( SecurityIdParamName, model.SecurityId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );
            cmd.Parameters.AddWithValue( DescriptionParamName, model.Description );
            cmd.Parameters.AddWithValue( ExternalIPParamName, model.ExternalIP );
            cmd.Parameters.AddWithValue( InternalIPParamName, model.InternalIP );
            cmd.Parameters.AddWithValue( TagsParamName, String.IsNullOrWhiteSpace(model.Tags) ? DBNull.Value : (object)model.Tags );

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, Server model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [arc].[Server] set [OperatingSystemId] = " + OperatingSystemIdParamName + " , [HostPlatformId] = " + HostPlatformIdParamName + " , [SecurityId] = " + SecurityIdParamName + " , [Name] = " + NameParamName + " , [Alias] = " + AliasParamName + " , [Description] = " + DescriptionParamName + " , [ExternalIP] = " + ExternalIPParamName + " , [InternalIP] = " + InternalIPParamName + " , [Tags] = " + TagsParamName  + " where [ServerId] = " + ServerIdParamName ;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( OperatingSystemIdParamName, model.OperatingSystemId );
            cmd.Parameters.AddWithValue( HostPlatformIdParamName, model.HostPlatformId );
            cmd.Parameters.AddWithValue( SecurityIdParamName, model.SecurityId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );
            cmd.Parameters.AddWithValue( DescriptionParamName, model.Description );
            cmd.Parameters.AddWithValue( ExternalIPParamName, model.ExternalIP );
            cmd.Parameters.AddWithValue( InternalIPParamName, model.InternalIP );
            cmd.Parameters.AddWithValue( TagsParamName, String.IsNullOrWhiteSpace(model.Tags) ? DBNull.Value : (object)model.Tags);
            cmd.Parameters.AddWithValue(ServerIdParamName, model.ServerId);

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [arc].[Server] where [ServerId] = " + ServerIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ServerIdParamName, criterion.GetValue<int>("ServerId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags] from [arc].[Server] where [ServerId] = " + ServerIdParamName ;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ServerIdParamName, criterion.GetValue<int>("ServerId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags] from [arc].[Server] ";
            StringBuilder sb = new StringBuilder();
            sb.Append(sql);
            int i = 0;
            if (criterion != null)
            {
                if (!criterion.ContainsStrategy())
                {

                    foreach (TypedItem item in criterion.Items)
                    {
                        if (maps.ContainsKey(item.Key))
                        {
                            if (i == 0)
                            {
                                sb.Append(" where ");
                            }
                            else
                            {
                                sb.Append(" and ");
                            }
                            sb.Append(maps[item.Key]);
                            string paramName = String.Format("@{0}", item.Key.ToLower());
                            cmd.Parameters.AddWithValue(paramName, item.Value);
                        }
                    }
                }
                else if(criterion.Contains("key"))
                {
                    string key = criterion.GetValue<string>("key");
                    sb.Append(" where ");
                    sb.Append(maps[key]);
                    cmd.Parameters.AddWithValue("@q", criterion.GetValue<string>("q"));
                }
            }

            cmd.CommandText = sb.ToString();

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

        public override void BorrowReader(SqlDataReader reader, List<Server> list)
        {
            while (reader.Read())
            {
                var model = new Server();
                model.ServerId = reader.GetInt32(reader.GetOrdinal("ServerId"));
                model.OperatingSystemId = reader.GetInt32(reader.GetOrdinal("OperatingSystemId"));
                model.HostPlatformId = reader.GetInt32(reader.GetOrdinal("HostPlatformId"));
                model.SecurityId = reader.GetInt32(reader.GetOrdinal("SecurityId"));
                model.Name = reader.GetString(reader.GetOrdinal("Name"));
                if (!reader.IsDBNull(reader.GetOrdinal("Alias")))
                {
                    model.Alias = reader.GetString(reader.GetOrdinal("Alias"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("Description")))
                {
                    model.Description = reader.GetString(reader.GetOrdinal("Description"));
                }


                if (!reader.IsDBNull(reader.GetOrdinal("ExternalIP")))
                {
                    model.ExternalIP = reader.GetString(reader.GetOrdinal("ExternalIP"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("InternalIP")))
                {
                    model.InternalIP = reader.GetString(reader.GetOrdinal("InternalIP"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("Tags")))
                {
                    model.Tags = reader.GetString(reader.GetOrdinal("Tags"));
                }
                list.Add(model);

            }
        }

        #endregion Borrower overrides

        private static IDictionary<string, string> maps = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"HostPlatformId","[HostPlatformId] = @hostplatformid"},
            {"OperatingSystemId","[OperatingSystemId] = @operatingsystemid"},
            {"Tags","[Tags] like '%'+ @tags + '%'"},
            {"Name","[Name] like '%' + @name + '%'"},
            {"ServerSecurityId", "[ServerSecurityId] = @serversecurityid"},
            {"IP","[ExternalIP] like '%' +  @q + '%' OR [InternalIP] like '%' + @q + '%' " },
            {"Alpha" ,"[Name] like '%' + @q + '%' OR [Tags] like '%' + @q + '%' "} ,
        };

    }
}
