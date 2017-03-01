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
