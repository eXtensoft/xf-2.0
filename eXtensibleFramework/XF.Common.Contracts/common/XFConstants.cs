// <copyright company="eXtensible Solutions, LLC" file="XFConstants.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;

    public static class XFConstants
    {
        public const string StrategyKey = "F30F2E17";
        public const string SystemNull = "{x:Null}";
        public const string DateTimeFormat = "MM/dd/yyyy hh:mm:ss.fff tt";
        public const string PipeUrl = "net.pipe://localhost/pipe/foobar";

        public static class Application
        {
            public const string UserIdentityParamName = "Username";


            public const string StrategyKey = "xf.strategy.key";
            public const string SystemNull = "{x:Null}";
            public const string DefaultAppplicationKey = "demo";
            public const string ApplicationUserIdentityParamName = "UserIdentity";
            public const string ActionResultModelType = "ActionResult.ModelType";
            public const string ActionResult = "Action.Result";
            public const string ActionExecuteStrategy = "Action.ExecuteStrategy";
            public const string ActionExecuteStrategyDynamic = "ExecuteStrategy.Dynamic";
            public const string ActionExecuteStrategyNormal = "ExecuteStrategy.Normal";
            public const string ActionExecuteMethodName = "Action.MethodName";
            public const string ModelService = "model.service";

            public const string SelectedStrategy = "selected.strategy";
            public const string PermissionsGroups = "permission.groups";
            public const string TargetActions = "target.actions";

            public static class Defaults
            {
                public const string SqlServerSchema = "dbo";
                public const string StrategyKey = "E8BBB9D6-F5A7-4F91-8BD0-4461309FA1AB";
            }

            public static class Config
            {
                public const string ApplicationKey = "eXtensoft.app.context.key";
                public const string ZoneKey = "eXtensoft.app.zone.key";
                public const string LoggingStrategyKey = "eXtensoft.app.logging.strategy";
                public const string LoggingSeverityKey = "eXtensoft.app.logging.severity";
                public const string LogSourceKey = "eXtensoft.app.logging.source";
                public const string ConnectionStringKey = "eXtensoft.app.connectionstring.key";
                public const string BigDataUrlKey = "eXtensoft.app.bigdataurl.key";
                public const string ServiceTokenKey = "eXtensoft.app.servicetoken.key";
                public const string InstanceIdentifierKey = "eXtensoft.app.instance.key";
                public const string InferKey = "eXtensoft.app.infer.key";
            }
        }
        public static class Category
        {
            public const string General = "General";
            public const string Debug = "Debug";
            public const string XF = "eXtensibleFramework";
            public const string DataAccess = "DataAccess";
        }

        public static class Config
        {
            
            public const string LoggingStrategy = "app.logging.strategy";
            public const string CommonServices = "common.services";
            public const string XFTool = "xftool";
            public const string EventViewer = "eventlog";
            public const string AppUserIdentityParamName = "UserIdentity";
            public const string DEFAULTKEY = "none";
            public const string DefaultZone = "development";
            public const string DefaultApplicationKey = "demo";
            public const string DefaultLoggingStrategy = "windowseventlog";
            public const string DefaultPublishingSeverity = "Verbose";
            public const string DATAACCESSCONTEXTFOLDERPATH = "Data";
            public const string FRAMEWORKSTRATEGYGROUPNAME = "xf.Plugin.Strategy";
            public const string MODELS = "Models";
            public const string ModelDataGateways = "MDG";
            public const string SectionName = "eXtensoft.application";
            public const string DefaultLogSource = "eXtensoft.Log";
            public const string DefaultServiceToken = "80FF8B69-54DB-46B0-976D-96CB16C35B9F";
            //public const string STRATEGYSECTIONNAME = "";
        }

        public static class Context
        {
            public const string Application = "app.context.key";
            public const string DefaultApplication = "demo";
            public const string Error = "app.context.error";
            public const string Claim = "user.claim";
            public const string LOGGINGCATEGORY = "app.logging.category";
            public const string ZONE = "app.context.zone";
            public const string TASK = "app.context.task";
            public const string Model = "app.context.model";
            public const string Verb = "app.context.verb";
            public const string LAYER = "app.context.layer";
            public const string TIER = "app.context.tier";
            public const string MODULE = "app.context.module";
            public const string CLASS = "app.context.class";
            public const string LINE = "app.context.line";
            public const string USERIDENTITY = "app.context.user";
            public const string USERCULTURE = "app.context.culture";
            public const string UICULTURE = "app.context.uxculture";
            public const string INSTANCEIDENTIFIER = "app.context.instance";
            public const string EXECUTIONID = "app.context.executionid";
            public const string COMPONENTID = "app.context.componentid";
            public const string DATASTORE = "app.context.datastore";
            public const string ACTIVITYID = "app.context.activityid";
            public const string LOGGINGSEVERITY = "app.logging.severity";
            public const string TITLE = "XF.SOA";
            public const string SEQUENCEID = "sequenceid";
            public const string RequestBegin = "request.begin";
            public const string RequestEnd = "request.end";
            public const string PrimaryScope = "scope.1";
            public const string SecondaryScope = "scope.2";
            public const string ServiceToken = "app.service.token";
            public const string Ticket = "app.context.ticket";

        }

        public static class Domain
        {
            public const string Context = "domain.context";
            public const string Claims = "domain.claims";
            public const string Metrics = "domain.metrix";
        }

        public static class EventWriter
        {            
            public const string ModelId = "event.modelId";
            public const string Model = "event.modelToString";
            public const string ModelT = "event.model.serialize";
            public const string ModelType = "event.modelType";
            public const string ModelStatus = "event.modelStatus";
            public const string Effective = "event.effective";
            public const string Category = "event.category";
            public const string EventType = "event.type";
            public const string ModelAction = "event.modelAction";
            public const string Message = "event.message";
            public const string ErrorSeverity = "event.severity";
            public const string StackTrace = "event.stacktrace";
            public const string MessageId = "xf-id";


        }

        public static class Alert
        {
            public const string Title = "event.alert.title";
            public const string Message = "event.alert.message";
            public const string Urgency = "event.alert.urgency";
            public const string Importance = "event.alert.importance";
            public const string Targets = "event.alert.targets";
            public const string Categories = "event.alert.categories";
            public const string Error = "event.alert.error";
            public const string StackTrace = "event.alert.stacktrace";
            public const string NamedTarget = "event.alert.namedtarget";
            public const string Topic = "event.alert.topic";
            public const string Source = "event.alert.source";
            public const string CreatedAt = "event.alert.createdat";
        }
        
        public static class TaskWriter
        {
            public const string TaskType = "event.task.taskType";
            public const string TaskName = "event.task.taskName";
            public const string TaskId = "event.task.taskId";
            public const string TaskMasterId = "event.task.masterId";
            public const string TaskItemName = "event.task.itemName";
            public const string TaskItemId = "event.task.itemId";

        }

        public static class Status
        {
            public const string ModelId = "event.status.modelId";
            public const string ModelName = "event.status.modelName";
            public const string ModelType = "event.status.modelType";
            public const string Description = "event.status.desc";
            public const string StatusText = "event.status.text";
            public const string Source = "event.status.source";
            public const string ProcessId = "event.status.processId";
            public const string Effective = "event.status.effectiveAt";
        }

        public static class Metrics
        {
            public const string Model = "metrics.model";
            public const string StatusCode = "metrics.status.code";
            public const string StatusDesc = "metrics.status.desc";
            public const string DbType = "metrics.database.type";
            public const string T = "metrics.model.instance";
            public const string Criteria = "metrics.criterion.string";
            public const string Count = "metrics.resultset.count";

            public static class Scope
            {
                public const string DataService = "layer.dataservice";
                public const string ModelService = "layer.modelservice";
                public const string DataStore = "layer.datastore";
                public const string Caching = "layer.caching";
                public const string WebApi = "layer.webapi";

                public static class ModelRequestService
                {
                    public const string Begin = "begin.mrs";
                    public const string End = "end.mrs";
                }
                public static class DataRequestService
                {
                    public const string Begin = "begin.drs";
                    public const string End = "end.drs";
                }

                public static class ModelDataGateway
                {
                    public const string Begin = "begin.mdg";
                    public const string End = "end.mdg";
                }
                //public static class SqlCommand
                //{
                //    public const string Begin = "command.begin";
                //    public const string End = "command.end";
                //}
                public static class Command
                {
                    public const string Begin = "command.begin";
                    public const string End = "command.end";
                    public const string Text = "command.text";
                }
                public static class Datastore
                {
                    public const string Begin = "begin.datastore.command";
                    public const string End = "end.datastore.command";
                }
                public static class Api
                {
                    public const string Begin = "begin.api";
                    public const string End = "end.api";
                }

            }
            
            
            public static class Database
            {
                public const string Command = "command.text";
                public const string Begin = "command.begin";
                public const string End = "command.end";
                public const string Datasource = "data.source";
            }

        }
        
        public static class Message
        {
            public const string Verb = "request.verb";
            public const string Context = "request.context";
            public const string RequestStatus = "request.status";

            public static IDictionary<string, ModelActionOption> ConstVerbList = new Dictionary<string, ModelActionOption>
            {
                {Verbs.None,ModelActionOption.None},
                {Verbs.Post,ModelActionOption.Post},
                {Verbs.Put,ModelActionOption.Put},
                {Verbs.Delete,ModelActionOption.Delete},
                {Verbs.Get,ModelActionOption.Get},
                {Verbs.GetAll,ModelActionOption.GetAll},
                {Verbs.GetAllProjections,ModelActionOption.GetAllProjections},
                {Verbs.ExecuteAction,ModelActionOption.ExecuteAction},
                {Verbs.ExecuteCommand,ModelActionOption.ExecuteCommand},
                {Verbs.ExecuteMany,ModelActionOption.ExecuteMany},
            };
            public static IDictionary<ModelActionOption,string> VerbConstList = new Dictionary<ModelActionOption,string>
            {
                {ModelActionOption.None,Verbs.None},
                {ModelActionOption.Post,Verbs.Post},
                {ModelActionOption.Put,Verbs.Put},
                {ModelActionOption.Delete,Verbs.Delete},
                {ModelActionOption.Get,Verbs.Get},
                {ModelActionOption.GetAll,Verbs.GetAll},
                {ModelActionOption.GetAllProjections,Verbs.GetAllProjections},
                {ModelActionOption.ExecuteAction,Verbs.ExecuteAction},
                {ModelActionOption.ExecuteCommand,Verbs.ExecuteCommand},
                {ModelActionOption.ExecuteMany,Verbs.ExecuteMany},
            };

            public static class Verbs
            {
                public const string None = "action.none";
                public const string Post = "action.post";
                public const string Put = "action.put";
                public const string Delete = "action.delete";
                public const string Get = "action.get";
                public const string GetAll = "action.getall";
                public const string GetAllProjections = "action.getalldisplay";
                public const string ExecuteAction = "action.executeaction";
                public const string ExecuteCommand = "action.executecommand";
                public const string ExecuteMany = "action.executemany";
            }
        }

        public static class ZONE
        {
            public const string Local = "local";
            public const string Development = "development";
            public const string QualityAssurance = "qa";
            public const string UserAcceptanceTesting = "uat";
            public const string Staging = "staging";
            public const string Production = "production";
        }
   
        public static class Api
        {
            public const string DefaultRoot = "log";
            public const string DefaultErrorsEndpoint = "errors";
            public const string DefaultEventsEndpoint = "events";
            public const string DefaultStatiiEndpoint = "statii";
            public const string DefaultMetricsEndpoint = "metrics";
            public const string DefaultAlertsEndpoint = "alerts";
            public const string DefaultTasksEndpoint = "tasks";
            public const string DefaultKpiEndpoint = "kpi";
            public const string DefaultCustomEndpoint = "custom";
            public const string RequestPath = "api.request.path";
            public const string RequestMethod = "api.request.method";
            public const string RequestStatusCode = "api.request.status";
            public const string RequestModelMethod = "api.model.method";
            public const string CustomLogKey = "api.extensible.list";
            public const string eXtensibleApiRequestKey = "xf.api.request";
        }
    
    }
}
