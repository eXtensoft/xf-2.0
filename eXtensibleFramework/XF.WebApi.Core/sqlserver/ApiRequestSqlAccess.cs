// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi.Core
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using XF.Common;
    using XF.Common.Special;

    public static class ApiRequestSqlAccess
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
        private const string PageSizeParamName = "@pagesize";
        private const string PageIndexParamName = "@pageindex";
        private const string DbSchema = "log";
        #endregion local fields

        private static string GetSchema()
        {
            DateTime now = DateTime.Now;
            return now.ToSchema(eXtensibleWebApiConfig.LoggingSchema, "log");
        }

        public static void Post(ApiRequest model)
        {
            string schema = GetSchema();
            string sql = "insert into [" + schema + "].[ApiRequest] ( [AppKey],[AppZone],[AppInstance],[Elapsed],[Start],[Protocol],[Host],[Path]" +
                ",[ClientIP],[UserAgent],[HttpMethod],[ControllerName],[ControllerMethod],[MethodReturnType],[ResponseCode],[ResponseText]" +
                ",[XmlData],[MessageId],[BasicToken],[BearerToken],[AuthSchema],[AuthValue],[MessageBody] ) values (" + AppKeyParamName + "," + AppZoneParamName + "," + 
                AppInstanceParamName + "," + ElapsedParamName + "," + StartParamName + "," + ProtocolParamName + "," + HostParamName + "," + 
                PathParamName + "," + ClientIPParamName + "," + UserAgentParamName + "," + HttpMethodParamName + "," + ControllerNameParamName + "," + 
                ControllerMethodParamName + "," + MethodReturnTypeParamName + "," + ResponseCodeParamName + "," + ResponseTextParamName + "," + 
                XmlDataParamName + "," + MessageIdParamName + "," + BasicTokenParamName + "," + BearerTokenParamName + "," + AuthSchemaParamName + "," + AuthValueParamName + "," + MessageBodyParamName + ")";


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
                            cmd.Parameters.AddWithValue(ClientIPParamName, model.ClientIP.Truncate(15));
                            cmd.Parameters.AddWithValue(UserAgentParamName, String.IsNullOrWhiteSpace(model.UserAgent) ? "none" : model.UserAgent.Truncate(200));
                            cmd.Parameters.AddWithValue(HttpMethodParamName, model.HttpMethod);
                            cmd.Parameters.AddWithValue(ControllerNameParamName, String.IsNullOrWhiteSpace(model.ControllerName) ? "none" : model.ControllerName.Truncate(50));
                            cmd.Parameters.AddWithValue(ControllerMethodParamName, String.IsNullOrWhiteSpace(model.ControllerMethod) ? "none" : model.ControllerMethod.Truncate(50));
                            cmd.Parameters.AddWithValue(MethodReturnTypeParamName, String.IsNullOrWhiteSpace(model.MethodReturnType) ? "none" : model.MethodReturnType.Truncate(50));
                            cmd.Parameters.AddWithValue(ResponseCodeParamName, model.ResponseCode);
                            cmd.Parameters.AddWithValue(ResponseTextParamName, model.ResponseText.Truncate(50));
                            cmd.Parameters.AddWithValue(XmlDataParamName, model.ToXmlString());
                            cmd.Parameters.AddWithValue(MessageIdParamName, model.MessageId);
                            cmd.Parameters.AddWithValue(BasicTokenParamName, String.IsNullOrWhiteSpace(model.BasicToken) ? "none" : model.BasicToken);
                            cmd.Parameters.AddWithValue(BearerTokenParamName, String.IsNullOrWhiteSpace(model.BearerToken) ? "none" : model.BearerToken);
                            cmd.Parameters.AddWithValue(AuthSchemaParamName, model.AuthSchema);
                            cmd.Parameters.AddWithValue(AuthValueParamName, model.AuthValue);
                            cmd.Parameters.AddWithValue(MessageBodyParamName, !String.IsNullOrWhiteSpace(model.MessageBody) ? (object)model.MessageBody : DBNull.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    IEventWriter writer = new EventLogWriter();
                    var props = eXtensibleConfig.GetProperties();
                    writer.WriteError(message, SeverityType.Critical, "ApiRequestSqlAccess", props);
                }                
            }

        }

 
        public static IEnumerable<ApiRequest> Get(int id)
        {
           List<ApiRequest> list = new List<ApiRequest>();
            string schema = GetSchema();
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

                            string sql = " r.ApiRequestId, r.XmlData AS XmlRequest,  l.XmlData AS XmlLog from [" + schema + "].[Error] as l right outer join [" + schema + "].[ApiRequest]  as r on l.MessageId = r.MessageId";
                            StringBuilder sb = new StringBuilder();
                            sb.Append("select ");
                            if (id > 999)
	                        {
		                        sb.Append(sql);
                                sb.Append(" where r.[ApiRequestId] = " + ApiRequestIdParamName);
                                cmd.Parameters.AddWithValue(ApiRequestIdParamName, id);
	                        }
                            else
	                        {
                                sb.Append("top " + id.ToString() + " ");
                                sb.Append(sql);
                                sb.Append(" order by r.[ApiRequestId] desc");
	                        }



                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sb.ToString();
                            cmd.CommandTimeout = 0;


                            

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    
                                    string xml = reader.GetString(reader.GetOrdinal("XmlRequest"));
                                    var apiRequest = StringToRequest(xml);
                                    apiRequest.ApiRequestId = reader.GetInt64(reader.GetOrdinal("ApiRequestId"));
                                    if (!reader.IsDBNull(reader.GetOrdinal("XmlLog")))
                                    {
                                        string xmllog = reader.GetString(reader.GetOrdinal("XmlLog"));
                                        var apiLog = StringToLog(xmllog);
                                    }
                                    list.Add(apiRequest);
                                    
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    IEventWriter writer = new EventLogWriter();
                    var props = eXtensibleConfig.GetProperties();
                    writer.WriteError(message, SeverityType.Critical, "ApiRequestSqlAccess", props);
                }
            }
            return list;
        }


        public static Page<ApiRequest> Get(string who, string path, string controller, string bearer, string basic, string code, bool hasLog, int pageSize, int pageIndex)
        {
            var page = new Page<ApiRequest>() { PageIndex = pageIndex, PageSize = pageSize };

            int i;
            page.Items = GetRequests(who, path, controller, bearer, basic, code, hasLog,pageSize, pageIndex, out i);
            page.Total = i;

            return page;
        }

        private static List<ApiRequest> GetRequests(string who, string path,string controller, string bearer, string basic, string code, bool hasLog, int pageSize, int pageIndex, out int totalCount)
        {
            List<ApiRequest> list = new List<ApiRequest>();
            totalCount = 0;
            string schema = GetSchema();
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

                            StringBuilder sb = new StringBuilder();

                            //sb.Append("select [ApiRequestId],[XmlData] from [" + schema + "].[ApiRequest]");
                            sb.Append("select r.ApiRequestId, r.XmlData AS XmlRequest, l.Id, l.ApplicationKey, l.Zone, l.AppContextInstance, l.Category, l.Severity, l.Message" + 
                                " from [" + schema + "].[Error] as l right outer join [" + schema + "].[ApiRequest]  as r on l.MessageId = r.MessageId");
                            StringBuilder sbWhere = new StringBuilder();

                            int whereCount = 0;
                            if (!String.IsNullOrWhiteSpace(who) || 
                                !String.IsNullOrWhiteSpace(path) || 
                                !String.IsNullOrWhiteSpace(controller) || 
                                !String.IsNullOrEmpty(bearer) || 
                                !String.IsNullOrEmpty(basic) || 
                                !String.IsNullOrEmpty(code) || 
                                hasLog)
                            {
                                sbWhere.Append(" where");

                                if (!String.IsNullOrEmpty(who))
                                {
                                    sbWhere.Append(" r.[AppInstance] = " + AppInstanceParamName);
                                    cmd.Parameters.AddWithValue(AppInstanceParamName, who);
                                    whereCount++;
                                }

                                if (!String.IsNullOrEmpty(path))
                                {
                                    if (whereCount > 0)
                                    {
                                        sbWhere.Append(" and");
                                    }
                                    sbWhere.Append(" r.[Path] like '%' + " + PathParamName + " + '%'");
                                    cmd.Parameters.AddWithValue(PathParamName, path);
                                    whereCount++;
                                }

                                if (!String.IsNullOrEmpty(controller))
                                {
                                    if (whereCount > 0)
                                    {
                                        sbWhere.Append(" and");
                                    }
                                    sbWhere.Append(" r.[ControllerName] like '%' + " + ControllerNameParamName + " + '%'");
                                    cmd.Parameters.AddWithValue(ControllerNameParamName, controller);
                                    whereCount++;
                                }

                                if (!String.IsNullOrEmpty(bearer))
                                {
                                    if (whereCount > 0)
                                    {
                                        sbWhere.Append(" and");
                                    }
                                    sbWhere.Append(" r.[BearerToken] like '%' + " + BearerTokenParamName + " + '%'");
                                    cmd.Parameters.AddWithValue(BearerTokenParamName, bearer);
                                    whereCount++;
                                }

                                if (!String.IsNullOrEmpty(basic))
                                {
                                    if (whereCount > 0)
                                    {
                                        sbWhere.Append(" and");
                                    }
                                    sbWhere.Append(" r.[BasicToken] like '%' + " + BasicTokenParamName + " + '%'");
                                    cmd.Parameters.AddWithValue(BasicTokenParamName, basic);
                                    whereCount++;
                                }
                                if (!String.IsNullOrEmpty(code))
                                {
                                    if (whereCount > 0)
                                    {
                                        sbWhere.Append(" and");
                                    }
                                    sbWhere.Append(" r.[ResponseCode] = " + ResponseCodeParamName);
                                    cmd.Parameters.AddWithValue(ResponseCodeParamName, code);
                                    whereCount++;
                                }
                                if (hasLog)
                                {
                                    if (whereCount > 0)
                                    {
                                        sbWhere.Append(" and");
                                    }
                                    sbWhere.Append(" r.[HasLog] = 1");
                                    whereCount++;
                                }

                                sb.Append(sbWhere.ToString());
                            }

                            sb.Append(" order by r.[ApiRequestId] desc");
                            sb.Append(" OFFSET " + PageSizeParamName + " * (" + PageIndexParamName + ") ROWS");
                            sb.Append(" FETCH NEXT " + PageSizeParamName + " ROWS ONLY");
                            cmd.Parameters.AddWithValue(PageSizeParamName, pageSize);
                            cmd.Parameters.AddWithValue(PageIndexParamName, pageIndex);

                            sb.Append(" select count (*) FROM [" + schema + "].[ApiRequest] as r (NOLOCK)");
                            sb.Append(sbWhere.ToString());

                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sb.ToString();
                            cmd.CommandTimeout = 0;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                    string xml = reader.GetString(reader.GetOrdinal("XmlRequest"));
                                    var apiRequest = StringToRequest(xml);
                                    apiRequest.ApiRequestId = reader.GetInt64(reader.GetOrdinal("ApiRequestId"));
                                    
                                    if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                                    {
                                        // <Value xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="xsd:string">rb.global.tenant.rbdigital</Value>
                                        // <Value xsi:type="xsd:string">singleview</Value>
                                        //string xmllog = reader.GetString(reader.GetOrdinal("XmlLog"));
                                        List<TypedItem> apiLog = new List<TypedItem>();// GenericSerializer.StringToGenericList<TypedItem>(xmllog);
                                        string key = reader.GetString(reader.GetOrdinal("ApplicationKey"));
                                        string zone = reader.GetString(reader.GetOrdinal("Zone"));
                                        string ctx = reader.GetString(reader.GetOrdinal("AppContextInstance"));
                                        string category = reader.GetString(reader.GetOrdinal("Category"));
                                        string severity = reader.GetString(reader.GetOrdinal("Severity"));
                                        string message = reader.GetString(reader.GetOrdinal("Message"));
                                        apiLog.Add(new TypedItem("key", key));
                                        apiLog.Add(new TypedItem("zone", zone));
                                        apiLog.Add(new TypedItem("ctx", ctx));
                                        apiLog.Add(new TypedItem("category", category));
                                        apiLog.Add(new TypedItem("severity", severity));
                                        apiLog.Add(new TypedItem("message", message));
                                        apiRequest.LogItems = apiLog;

                                    }
                                    list.Add(apiRequest);

                                }
                                if (reader.NextResult())
                                {
                                    reader.Read();
                                    totalCount = reader.GetInt32(0);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    IEventWriter writer = new EventLogWriter();
                    var props = eXtensibleConfig.GetProperties();
                    writer.WriteError(message, SeverityType.Critical, "ApiRequestSqlAccess", props);
                }
            }
            return list;
        }




        public static ApiRequest Get(Guid id)
        {
            ApiRequest item = null;

            string sql = "";
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
                    IEventWriter writer = new EventLogWriter();
                    var props = eXtensibleConfig.GetProperties();
                    writer.WriteError(message, SeverityType.Critical, "ApiRequestSqlAccess", props);
                }
            }

            return item;
        }


        private static ApiRequest StringToRequest(string xml)
        {

            var t = Activator.CreateInstance<ApiRequest>();
            Type type = typeof(ApiRequest);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            XmlSerializer serializer = new XmlSerializer(type);
            using(MemoryStream stream = new MemoryStream())
            {
                xdoc.Save(stream);
                stream.Position = 0;
                t = (ApiRequest)serializer.Deserialize(stream);
            }

            return t;
        }

        private static List<TypedItem> StringToLog(string xml)
        {
            var t = Activator.CreateInstance<List<TypedItem>>();
            Type type = typeof(List<TypedItem>);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            XmlSerializer serializer = new XmlSerializer(type);
            using (MemoryStream stream = new MemoryStream())
            {
                xdoc.Save(stream);
                stream.Position = 0;
                t = (List<TypedItem>)serializer.Deserialize(stream);
            }

            return t;
        }

        public static IEnumerable<ApiRequest> Get(int pageSize, int pageIndex, string token)
        {
            //throw new NotImplementedException();
            return new List<ApiRequest>();
        }

    }
}
