// <copyright company="eXtensible Solutions, LLC" file="eXtensibleConfig.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;

    public static class eXtensibleConfig
    {
        public static readonly string Scope;
        public static readonly bool Extant;
        public static readonly string Exception;
        public static readonly string Zone;
        public static readonly string InstanceIdentifier;
        public static readonly LoggingStrategyOption LoggingStrategy;
        public static readonly LoggingModeOption LoggingMode;
        public static readonly TraceEventTypeOption LoggingSeverity;
        public static DateTimeSchemaOption LoggingSchema;
        public static readonly string ModelServicesStrategySectionGroupName;
        public static readonly string DataServicesStrategySectionGroupName;
        public static readonly string DataPlugins;
        public static readonly string ModelPlugins;
        public static readonly string ServicePlugins;
        public static readonly string ModelDataGatewayPlugins;
        public static readonly string RemoteProcedureCallPlugins;
        public static readonly string ConfigurationProviderPlugins;
        public static readonly string DbConfigs;
        public static readonly string BaseDirectory;
        public static readonly string Context;
        public static readonly bool IsAsync;
        public static readonly string AppUserIdentity;
        public static readonly string DefaultLoggingCategory;
        public static readonly string ConnectionStringKey;
        public static readonly string LogSource;
        public static readonly string BigDataUrl;
        public static readonly string ServiceToken;
        public static readonly bool Inform;
        public static readonly string LogKey;
        public static readonly bool HandleAlerts;
        public static readonly string ApiRoot;
        public static readonly string ApiErrors;
        public static readonly string ApiEvents;
        public static readonly string ApiStatii;
        public static readonly string ApiMetrics;
        public static readonly string ApiAlerts;
        public static readonly string ApiTasks;
        public static readonly string ApiKpi;
        public static readonly string ApiCustom;

        public static readonly bool CaptureMetrics;

        public static readonly bool Infer;


        private static readonly Dictionary<string, object> _ExtendedProperties = new Dictionary<string,object>();

        static eXtensibleConfig()
        {
            try
            {
                string configfilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                string configFolder = Path.GetDirectoryName(configfilepath);
                ModelPlugins = configFolder + "\\" + "model";
                ModelDataGatewayPlugins = configFolder + "\\" + "mdg";
                ServicePlugins = configFolder + "\\" + "services";
                RemoteProcedureCallPlugins = configFolder + "\\" + "rpc";
                DbConfigs = configFolder + "\\" + "db.configs";
                ConfigurationProviderPlugins = configFolder + "\\" + "config.provider";
                BaseDirectory = configFolder;

                DateTimeSchemaOption loggingSchema;

                var configfilemap = new ExeConfigurationFileMap() { ExeConfigFilename = configfilepath };
                Configuration config = ConfigurationProvider.OpenMappedExeConfiguration(configfilemap, ConfigurationUserLevel.None);
                eXtensibleFrameworkSection section = config.Sections[XFConstants.Config.SectionName] as eXtensibleFrameworkSection;
                if (section != null)
                {
                    string candidateContext = ConfigurationProvider.AppSettings[XFConstants.Application.Config.ApplicationKey];
                    string candidateZone = ConfigurationProvider.AppSettings[XFConstants.Application.Config.ZoneKey];
                    //string candidateLogging = ConfigurationProvider.AppSettings[XFConstants.Application.Config.LoggingStrategyKey];
                    //string candidateLoggingSeverity = ConfigurationProvider.AppSettings[XFConstants.Application.Config.LoggingSeverityKey];
                    string candidateConnectionStringKey = ConfigurationProvider.AppSettings[XFConstants.Application.Config.ConnectionStringKey];
                    string candidateLogSource = ConfigurationProvider.AppSettings[XFConstants.Application.Config.LogSourceKey];
                    string logSchemaCandidate = ConfigurationProvider.AppSettings[XFConstants.Application.Config.LoggingSchema];
                    string candidateBigDataUrl = ConfigurationProvider.AppSettings[XFConstants.Application.Config.BigDataUrlKey];
                    string candidateServiceToken = ConfigurationProvider.AppSettings[XFConstants.Application.Config.ServiceTokenKey];
                    string candidateInstance = ConfigurationProvider.AppSettings[XFConstants.Application.Config.InstanceIdentifierKey];
                    string candidateInfer = ConfigurationProvider.AppSettings[XFConstants.Application.Config.InferKey];

                    ZoneOption option;
                    if (Enum.TryParse<ZoneOption>(section.Zone, true, out option))
                    {
                        Zone = option.ToString();
                    }
                    else if(!String.IsNullOrEmpty(candidateZone) && Enum.TryParse<ZoneOption>(candidateZone,true, out option))
                    {
                        Zone = option.ToString();
                    }
                    else
                    {
                        Zone = ZoneOption.Development.ToString();
                    }

                    if (!String.IsNullOrEmpty(section.Context))
                    {
                        Context = section.Context;
                    }
                    else
                    {
                        Context = !String.IsNullOrEmpty(candidateContext) ? candidateContext : XFConstants.Application.DefaultAppplicationKey;
                    }

                    HandleAlerts = section.HandleAlerts;
                    
                    if (!String.IsNullOrEmpty(section.InstanceIdentifier))
                    {
                        if (section.InstanceIdentifier.Equals("machine", StringComparison.OrdinalIgnoreCase))
                        {
                            InstanceIdentifier = Environment.MachineName.ToLower();
                        }
                        else
                        {
                            InstanceIdentifier = section.InstanceIdentifier;
                        }
                        
                    }
                    else if (!String.IsNullOrEmpty(candidateInfer))
                    {
                        if (candidateInfer.Equals("machine", StringComparison.OrdinalIgnoreCase))
                        {
                            InstanceIdentifier = Environment.MachineName.ToLower();
                        }
                        else
                        {
                            InstanceIdentifier = candidateInfer;
                        }

                        InstanceIdentifier = !String.IsNullOrEmpty(candidateInstance) ? candidateInstance : new Guid().ToString();
                    }
                    else
	                { 
                        InstanceIdentifier = Environment.MachineName.ToLower();

	                }
                    bool infer;
                    if (!String.IsNullOrWhiteSpace(candidateInfer) && Boolean.TryParse(candidateInfer, out infer))
                    {
                        Infer = infer;
                    }
                    else
                    {
                        Infer = false;
                    }

                    bool inform = false;
                    var found = section.Elements.GetForLoggingMode(section.LoggingKey);
                    LogKey = !String.IsNullOrWhiteSpace(found.DatastoreKey) ? found.DatastoreKey : String.Empty;
                    LoggingStrategyOption loggingStrategy;
                    if (Enum.TryParse<LoggingStrategyOption>(found.LoggingStrategy,true, out loggingStrategy))
                    {
                        LoggingStrategy = loggingStrategy;
                        inform = found.Inform;
                        if (!String.IsNullOrWhiteSpace(found.LoggingSchema) 
                            && Enum.TryParse<DateTimeSchemaOption>(found.LoggingSchema,true,out loggingSchema))
                        {
                            LoggingSchema = loggingSchema;
                        }

                    }
                    else
                    {
                        LoggingStrategy = LoggingStrategyOption.Silent;
                    }
                    Inform = inform;


                    TraceEventTypeOption eventType;
                    if (Enum.TryParse<TraceEventTypeOption>(found.PublishSeverity,true, out eventType))
                    {
                        LoggingSeverity = eventType;
                    }
                    else
                    {
                        LoggingSeverity = TraceEventTypeOption.Verbose;
                    }

                    if (!String.IsNullOrEmpty(section.LogSource))
                    {
                        LogSource = section.LogSource;
                    }
                    else if (!String.IsNullOrEmpty(candidateLogSource))
                    {
                        LogSource = candidateLogSource;
                    }
                    else
                    {
                        LogSource = XFConstants.Config.DefaultLogSource;
                    }


                    if (!String.IsNullOrEmpty(section.BigDataUrl))
                    {
                        BigDataUrl = section.BigDataUrl;
                    }
                    else if (!String.IsNullOrEmpty(candidateBigDataUrl))
                    {
                        BigDataUrl = candidateBigDataUrl;
                    }
                    else
                    {
                        //
                    }

                    if (!String.IsNullOrEmpty(section.ServiceToken))
                    {
                        ServiceToken = section.ServiceToken;
                    }
                    else if(!String.IsNullOrEmpty(candidateServiceToken))
                    {
                        ServiceToken = candidateServiceToken;
                    }
                    else
                    {
                        ServiceToken = XFConstants.Config.DefaultServiceToken;
                    }

                    if (!String.IsNullOrEmpty(candidateConnectionStringKey))
                    {
                        ConnectionStringKey = candidateConnectionStringKey;
                    }
                    CaptureMetrics = section.CaptureMetrics;

                    IsAsync = false;

                    AppUserIdentity = XFConstants.Config.AppUserIdentityParamName;
                    DefaultLoggingCategory = XFConstants.Category.General;

                    ApiRoot = XFConstants.Api.DefaultRoot;
                    ApiErrors = XFConstants.Api.DefaultErrorsEndpoint;
                    ApiEvents = XFConstants.Api.DefaultEventsEndpoint;
                    ApiStatii = XFConstants.Api.DefaultStatiiEndpoint;
                    ApiMetrics = XFConstants.Api.DefaultMetricsEndpoint;
                    ApiAlerts = XFConstants.Api.DefaultAlertsEndpoint;
                    ApiTasks = XFConstants.Api.DefaultTasksEndpoint;
                    ApiKpi = XFConstants.Api.DefaultKpiEndpoint;
                    ApiCustom = XFConstants.Api.DefaultCustomEndpoint;
                }
                else
                {
                    section = new eXtensibleFrameworkSection();
                    Zone = section.Zone = XFConstants.ZONE.Development;
                    Context = section.Context = XFConstants.Config.DefaultApplicationKey;
                    IsAsync = section.IsAsync = false;
                    InstanceIdentifier = section.InstanceIdentifier = String.Empty;
                    AppUserIdentity = section.UserIdentityParamName = XFConstants.Config.AppUserIdentityParamName;
                    DefaultLoggingCategory = XFConstants.Category.General;
                    LoggingStrategy = LoggingStrategyOption.WindowsEventLog;
                    LoggingSeverity = TraceEventTypeOption.Verbose;
                    LogSource = XFConstants.Config.DefaultLogSource;
                    ServiceToken = XFConstants.Config.DefaultServiceToken;
                    Infer = false;
                }

            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                Zone = XFConstants.Config.DefaultZone;
                DataPlugins = XFConstants.Config.DATAACCESSCONTEXTFOLDERPATH;
                DefaultLoggingCategory = XFConstants.Category.General;
                LoggingStrategyOption option;
                if (Enum.TryParse<LoggingStrategyOption>(XFConstants.Config.DefaultLoggingStrategy, true, out option))
                {
                    LoggingStrategy = option;
                }
                else
                {
                    LoggingStrategy = LoggingStrategyOption.Silent;
                }
                TraceEventTypeOption severity;
                if (Enum.TryParse<TraceEventTypeOption>(XFConstants.Config.DefaultPublishingSeverity, true, out severity))
                {
                    LoggingSeverity = severity;
                }
                else
                {
                    LoggingSeverity = TraceEventTypeOption.Verbose;
                }
                Infer = false;
                LogSource = XFConstants.Config.DefaultLogSource;
                ServiceToken = XFConstants.Config.DefaultServiceToken;

                ApiRoot = XFConstants.Api.DefaultRoot;
                ApiErrors = XFConstants.Api.DefaultErrorsEndpoint;
                ApiEvents = XFConstants.Api.DefaultEventsEndpoint;
                ApiStatii = XFConstants.Api.DefaultStatiiEndpoint;
                ApiMetrics = XFConstants.Api.DefaultMetricsEndpoint;
                ApiAlerts = XFConstants.Api.DefaultAlertsEndpoint;
                ApiTasks = XFConstants.Api.DefaultTasksEndpoint;
                ApiKpi = XFConstants.Api.DefaultKpiEndpoint;
                ApiCustom = XFConstants.Api.DefaultCustomEndpoint;
            }
            finally
            {
                
            }
            _ExtendedProperties.Add(XFConstants.Context.ZONE, Zone);
            _ExtendedProperties.Add(XFConstants.Context.Application, Context);
            _ExtendedProperties.Add(XFConstants.Context.INSTANCEIDENTIFIER, InstanceIdentifier);
        }


        public static bool IsSeverityAtLeast(TraceEventTypeOption option)
        {
            return LoggingSeverity >= option;
        }


        public static Dictionary<string, object> GetProperties<T>(IEnumerable<TypedItem> items, string action, string codeModule, string codeClass, string codeLine)
        {
            var extendedproperties = GetProperties<T>(items, action);
            extendedproperties.Add(XFConstants.Context.MODULE, codeModule);
            extendedproperties.Add(XFConstants.Context.CLASS, codeClass);
            extendedproperties.Add(XFConstants.Context.LINE, codeLine);
            return extendedproperties;
        }

        public static Dictionary<string, object> GetProperties<T>(IEnumerable<TypedItem> items, string action)
        {
            T t = Activator.CreateInstance<T>();
            string model = t.GetType().FullName;
            Dictionary<string, object> extendedproperties = GetProperties(items);
            extendedproperties.Add("Model", model);
            extendedproperties.Add("Action", action);
            return extendedproperties;
        }

        public static Dictionary<string, object> GetProperties(IEnumerable<TypedItem> items)
        {
            Dictionary<string, object> extendedproperties = new Dictionary<string, object>();
            foreach (var item in items)
            {
                extendedproperties.Add(item.Key, item.Value);
            }
            foreach (var item in _ExtendedProperties)
            {
                if (!extendedproperties.ContainsKey(item.Key))
                {
                    extendedproperties.Add(item.Key, item.Value);
                }
            }
            return extendedproperties;
        }

        public static Dictionary<string, object> GetProperties()
        {
            Dictionary<string, object> extendedproperties = new Dictionary<string, object>();
            foreach (var item in _ExtendedProperties)
            {
                extendedproperties.Add(item.Key, item.Value);
            }
            return extendedproperties;
        }

        public static Dictionary<string, object> GetProperties(string codeModule, string codeClass, string codeLine)
        {
            var extendedproperties = GetProperties();
            extendedproperties.Add(XFConstants.Context.MODULE, codeModule);
            extendedproperties.Add(XFConstants.Context.CLASS, codeClass);
            extendedproperties.Add(XFConstants.Context.LINE, codeLine);
            return extendedproperties;
        }
    }
}