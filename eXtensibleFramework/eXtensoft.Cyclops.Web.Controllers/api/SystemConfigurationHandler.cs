

namespace Cyclops.Api.Controllers
{
    //using MongoDB.Driver;
    //using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using XF.Common;
    using XF.WebApi;

    public class SystemConfigurationHandler
    {

        public static DataTable GetEnvironment()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("scope", typeof(String));
            dt.Columns.Add("name", typeof(String));
            dt.Columns.Add("value", typeof(String));

            dt.Rows.Add("environment", "appKey", eXtensibleConfig.Context);
            dt.Rows.Add("environment", "zone", eXtensibleConfig.Zone);
            dt.Rows.Add("environment", "instance", eXtensibleConfig.InstanceIdentifier);
            dt.Rows.Add("environment", "logMode", eXtensibleConfig.LoggingMode);
            dt.Rows.Add("environment", "logSeverity", eXtensibleConfig.LoggingSeverity);
            dt.Rows.Add("environment", "logStrategy", eXtensibleConfig.LoggingStrategy);
            dt.Rows.Add("environment", "logKey", eXtensibleConfig.LogKey);
            dt.Rows.Add("environment", "logSource", eXtensibleConfig.LogSource);
            dt.Rows.Add("environment", "captureMetrics", eXtensibleConfig.CaptureMetrics);

            dt.Rows.Add("environment", "api_zone", eXtensibleWebApiConfig.Zone);
            dt.Rows.Add("environment", "api_isLogToDatastore", eXtensibleWebApiConfig.IsLogToDatastore);
            dt.Rows.Add("environment", "api_logMode", eXtensibleWebApiConfig.LoggingMode);
            dt.Rows.Add("environment", "api_logSource", eXtensibleWebApiConfig.LogSource);
            dt.Rows.Add("environment", "api_logTo", eXtensibleWebApiConfig.LogTo);
            dt.Rows.Add("environment", "api_logConnectionKey", eXtensibleWebApiConfig.SqlConnectionKey);
            return dt;
        }


        public static DataTable GetAppSettings()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("scope", typeof(String));
            dt.Columns.Add("name", typeof(String));
            dt.Columns.Add("value", typeof(String));

            NameValueCollection nvc = ConfigurationProvider.AppSettings;
            foreach (string key in nvc.AllKeys)
            {
                dt.Rows.Add("setting", key, nvc[key]);
            }

            return dt;
        }
        public static DataTable GetConnections(bool b)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("scope", typeof(String)));
            dt.Columns.Add(new DataColumn("name", typeof(String)));
            dt.Columns.Add(new DataColumn("value", typeof(String)));
            dt.Columns.Add(new DataColumn("connection", typeof(String)));
            dt.Columns.Add(new DataColumn("isOkay", typeof(bool)));
            var settings = ConfigurationProvider.ConnectionStrings;

            SortedDictionary<string, string> sqlserver = new SortedDictionary<string, string>();
            SortedDictionary<string, string> mysql = new SortedDictionary<string, string>();
            SortedDictionary<string, string> mongo = new SortedDictionary<string, string>();
            SortedDictionary<string, string> redis = new SortedDictionary<string, string>();

            foreach (ConnectionStringSettings setting in settings)
            {
                if (setting.ProviderName.Equals("System.Data.SqlClient", StringComparison.OrdinalIgnoreCase)
                    && !setting.Name.Equals("localsqlserver", StringComparison.OrdinalIgnoreCase))
                {
                    sqlserver.Add(setting.Name.ToLower(), setting.ConnectionString);
                }
                else if (setting.ProviderName.Equals("MySql.Data.MySqlClient", StringComparison.OrdinalIgnoreCase))
                {
                    mysql.Add(setting.Name.ToLower(), setting.ConnectionString);
                }
                else if (setting.ProviderName.Equals("MongoDb.Driver", StringComparison.OrdinalIgnoreCase))
                {
                    mongo.Add(setting.Name.ToLower(), setting.ConnectionString);
                }
                else if (setting.ProviderName.Equals("StackExchange.Redis", StringComparison.OrdinalIgnoreCase))
                {
                    redis.Add(setting.Name.ToLower(), setting.ConnectionString);
                }

            }

            TestSqlServer(dt, sqlserver);
            //TestMySql(dt, mysql);
            //TestMongo(dt, mongo);

            if (!b)
            {
                dt.Columns.RemoveAt(3);
            }
            dt.Columns.RemoveAt(0);

            return dt;
        }
        private static void TestSqlServer(DataTable dt, SortedDictionary<string, string> d)
        {
            bool isOkay = false;
            foreach (string name in d.Keys)
            {
                string message = String.Empty;
                string connectionString = d[name];
                try
                {
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        cn.Open();
                        if (cn.State == ConnectionState.Open)
                        {
                            message = "connection successful";
                            isOkay = true;
                        }
                        else
                        {
                            message = "no connection";
                        }

                    }
                }
                catch (Exception ex)
                {
                    message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                }
                dt.Rows.Add("sqlserver", name, message, connectionString, isOkay);
            }
        }


        //private static void TestMySql(DataTable dt, SortedDictionary<string, string> d)
        //{
        //    bool isOkay = false;
        //    foreach (string name in d.Keys)
        //    {
        //        string message = String.Empty;
        //        string connectionString = d[name];
        //        try
        //        {
        //            using (MySqlConnection cn = new MySqlConnection(connectionString))
        //            {
        //                cn.Open();
        //                message = "connection successful";
        //                isOkay = true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        }
        //        dt.Rows.Add("mysql", name, message, connectionString, isOkay);
        //    }
        //}

        //private static void TestMongo(DataTable dt, SortedDictionary<string, string> d)
        //{
        //    bool isOkay = false;
        //    foreach (string name in d.Keys)
        //    {
        //        string message = String.Empty;
        //        string connectionString = d[name];
        //        try
        //        {
        //            MongoConnectionInfo info = new MongoConnectionInfo();
        //            if (info.Initialize(name))
        //            {
        //                MongoDatabase mongodb = info.GetDatabase();
        //                message = "connection successful";
        //                isOkay = true;
        //            }
        //            else
        //            {
        //                message = "no connection";
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        }
        //        dt.Rows.Add("mongo", name, message, connectionString, isOkay);
        //    }
        //}



    }
}
