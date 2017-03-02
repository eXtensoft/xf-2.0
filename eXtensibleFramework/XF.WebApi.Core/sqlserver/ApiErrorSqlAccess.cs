
namespace XF.WebApi
{

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using XF.Common;

    public static class ApiErrorSqlAccess
    {
        public static Page<ApiError> Get(string messageId)
        {
            Page<ApiError> page = new Page<ApiError>() {Items = new List<ApiError>() };
            if (!String.IsNullOrWhiteSpace(messageId))
            {
                Guid id = new Guid(messageId);
                string schema = eXtensibleConfig.Zone.Equals("production", StringComparison.OrdinalIgnoreCase) ? DateTime.Today.ToString("MMM").ToLower() : "log";
                var settings = ConfigurationManager.ConnectionStrings[eXtensibleWebApiConfig.SqlConnectionKey];
                if (settings != null && !String.IsNullOrWhiteSpace(settings.ConnectionString))
                {
                    try
                    {
                        List<ApiError> list = new List<ApiError>();
                        using (SqlConnection cn = new SqlConnection(settings.ConnectionString))
                        {
                            cn.Open();
                            using (SqlCommand cmd = cn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.Text;
                                const string MessageIdParamName = "@messageid";
                                string sql = "select [Id], [CreatedAt], [ApplicationKey], [Zone], [AppContextInstance], [MessageId]," +
                                    "[Category], [Severity], [Message], [XmlData] from [log].[Error] where [MessageId] = " + MessageIdParamName;
                                cmd.Parameters.AddWithValue(MessageIdParamName, id);
                                cmd.CommandText = sql;

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    BorrowReader(reader, list);

                                    if (list.Count > 0)
                                    {
                                        page.Items.Add( list[0]);
                                        page.Total = 1;
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
            }
            
            return page;
        }

        public static Page<ApiError> Get(string zone, int pageSize, int pageIndex)
        {
            Page<ApiError> page = new Page<ApiError>();

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
                            cmd.CommandType = CommandType.Text;

                            string sql = "select top " + pageSize + " [Id], [CreatedAt], [ApplicationKey], [Zone], [AppContextInstance], [MessageId], " +
                                "[Category], [Severity], [Message], [XmlData] from [log].[Error]  order by [Id] desc";
                            cmd.CommandText = sql;
                            List<ApiError> list = new List<ApiError>();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                BorrowReader(reader, list);
                                page.Items = list;
                                page.Total = list.Count;
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

        private static void BorrowReader(SqlDataReader reader, List<ApiError> list)
        {
            while (reader.Read())
            {
                var model = new ApiError();
                model.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                model.ApplicationKey = reader.GetString(reader.GetOrdinal("ApplicationKey"));
                model.Zone = reader.GetString(reader.GetOrdinal("Zone"));
                model.AppContextInstance = reader.GetString(reader.GetOrdinal("AppContextInstance"));
                model.MessageId = reader.GetGuid(reader.GetOrdinal("MessageId"));
                model.Category = reader.GetString(reader.GetOrdinal("Category"));
                model.Severity = reader.GetString(reader.GetOrdinal("Severity"));
                model.Message = reader.GetString(reader.GetOrdinal("Message"));
                model.Items = reader.GetSqlXml(reader.GetOrdinal("XmlData")).Value;

                //if (!xmldoc.IsNull)
                //{
                  
                //    List<TypedItem> items = GenericSerializer.StringToGenericList<TypedItem>(xmldoc.Value);
                //    var x = items;
                //    // model.ComplexProperty = GenericSerializer.StringToGenericItem<[typename]>(xmldoc.Value);
                //}
                list.Add(model);

            }
        }

        private static string GetString(XmlDocument xml)
        {
            if (xml.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
            {
                var declaration = (XmlDeclaration)xml.FirstChild;
                declaration.Encoding = "UTF-16";
            }
            xml.DocumentElement.RemoveAttribute("xmlns:xsi");
            xml.DocumentElement.RemoveAttribute("xmlns:xsd");
            return xml.OuterXml;
        }

    }
}
