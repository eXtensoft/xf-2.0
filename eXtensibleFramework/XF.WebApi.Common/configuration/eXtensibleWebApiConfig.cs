// <copyright company="Recorded Books, Inc" file="eXtensibleWebApiConfig.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
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

        static eXtensibleWebApiConfig()
        {
            try
            {
                string configfilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                string configFolder = Path.GetDirectoryName(configfilepath);

                WebApiPlugins = configFolder + "\\" + "webApiControllers";


                LoggingModeOption loggingMode;
                LoggingStrategyOption loggingStrategy;
                string logToCandidate = ConfigurationProvider.AppSettings[XFWebApiConstants.Config.LogToKey];
                string loggingModeCandidate = ConfigurationProvider.AppSettings[XFWebApiConstants.Config.LoggingModeKey];
                string sqlConnectionKeyCandidate = ConfigurationProvider.AppSettings[XFWebApiConstants.Config.SqlConnectionKey];

                var configfilemap = new ExeConfigurationFileMap() { ExeConfigFilename = configfilepath };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configfilemap, ConfigurationUserLevel.None);
                eXtensibleWebApiSection section = config.Sections[XFWebApiConstants.Config.SectionName] as eXtensibleWebApiSection;

                
                if (section != null)
                {
                    MessageProviderFolder = section.MessageProviderFolder;
                    var found = section.Elements.GetForLoggingMode(section.LoggingKey);

                    if (Enum.TryParse<LoggingStrategyOption>(found.LoggingStrategy, true, out loggingStrategy)
                        && Enum.TryParse<LoggingModeOption>(found.LoggingMode, true, out loggingMode))
                    {
                        if (loggingStrategy == LoggingStrategyOption.Datastore)
                        {
                            SqlConnectionKey = found.DatastoreKey;
                        }
                        LoggingMode = loggingMode;
                        LogTo = loggingStrategy;
                    }
                    else
                    {
                        loggingMode = XFWebApiConstants.Default.LoggingMode;
                        LogTo = XFWebApiConstants.Default.LogTo;
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
                    }
                    else
                    {
                        LoggingMode = XFWebApiConstants.Default.LoggingMode;
                    }
                    // read from appsettings or use defaults
                    if (!String.IsNullOrWhiteSpace(logToCandidate) && Enum.TryParse<LoggingStrategyOption>(logToCandidate, true, out loggingStrategy))
                    {
                        LogTo = loggingStrategy;
                    }
                    else
                    {
                        LogTo = XFWebApiConstants.Default.LogTo;
                    }

                    if (!String.IsNullOrWhiteSpace(sqlConnectionKeyCandidate))
                    {
                        SqlConnectionKey = sqlConnectionKeyCandidate;
                    }
                    else
                    {
                        SqlConnectionKey = XFWebApiConstants.Default.DatastoreConnectionKey;
                    }

                    if (LogTo.Equals(LoggingStrategyOption.Datastore))
                    {
                        try
                        {
                            string cnText = ConfigurationProvider.ConnectionStrings[SqlConnectionKey].ConnectionString;
                            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(cnText))
                            {
                                cn.Open();
                                if (cn.State == System.Data.ConnectionState.Open)
                                {
                                    IsLogToDatastore = true;
                                }
                                else
                                {
                                    LogTo = XFWebApiConstants.Default.LogTo;
                                }
                            }
                        }
                        catch
                        {
                            LogTo = LoggingStrategyOption.None;
                        }
                    }
                    MessageProviderFolder = XFWebApiConstants.Default.MessageProviderFolder;
                    IsEditRegistration = XFWebApiConstants.Default.IsEditRegistration;
                    CatchAll = XFWebApiConstants.Default.CatchAllControllerId;
                    MessageIdHeaderKey = XFWebApiConstants.Default.MessageIdHeaderKey;
                }


            }
            catch
            {
                // nows setup defaults
                MessageProviderFolder = XFWebApiConstants.Default.MessageProviderFolder;
                LoggingMode = XFWebApiConstants.Default.LoggingMode;
                LogTo = XFWebApiConstants.Default.LogTo;
                IsEditRegistration = XFWebApiConstants.Default.IsEditRegistration;
                CatchAll = XFWebApiConstants.Default.CatchAllControllerId;
                MessageIdHeaderKey = XFWebApiConstants.Default.MessageIdHeaderKey;
            }
        }
    }

}
