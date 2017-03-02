// <copyright company="eXtensoft, LLC" file="SqlServerApiRequestProvider.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Data;

    public sealed class SqlServerApiRequestProvider : ApiRequestProvider
    {

        #region local fields

        private const string ApiRequestIdParamName = "@apirequestid";
        private const string AppKeyParamName = "@appkey";
        private const string AppZoneParamName = "@appzone";
        private const string AppInstanceParamName = "@appinstance";
        private const string ElapsedParamName = "@elapsed";
        private const string StartParamName = "@start";
        private const string ProtocolParamName = "@protocol";
        private const string HostParamName = "@host";
        private const string PathParamName = "@path";
        private const string ClientIPParamName = "@clientip";
        private const string UserAgentParamName = "@useragent";
        private const string HttpMethodParamName = "@httpmethod";
        private const string ControllerNameParamName = "@controllername";
        private const string ControllerMethodParamName = "@controllermethod";
        private const string MethodReturnTypeParamName = "@methodreturntype";
        private const string ResponseCodeParamName = "@responsecode";
        private const string ResponseTextParamName = "@responsetext";
        private const string XmlDataParamName = "@xmldata";
        private const string MessageIdParamName = "@messageid";
        private const string BasicTokenParamName = "@basictoken";
        private const string BearerTokenParamName = "@bearertoken";
        private const string AuthSchemaParamName = "@authschema";
        private const string AuthValueParamName = "@authvalue";
        private const string MessageBodyParamName = "@messagebody";
        private const string ErrorLogParamName = "@errolog";
        private const string DbSchema = "log";
        #endregion local fields


        protected override string ProviderKey
        {
            get
            {
                return "xf.webapi.sqlserver";
            }
        }

        protected override ApiRequest Get(Guid id)
        {
            return LocalGet(id);
        }
        protected override ApiRequest Get(string id)
        {
            int j;
            Guid g;
            if (Int32.TryParse(id, out j))
            {
                return LocalGetOne(j);

            }
            else if(Guid.TryParse(id, out g))
            {
                return LocalGet(g);
            }
            else
            {
                return null;
            }
        }
        protected override Page<ApiRequest> Get(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<ApiRequest> Get(int id)
        {
            return LocalGet(id);
        }

        protected override void Post(ApiRequest model)
        {
            LocalPost(model);
        }

        protected override void Post(IEnumerable<ApiRequest> models)
        {
            throw new NotImplementedException();
        }

        private static void LocalPost(IEnumerable<ApiRequest> models)
        {

        }

        private ApiRequest LocalGetOne(int id)
        {
            var items = LocalGet(id);
            return (items != null && items.Count() > 0) ? items.ToList()[0] : null;
        }
        private static IEnumerable<ApiRequest> LocalGet(int id)
        {
            List<ApiRequest> list = new List<ApiRequest>();
            string schema = eXtensibleConfig.Zone.Equals("production", StringComparison.OrdinalIgnoreCase) ? DateTime.Today.ToString("MMM").ToLower() : "log";
            var settings = ConfigurationManager.ConnectionStrings[eXtensibleWebApiConfig.SqlConnectionKey];
            if (settings != null && !String.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(settings.ConnectionString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {

                            string sql = " [ApiRequestId],[XmlData] from [" + schema + "].[ApiRequest] ";
                            StringBuilder sb = new StringBuilder();
                            sb.Append("select ");
                            if (id > 999)
                            {
                                sb.Append(sql);
                                sb.Append(" where [ApiRequestId] = " + ApiRequestIdParamName);
                                cmd.Parameters.AddWithValue(ApiRequestIdParamName, id);
                            }
                            else
                            {
                                sb.Append("top " + id.ToString() + " ");
                                sb.Append(sql);
                                sb.Append(" order by [ApiRequestId] desc");
                            }



                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sb.ToString();
                            cmd.CommandTimeout = 0;




                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                    string xml = reader.GetString(reader.GetOrdinal("XmlData"));
                                    var apiRequest = StringToRequest(xml);
                                    apiRequest.ApiRequestId = reader.GetInt64(reader.GetOrdinal("ApiRequestId"));
                                    list.Add(apiRequest);

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                }
            }
            return list;
        }

        private static ApiRequest LocalGet(Guid id)
        {
            ApiRequest item = null;

            string sql = "";
            var settings = ConfigurationManager.ConnectionStrings[eXtensibleWebApiConfig.SqlConnectionKey];
            if (settings != null && !String.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(settings.ConnectionString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;
                            cmd.CommandTimeout = 0;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                }
            }

            return item;
        }

        private static void LocalPost(ApiRequest model)
        {
            string schema = eXtensibleConfig.Zone.Equals("production", StringComparison.OrdinalIgnoreCase) ? DateTime.Today.ToString("MMM").ToLower() : "log";
            string sql = "insert into [" + schema + "].[ApiRequest] ( [AppKey],[AppZone],[AppInstance],[Elapsed],[Start],[Protocol],[Host],[Path]" +
                ",[ClientIP],[UserAgent],[HttpMethod],[ControllerName],[ControllerMethod],[MethodReturnType],[ResponseCode],[ResponseText]" +
                ",[XmlData],[MessageId],[BasicToken],[BearerToken],[AuthSchema],[AuthValue],[MessageBody],[HasLog] ) values (" + AppKeyParamName + "," + AppZoneParamName + "," +
                AppInstanceParamName + "," + ElapsedParamName + "," + StartParamName + "," + ProtocolParamName + "," + HostParamName + "," +
                PathParamName + "," + ClientIPParamName + "," + UserAgentParamName + "," + HttpMethodParamName + "," + ControllerNameParamName + "," +
                ControllerMethodParamName + "," + MethodReturnTypeParamName + "," + ResponseCodeParamName + "," + ResponseTextParamName + "," +
                XmlDataParamName + "," + MessageIdParamName + "," + BasicTokenParamName + "," + BearerTokenParamName + "," + AuthSchemaParamName + 
                "," + AuthValueParamName + "," + MessageBodyParamName + "," + ErrorLogParamName + ")";


            var settings = ConfigurationManager.ConnectionStrings[eXtensibleWebApiConfig.SqlConnectionKey];
            if (settings != null && !String.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(settings.ConnectionString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;
                            cmd.CommandTimeout = 0;

                            cmd.Parameters.AddWithValue(AppKeyParamName, model.AppKey.Truncate(25));
                            cmd.Parameters.AddWithValue(AppZoneParamName, model.AppZone.Truncate(15));
                            cmd.Parameters.AddWithValue(AppInstanceParamName, model.AppInstance.Truncate(25));
                            cmd.Parameters.AddWithValue(ElapsedParamName, model.Elapsed);
                            cmd.Parameters.AddWithValue(StartParamName, model.Start);
                            cmd.Parameters.AddWithValue(ProtocolParamName, model.Protocol.Truncate(5));
                            cmd.Parameters.AddWithValue(HostParamName, model.Host.Truncate(50));
                            cmd.Parameters.AddWithValue(PathParamName, model.Path.Truncate(150));
                            cmd.Parameters.AddWithValue(ClientIPParamName, model.ClientIP);
                            cmd.Parameters.AddWithValue(UserAgentParamName, String.IsNullOrWhiteSpace(model.UserAgent) ? "none" : model.UserAgent.Truncate(200));
                            cmd.Parameters.AddWithValue(HttpMethodParamName, model.HttpMethod.Truncate(10));
                            cmd.Parameters.AddWithValue(ControllerNameParamName, String.IsNullOrWhiteSpace(model.ControllerName) ? "none" : model.ControllerName.Truncate(50));
                            cmd.Parameters.AddWithValue(ControllerMethodParamName, String.IsNullOrWhiteSpace(model.ControllerMethod) ? "none" : model.ControllerMethod.Truncate(50));
                            cmd.Parameters.AddWithValue(MethodReturnTypeParamName, String.IsNullOrWhiteSpace(model.MethodReturnType) ? "none" : model.MethodReturnType.Truncate(50));
                            cmd.Parameters.AddWithValue(ResponseCodeParamName, model.ResponseCode.Truncate(3));
                            cmd.Parameters.AddWithValue(ResponseTextParamName, model.ResponseText.Truncate(100));
                            cmd.Parameters.AddWithValue(XmlDataParamName, model.ToXmlString());
                            cmd.Parameters.AddWithValue(MessageIdParamName, model.MessageId);
                            cmd.Parameters.AddWithValue(BasicTokenParamName, String.IsNullOrWhiteSpace(model.BasicToken) ? "none" : model.BasicToken.Truncate(50));
                            cmd.Parameters.AddWithValue(BearerTokenParamName, String.IsNullOrWhiteSpace(model.BearerToken) ? "none" : model.BearerToken.Truncate(50));
                            cmd.Parameters.AddWithValue(AuthSchemaParamName, model.AuthSchema.Truncate(10));
                            cmd.Parameters.AddWithValue(AuthValueParamName, model.AuthValue.Truncate(50));
                            cmd.Parameters.AddWithValue(MessageBodyParamName, !String.IsNullOrWhiteSpace(model.MessageBody) ? (object)model.MessageBody : DBNull.Value);
                            cmd.Parameters.AddWithValue(ErrorLogParamName, model.HasErrorLog);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties();
                    IEventWriter writer = new EventLogWriter();
                    writer.WriteError(message, SeverityType.Critical, "ApiRequest", props);
                }
            }

        }


    }

}
