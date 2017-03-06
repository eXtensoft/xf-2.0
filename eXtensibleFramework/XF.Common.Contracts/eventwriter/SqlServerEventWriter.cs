using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common
{
    public class SqlServerEventWriter
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


        static SqlServerEventWriter()
        {
            try
            {
                var settings = ConfigurationManager.ConnectionStrings[eXtensibleConfig.LogKey];
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

        public static void Post(eXError error)
        {
            PostError(error);
        }

        public static void PostAlert(List<TypedItem> properties)
        {
            eXAlert model = new eXAlert(properties);
            if (eXtensibleConfig.ProcessAlerts)
            {
                ProcessAlert(model);
            }
            PostAlert(model);
        }

        public static void PostList(List<TypedItem> properties)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        const string xmlDocParamName = "@xdoc";
                        const string handlerKeyParamName = "@handler";
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into [log].[eXtensibleList]([handlerkey],[xmldoc]) values (" + handlerKeyParamName + "," + xmlDocParamName + ")";
                        cmd.Parameters.AddWithValue(handlerKeyParamName, "alertHandler");
                        string xml = GenericSerializer.DbParamFromItem(properties);
                        cmd.Parameters.AddWithValue(xmlDocParamName, xml);
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

        private static void ProcessAlert(eXAlert model)
        {

        }

        private static void PostAlert(eXAlert model)
        {

            const string appKeyName = "@key";
            const string appZoneName = "@zone";
            const string appInstanceName = "@instance";
            const string titleParamName = "@title";
            const string sourceParamName = "@source";
            const string importanceParamName = "@importance";
            const string urgencyParamName = "@urgency";
            const string xmlDataParamName = "@xmldata";
            const string dispositionParamName = "@disposition";
            const string createdAtParamName = "@createdat";
            try
            {
                using (SqlConnection cn = new SqlConnection(cnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        string sql = "insert into [log].[alert] ([appkey],[appzone],[appinstance],[title],[source],[importance],[urgency],[xmldata],[disposition],[createdat]) values ( " +
                            appKeyName + "," + appZoneName + "," + appInstanceName + "," + titleParamName + "," + sourceParamName + "," + importanceParamName + "," +
                            urgencyParamName + "," + xmlDataParamName + "," + dispositionParamName + "," + createdAtParamName + ")";
                        cmd.Parameters.AddWithValue(appKeyName, model.ApplicationKey);
                        cmd.Parameters.AddWithValue(appZoneName, model.Zone);
                        string appinstance;
                        if (model.Items.TryGet<string>("app.context.instance", out appinstance))
                        {
                            cmd.Parameters.AddWithValue(appInstanceName, appinstance);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(appInstanceName, "none");
                        }

                        cmd.Parameters.AddWithValue(titleParamName, model.Title);
                        cmd.Parameters.AddWithValue(sourceParamName, !String.IsNullOrWhiteSpace(model.Source) ? model.Source : "none");
                        cmd.Parameters.AddWithValue(importanceParamName, model.Importance);
                        cmd.Parameters.AddWithValue(urgencyParamName, model.Urgency);
                        var xml = GenericSerializer.DbParamFromItem(model);
                        cmd.Parameters.AddWithValue(XmlDataParamName, xml);
                        cmd.Parameters.AddWithValue(dispositionParamName, model.Dispositions.ToConcat(";", true));
                        cmd.Parameters.AddWithValue(createdAtParamName, DateTime.Now);

                        cmd.CommandText = sql;
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

        public static void Post(List<TypedItem> properties)
        {
            //throw new NotImplementedException();
        }

        private static void PostError(eXError model)
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
                            string schema = eXtensibleConfig.Zone.Equals("production", StringComparison.OrdinalIgnoreCase) ? DateTime.Today.ToString("MMM").ToLower() : "log";

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
                    props.Add("location","SqlServerEventWriter.line.105");
                    writer.WriteError(m,SeverityType.Critical,"EventWriter",props);
                }
               
            }

        }


    }
}
