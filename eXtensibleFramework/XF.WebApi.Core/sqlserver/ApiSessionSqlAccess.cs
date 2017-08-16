// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using XF.Common;

    public static class ApiSessionSqlAccess
    {

        public static ApiSession Get(string bearerToken)
        {
            ApiSession model = new ApiSession();

            if (!String.IsNullOrWhiteSpace(bearerToken))
            {

                string schema = eXtensibleConfig.Zone.Equals("production", StringComparison.OrdinalIgnoreCase) ? DateTime.Today.ToString("MMM").ToLower() : "log";
                var settings = ConfigurationProvider.ConnectionStrings[eXtensibleWebApiConfig.SqlConnectionKey];
                if (settings != null && !String.IsNullOrWhiteSpace(settings.ConnectionString))
                {
                    List<ApiSession> list = new List<ApiSession>();
                    try
                    {
                        using (SqlConnection cn = new SqlConnection(settings.ConnectionString))
                        {
                            cn.Open();
                            using (SqlCommand cmd = cn.CreateCommand())
                            {
                                const string BearerTokenParamName = "@bearertoken";

                                string sql = "select [Id], [CreatedAt], [BasicToken], [BearerToken], [TenantId], [PatronId], [SsoPatronId]," +
                                    " [IPAddress], [UserAgent], [PassKey], [LinesOfBusiness] from [" + schema + "].[Session] " +
                                    " where [BearerToken] = " + BearerTokenParamName;

                                cmd.CommandText = sql;

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    BorrowReader(reader, list);
                                }

                                if (list.Count > 0)
                                {
                                    model = list[0];
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        var props = eXtensibleConfig.GetProperties();
                        EventWriter.WriteError(message, SeverityType.Error, "DataAccess", props);
                    }


                }
            }


            return model;
        }


        public static Page<ApiSession> Get(int pageSize = 20, int pageIndex = 0, int tenantId = 0, int patronId = 0, string ipAddress = "", string basicToken = "", string marker = "")
        {
            Page<ApiSession> page = new Page<ApiSession>();
            List<ApiSession> list = new List<ApiSession>();
            string schema = eXtensibleConfig.Zone.Equals("production", StringComparison.OrdinalIgnoreCase) ? DateTime.Today.ToString("MMM").ToLower() : "log";
            var settings = ConfigurationProvider.ConnectionStrings[eXtensibleWebApiConfig.SqlConnectionKey];
            if (settings != null && !String.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(settings.ConnectionString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            string sql = "select top " + pageSize + " [Id], [CreatedAt], [BasicToken], [BearerToken], [TenantId], [PatronId], [SsoPatronId]," +
                                " [IPAddress], [UserAgent], [PassKey], [LinesOfBusiness] from [" + schema + "].[Session] " +
                                " order by [Id] desc ";
                            cmd.CommandText = sql;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                BorrowReader(reader, list);
                                page.Items = list;

                                if (reader.NextResult())
                                {
                                    page.Total = reader.GetInt32(0);
                                }
                            }

                        }


                    }
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties();
                    EventWriter.WriteError(message, SeverityType.Error, "DataAccess", props);
                }
            }
       
            return page;
        }

        public static void BorrowReader(SqlDataReader reader, List<ApiSession> list)
        {
            while (reader.Read())
            {
                var model = new ApiSession();
                model.CreatedAt = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedAt"));
                model.BasicToken = reader.GetGuid(reader.GetOrdinal("BasicToken"));
                model.BearerToken = reader.GetString(reader.GetOrdinal("BearerToken"));
                model.TenantId = reader.GetInt32(reader.GetOrdinal("TenantId"));
                model.PatronId = reader.GetInt32(reader.GetOrdinal("PatronId"));
                model.SsoPatronId = reader.GetInt32(reader.GetOrdinal("SsoPatronId"));
                if (!reader.IsDBNull(reader.GetOrdinal("IPAddress")))
                {
                    model.IPAddress = reader.GetString(reader.GetOrdinal("IPAddress"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("UserAgent")))
                {
                    model.UserAgent = reader.GetString(reader.GetOrdinal("UserAgent"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("PassKey")))
                {
                    model.PassKey = reader.GetString(reader.GetOrdinal("PassKey"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LinesOfBusiness")))
                {
                    model.LinesOfBusiness = reader.GetString(reader.GetOrdinal("LinesOfBusiness"));
                }
                list.Add(model);

            }
        }


    }
}
