using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using XF.Common;

namespace XF.Logging.Eventwriter
{
    [Export(typeof(IEventWriter))]
    public class SqlServerEventWriter : EventWriterBase
    {
        private static string cnString;
        private static bool isInitialized;


        private const string ApplicationKeyParamName = "@applicationkey";
        private const string ZoneParamName = "@zone";
        private const string AppContextInstanceParamName = "@appcontextinstance";
        private const string MessageIdParamName = "@messageid";
        private const string CategoryParamName = "@category";
        private const string SeverityParamName = "@severity";
        private const string MessageParamName = "@message";
        private const string XmlDataParamName = "@xmldata";
        private const string DbSchema = "log";

        protected override void Publish(EventTypeOption eventType, List<TypedItem> properties)
        {
            switch (eventType)
            {
                case EventTypeOption.Error:
                    eXError error = new eXError(properties);
                    Post(error);
                    break;
                case EventTypeOption.Alert:
                    PostAlert(properties);
                    break;
                case EventTypeOption.Status:
                case EventTypeOption.Task:
                case EventTypeOption.Kpi:
                case EventTypeOption.None:
                case EventTypeOption.Event:
                default:
                    PostList(properties);
                    break;
            }
        }

        protected override void Publish(eXMetric metric)
        {
            //throw new NotImplementedException();
        }

        static SqlServerEventWriter()
        {
            try
            {
                var settings = ConfigurationProvider.ConnectionStrings[eXtensibleConfig.LogKey];
                if (settings != null && !String.IsNullOrWhiteSpace(settings.ConnectionString))
                {
                    cnString = settings.ConnectionString;
                    using (SqlConnection cn = new SqlConnection(cnString))
                    {
                        cn.Open();
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            isInitialized = true;
                        }
                    }
                }
            }
            catch
            {
                isInitialized = false;
                cnString = String.Empty;
            }

        }

        private void PostList(List<TypedItem> properties)
        {
            throw new NotImplementedException();
        }

        private void PostAlert(List<TypedItem> properties)
        {
            throw new NotImplementedException();
        }

        private void Post(eXError error)
        {
            throw new NotImplementedException();
        }

        private static void LocalPostError(eXError model)
        {
            if (isInitialized)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(cnString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            string schema = GetSchema();
                            string sql = "insert into [" + schema + "].[Error] ( [ApplicationKey],[Zone],[AppContextInstance],[MessageId]," +
                                "[Category],[Severity],[Message],[XmlData] ) values (" +
                                ApplicationKeyParamName + "," + ZoneParamName + "," + AppContextInstanceParamName + "," +
                                MessageIdParamName + "," + CategoryParamName + "," + SeverityParamName + "," +
                                MessageParamName + "," + XmlDataParamName + ")";

                            cmd.CommandText = sql;

                            cmd.Parameters.AddWithValue(ApplicationKeyParamName, model.ApplicationKey.Truncate(15));
                            cmd.Parameters.AddWithValue(ZoneParamName, model.Zone.Truncate(15));
                            cmd.Parameters.AddWithValue(AppContextInstanceParamName, model.AppContextInstance.Truncate(25));
                            cmd.Parameters.AddWithValue(MessageIdParamName, model.MessageId);
                            cmd.Parameters.AddWithValue(CategoryParamName, model.Category.Truncate(25));
                            cmd.Parameters.AddWithValue(SeverityParamName, model.Severity.Truncate(15));
                            cmd.Parameters.AddWithValue(MessageParamName, model.Message.Truncate(100));
                            string items = GenericSerializer.DbParamFromItem(model.Items);
                            cmd.Parameters.AddWithValue(XmlDataParamName, items);

                            cmd.ExecuteNonQuery();
                        }

                    }
                }
                catch (Exception ex)
                {
                    IEventWriter writer = new EventLogWriter();
                    string m = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties();
                    props.Add("location", "SqlServerEventWriter.line.105");
                    writer.WriteError(m, SeverityType.Critical, "EventWriter", props);
                }

            }

        }


        private static string GetSchema()
        {
            DateTime now = DateTime.Now;
            return now.ToSchema(eXtensibleConfig.LoggingSchema, "log");
        }
    }
}
