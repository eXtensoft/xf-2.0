// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using XF.Common;
    using XF.WebApi.Config;

    public static class eXtensibleWebApiConfig
    {

        public static readonly string Zone;
        public static readonly string WebApiPlugins;

        public static readonly string LogSource;
        public static readonly string BigDataUrl;
        public static readonly string ServiceToken;

        public static readonly bool IsLogToDatastore;
        public static readonly LoggingStrategyOption LogTo;
        public static readonly string SqlConnectionKey;

        public static readonly LoggingModeOption LoggingMode;

        public static readonly string MessageProviderFolder;

        public static readonly bool IsEditRegistration;

        public static readonly string CatchAll;

        public static readonly string MessageIdHeaderKey;

        public static DateTimeSchemaOption LoggingSchema;

        static eXtensibleWebApiConfig()
        {
            IEventWriter writer = new EventLogWriter();
            StringBuilder message = new StringBuilder();
            var props = eXtensibleConfig.GetProperties();
            try
            {
                string configfilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                string configFolder = Path.GetDirectoryName(configfilepath);

                WebApiPlugins = configFolder + "\\" + "webApiControllers";

                DateTimeSchemaOption loggingSchema;
                LoggingModeOption loggingMode;
                LoggingStrategyOption loggingStrategy;
                string logSchemaCandidate = ConfigurationProvider.AppSettings[XFWebApiConstants.Config.LoggingSchema];
                string logToCandidate = ConfigurationProvider.AppSettings[XFWebApiConstants.Config.LogToKey];
                string loggingModeCandidate = ConfigurationProvider.AppSettings[XFWebApiConstants.Config.LoggingModeKey];
                string sqlConnectionKeyCandidate = ConfigurationProvider.AppSettings[XFWebApiConstants.Config.SqlConnectionKey];

                var configfilemap = new ExeConfigurationFileMap() { ExeConfigFilename = configfilepath };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configfilemap, ConfigurationUserLevel.None);
                eXtensibleWebApiSection section = config.Sections[XFWebApiConstants.Config.SectionName] as eXtensibleWebApiSection;
                message.AppendLine(String.Format("{0}: {1}", "configFolder", configFolder));

                if (section != null)
                {
                    MessageProviderFolder = section.MessageProviderFolder;
                    var found = section.Elements.GetForLoggingMode(section.LoggingKey);
                    if (found != null)
                    {
                        message.AppendLine(String.Format("{0}: {1}", XFWebApiConstants.Config.SectionName, "found"));
                    }

                    if (Enum.TryParse<LoggingStrategyOption>(found.LoggingStrategy, true, out loggingStrategy)
                        && Enum.TryParse<LoggingModeOption>(found.LoggingMode, true, out loggingMode))
                    {
                        if (loggingStrategy == LoggingStrategyOption.Datastore)
                        {
                            SqlConnectionKey = found.DatastoreKey;
                        }
                        LoggingMode = loggingMode;
                        LogTo = loggingStrategy;
                        if (!String.IsNullOrEmpty(found.LoggingSchema)
                            && Enum.TryParse<DateTimeSchemaOption>(found.LoggingSchema, true, out loggingSchema))
                        {
                            LoggingSchema = loggingSchema;
                        }

                        message.AppendLine(String.Format("{0}: {1}", "parsing", "success"));
                    }
                    else
                    {
                        loggingMode = XFWebApiConstants.Default.LoggingMode;
                        LogTo = XFWebApiConstants.Default.LogTo;
                        message.AppendLine(String.Format("{0}: {1}", "parsing", "failure"));
                    }

                    IsEditRegistration = section.EditRegistration;
                    CatchAll = section.CatchAllControllerId;
                    MessageIdHeaderKey = section.MessageIdHeaderKey;

                }
                else
                {
                    IsEditRegistration = false;
                    if (!String.IsNullOrWhiteSpace(loggingModeCandidate) && Enum.TryParse<LoggingModeOption>(loggingModeCandidate, true, out loggingMode))
                    {
                        LoggingMode = loggingMode;
                        message.AppendLine(String.Format("{0}: {1}", "parse LoggingMode", true));
                    }
                    else
                    {
                        LoggingMode = XFWebApiConstants.Default.LoggingMode;
                        message.AppendLine(String.Format("{0}: {1}", "parse LoggingMode", false));
                    }
                    // read from appsettings or use defaults
                    if (!String.IsNullOrWhiteSpace(logToCandidate) && Enum.TryParse<LoggingStrategyOption>(logToCandidate, true, out loggingStrategy))
                    {
                        LogTo = loggingStrategy;
                        message.AppendLine(String.Format("{0}: {1}", "parse LoggingTo", true));
                    }
                    else
                    {
                        LogTo = XFWebApiConstants.Default.LogTo;
                        message.AppendLine(String.Format("{0}: {1}", "parse LoggingTo", false));
                    }

                    if (!String.IsNullOrWhiteSpace(sqlConnectionKeyCandidate))
                    {
                        SqlConnectionKey = sqlConnectionKeyCandidate;
                        message.AppendLine(String.Format("{0}: {1}", "key", SqlConnectionKey));
                    }
                    else
                    {
                        SqlConnectionKey = XFWebApiConstants.Default.DatastoreConnectionKey;
                        message.AppendLine(String.Format("{0}: {1}", "key", "default"));
                    }

                    if (!String.IsNullOrWhiteSpace(logSchemaCandidate)
                        && Enum.TryParse<DateTimeSchemaOption>(logSchemaCandidate, true, out loggingSchema))
                    {
                        LoggingSchema = loggingSchema;
                    }
                    else
                    {
                        LoggingSchema = DateTimeSchemaOption.None;
                    }

                    if (LogTo.Equals(LoggingStrategyOption.Datastore))
                    {
                        try
                        {
                            string cnText = ConfigurationProvider.ConnectionStrings[SqlConnectionKey].ConnectionString;
                            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(cnText))
                            {
                                cn.Open();
                                message.AppendLine(String.Format("{0}: {1}", "cnState", cn.State));

                                if (cn.State == System.Data.ConnectionState.Open)
                                {
                                    IsLogToDatastore = true;
                                }
                                else
                                {
                                    LogTo = XFWebApiConstants.Default.LogTo;
                                    message.AppendLine(String.Format("{0}: {1}: {2}", "LogTo", "revert to default", LogTo));
                                }
                            }
                        }
                        catch (Exception innerEx)
                        {
                            LogTo = LoggingStrategyOption.None;
                            message.AppendLine(String.Format("{0}: {1}: {2}", "LogTo", "on error", LogTo));
                            string m = innerEx.InnerException != null ? innerEx.InnerException.Message : innerEx.Message;
                            message.AppendLine(m);
                        }
                    }
                    MessageProviderFolder = XFWebApiConstants.Default.MessageProviderFolder;
                    IsEditRegistration = XFWebApiConstants.Default.IsEditRegistration;
                    CatchAll = XFWebApiConstants.Default.CatchAllControllerId;
                    MessageIdHeaderKey = XFWebApiConstants.Default.MessageIdHeaderKey;
                }


            }
            catch (Exception ex)
            {
                // nows setup defaults
                MessageProviderFolder = XFWebApiConstants.Default.MessageProviderFolder;
                LoggingMode = XFWebApiConstants.Default.LoggingMode;
                LogTo = XFWebApiConstants.Default.LogTo;
                IsEditRegistration = XFWebApiConstants.Default.IsEditRegistration;
                CatchAll = XFWebApiConstants.Default.CatchAllControllerId;
                MessageIdHeaderKey = XFWebApiConstants.Default.MessageIdHeaderKey;
                var m = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                message.AppendLine(m);
            }
            //writer.WriteError(message.ToString(), SeverityType.Critical, "webApiconfig", props);
            writer.WriteEvent(message.ToString(), "webApiConfig", props);
        }
    }

}