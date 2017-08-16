// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using XF.Common;

    public class ModelDataGatewayDataService : IDatastoreService
    {
        #region local fields

        private static ITypeMapCache _TypeCache;

        private static XF.Common.Db.DbConfigCollection _DbConfigs = null;

        #endregion

        #region properties

        #region DbKey Resolver

        private IConfigStrategyResolver _DatabaseKeyResolver = null;

        public IConfigStrategyResolver DatabaseKeyResolver
        {
            get
            {
                if (_DatabaseKeyResolver == null)
                {
                    _DatabaseKeyResolver = ConfigStrategyResolverLoader.Load(ConfigConstants.DatabaseSectionName);
                }
                return _DatabaseKeyResolver;
            }
        }

        #endregion DbKey Resolver

        #region ICachingService

        private ICache _Cache = null;

        public ICache Cache
        {
            get
            {
                if (_Cache == null)
                {
                    _Cache = new NetMemoryCache();
                }
                return _Cache;
            }
            set
            {
                _Cache = value;
            }
        }


        #endregion



        #endregion

        #region constructors

        public ModelDataGatewayDataService() { }

        static ModelDataGatewayDataService()
        {
            Initialize();
        }

        private static void Initialize()
        {
            InitializeTypeCache();
            InitializeDbConfig();
        }

        private static void InitializeDbConfig()
        {
            _DbConfigs = new Common.Db.DbConfigCollection();
            try
            {
                if (System.IO.Directory.Exists(eXtensibleConfig.DbConfigs))
                {
                    var candidates = System.IO.Directory.GetFiles(eXtensibleConfig.DbConfigs,"*.dbconfig.xml");
                    if (candidates != null && candidates.Length > 0)
                    {
                        foreach (var candidate in candidates)
                        {
                            try
                            {
                                var config = GenericSerializer.ReadGenericItem<XF.Common.Db.DbConfig>(candidate);
                                _DbConfigs.Add(config);
                            }
                            catch (Exception ex)
                            {
                                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                var props = eXtensibleConfig.GetProperties();
                                EventWriter.WriteError(message, SeverityType.Critical, "DataAccess", props);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

        }
        private static void InitializeTypeCache()
        {
            string configfilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            string configFolder = Path.GetDirectoryName(configfilepath);

            string mdg = configFolder + "\\" + "mdg";
            string mdgbin = configFolder + "\\" + "bin" + "\\" + "mdg";
            string bin = configFolder + "\\" + "bin";

            List<string> folderpaths = new List<string>();
            if (Directory.Exists(mdg))
            {
                folderpaths.Add(mdg);
            }
            if (Directory.Exists(mdgbin))
            {
                folderpaths.Add(mdgbin);
            }
            if (Directory.Exists(configFolder))
            {
                folderpaths.Add(configFolder);
            }
            if (Directory.Exists(bin))
            {
                folderpaths.Add(bin);
            }
            
            _TypeCache = new TypeMapCache(folderpaths);

            _TypeCache.Initialize();
        }

        #endregion

        #region interface implementations

        T IDatastoreService.Post<T>(T model, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            T item = default(T);
            try
            {
                item = Create<T>(model, requestContext);
            }
            catch (Exception ex)
            {                               
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Post, ex, model, null, requestContext);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                requestContext.SetError(500, message.ToPublish() );
                EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);
            }
            if (item == null && requestContext.HasError())
            {
                requestContext.SetError(500, "Internal Server Error");
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.T, model.ToString()) { Domain = XFConstants.Domain.Metrics });
            }
            return item;
        }

        T IDatastoreService.Put<T>(T model, ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            T item = null;
            try
            {
                item = Update<T>(model, criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Put, ex, model, null, requestContext);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                requestContext.SetError(500, message.ToPublish() );
                requestContext.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                string data = (model != null) ? model.ToString() : (criterion != null) ? criterion.ToShortString() : "{x:Null}";
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.T, data) { Domain = XFConstants.Domain.Metrics });
            }
            return item;
        }

        ICriterion IDatastoreService.Delete<T>(ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            ICriterion item = null;
            try
            {
                item = Delete<T>(criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Delete, ex, null, criterion, requestContext);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                requestContext.SetError(500, message.ToPublish() );
                requestContext.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);
            }
            if (item == null && !requestContext.HasError())
            {
                item = new Criterion();
                requestContext.SetError(500, "Internal Server Error");
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
            }
            return item;
        }

        T IDatastoreService.Get<T>(ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            T item = null;
            try
            {
                item = Get<T>(criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Get, ex, null, criterion, requestContext);
                requestContext.SetError(500, message.ToPublish() );
                requestContext.SetStacktrace(ex.StackTrace);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);
            }
            if (item == null && !requestContext.HasError())
            {
                var message = Exceptions.ComposeResourceNotFoundError<T>(ModelActionOption.Get, null, criterion, requestContext);
                requestContext.SetError(404, message.ToPublish());
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
            }
            return item;
        }

        IEnumerable<T> IDatastoreService.GetAll<T>(ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IEnumerable<T> list = null;
            try
            {
                list = GetAll<T>(criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAll , ex, null, criterion, requestContext);
                requestContext.SetError(500,message.ToPublish());
                requestContext.SetStacktrace(ex.StackTrace);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);
            }
            //if (list != null && list.Count() == 0 && !requestContext.HasError())
            //{
            //    var message = Exceptions.ComposeResourceNotFoundError<T>(ModelActionOption.GetAll, null, criterion, requestContext);
            //    requestContext.SetError(404, message.ToPublish());                
            //}
            if (list == null && !requestContext.HasError())
            {
                requestContext.SetError(500, "Internal Server Error");
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
            }
            return list;
        }

        IEnumerable<IProjection> IDatastoreService.GetAllProjections<T>(ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IEnumerable<IProjection> list = null;
            try
            {
                list = GetAllDisplay<T>(criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAllProjections, ex, null, criterion, requestContext);
                requestContext.SetError(500, message.ToPublish() );
                requestContext.SetStacktrace(ex.StackTrace);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);
                //Logger.Log
            }
            if (list != null && list.Count() == 0 && !requestContext.HasError())
            {
                var message = Exceptions.ComposeResourceNotFoundError<T>(ModelActionOption.GetAllProjections, null, criterion, requestContext);
                requestContext.SetError(404, message.ToPublish());
            }
            else if(list == null && !requestContext.HasError())
            {
                list = new List<Projection>();
                requestContext.SetError(500, "Internal Server Error");
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
            }
            return list;
        }

        U IDatastoreService.ExecuteAction<T, U>(T model, ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            U u = default(U);
            try
            {
                u = ExecuteAction<T, U>(model, criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, model, criterion, requestContext);
                requestContext.SetError(500, message.ToPublish() );
                requestContext.SetStacktrace(ex.StackTrace);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);              
            }
            if (u == null)
            {
                u = default(U);
                requestContext.SetError(500, "Internal Server Error");
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
            }
            return u;
        }

        DataSet IDatastoreService.ExecuteCommand<T>(DataSet dataSet, ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            DataSet ds = new DataSet();
            try
            {
                ds = ExecuteCommand<T>(dataSet, criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteCommand, ex, null, criterion, requestContext);
                requestContext.SetError(500, message.ToPublish());
                requestContext.SetStacktrace(ex.StackTrace);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);
                //Logger.Log
            }
            if (ds != null && ds.Tables.Count == 0 && !requestContext.HasError())
            {
                var message = Exceptions.ComposeResourceNotFoundError<T>(ModelActionOption.ExecuteCommand, null, criterion, requestContext);
                requestContext.SetError(404, message.ToPublish());
            }
            else if (ds == null && !requestContext.HasError())
            {
                ds = new DataSet() { };
                requestContext.SetError(500, "Internal Server Error");
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
            }
            if (String.IsNullOrEmpty(ds.DataSetName))
            {
                ds.DataSetName = "DataSet";
            }
            return ds;
        }



        U IDatastoreService.ExecuteMany<T, U>(IEnumerable<T> list, ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            U u = default(U);
            try
            {
                u = ExecuteMany<T, U>(list, criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, null, criterion, requestContext);
                requestContext.SetError(500, message.ToPublish());
                requestContext.SetStacktrace(ex.StackTrace);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, props);
            }
            if (u == null)
            {
                u = default(U);
                requestContext.SetError(500, "Internal Server Error");
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
            }
            return u;
        }


        //U IDatastoreService.Execute<T, U>(T model, ICriterion criterion, IRequestContext requestContext)
        //{
        //    if (eXtensibleConfig.CaptureMetrics)
        //    {
        //        ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
        //    }
        //    U u = default(U);
        //    try
        //    {
        //        u = Execute<T, U>(model, criterion, requestContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, model, criterion, requestContext);
        //        requestContext.SetError(500, message.ToPublish());
        //        requestContext.SetStacktrace(ex.StackTrace);
        //        var props = eXtensibleConfig.GetProperties();
        //        props.Add(XFConstants.Context.Ticket, message.Id);
        //        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, props);
        //    }
        //    if (u == null)
        //    {
        //        u = default(U);
        //        requestContext.SetError(500, "Internal Server Error");
        //    }
        //    if (eXtensibleConfig.CaptureMetrics)
        //    {
        //        ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
        //        ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
        //    }
        //    return u;
        //}
        
        #endregion


        #region local implementations

        private T Create<T>(T model, IRequestContext requestContext) where T : class, new()
        {
            T result = default(T);
            var implementor = ResolveImplementor<T>(ModelActionOption.Post, requestContext, model,null);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Post(model, context);
            }
            return result;
        }

        private T Update<T>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            T result = default(T);
            var implementor = ResolveImplementor<T>(ModelActionOption.Put, requestContext, model,null);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Put(model, criterion, context);
            }
            return result;            
        }

        private ICriterion Delete<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            ICriterion result = null;
            var implementor = ResolveImplementor<T>(ModelActionOption.Delete, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Delete(criterion, context);
            }
            return result;
        }

        private T Get<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            T result = default(T);
            var implementor = ResolveImplementor<T>(ModelActionOption.Get, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Get(criterion, context);
            }
            return result;
        }

        private IEnumerable<T> GetAll<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            IEnumerable<T> result = null;
            var implementor = ResolveImplementor<T>(ModelActionOption.GetAll, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.GetAll(criterion, context);
            }
            return result;
        }

        private IEnumerable<IProjection> GetAllDisplay<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            IEnumerable<IProjection> result = null;
            var implementor = ResolveImplementor<T>(ModelActionOption.GetAllProjections, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.GetAllProjections(criterion, context);
            }
            return result;
        }

        private U ExecuteAction<T, U>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            U result = default(U);
            var implementor = ResolveImplementor<T>(ModelActionOption.ExecuteAction, requestContext,null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.ExecuteAction<U>(model, criterion, context);
            }
            return result;
        }

        private DataSet ExecuteCommand<T>(DataSet ds, ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            DataSet result = null;
            var implementor = ResolveImplementor<T>(ModelActionOption.ExecuteCommand, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.ExecuteCommand(ds,criterion, context);
            }
            return result;
        }

        private U ExecuteMany<T, U>(IEnumerable<T> list, ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            U result = default(U);
            var implementor = ResolveImplementor<T>(ModelActionOption.ExecuteMany, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.ExecuteMany<U>(list, criterion, context);
            }
            return result;
        }


        //private U Execute<T, U>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new()
        //{
        //    U result = default(U);
        //    var implementor = ResolveImplementor<T>(ModelActionOption.Execute, requestContext, null, criterion);
        //    if (implementor != null)
        //    {
        //        IContext context = requestContext as IContext;
        //        result = implementor.Execute<U>(model, criterion, context);
        //    }
        //    return result;
        //}



        #endregion

        //    string s = context.GetValue<string>(XFConstants.Context.Application);

        private string ResolveDbKey<T>(IContext context) where T : class, new()
        {
            string key = DatabaseKeyResolver.Resolve<T>(context);
            if (String.IsNullOrWhiteSpace(key) && !String.IsNullOrWhiteSpace(eXtensibleConfig.ConnectionStringKey))
            {
                key = eXtensibleConfig.ConnectionStringKey;
            }
            if (eXtensibleConfig.Inform)
            {
                EventWriter.Inform(String.Format("database key resolved to {0}", key));
            }
            return key;
        }

        private IModelDataGateway<T> ResolveImplementor<T>(ModelActionOption option, IContext context,T t, ICriterion criterion) where T : class, new()
        {
            IModelDataGateway<T> implementor = null;
            
            Type type = _TypeCache.ResolveType<T>();
            if (type == null)
            {
                // if not, create one and add the model.  then add sproc based if exists, otherwise add inline on appcontext-model by appcontext-model basis;
                if (_DbConfigs.Contains(context.ApplicationContextKey))
                {
                    var modelType = typeof(T);
                    string modelKey = modelType.FullName;
                    var found = _DbConfigs[context.ApplicationContextKey].Models.Find(x => x.Key.Equals(modelKey));
                    if (found == null)
                    {
                        XF.Common.Db.Model model = new Common.Db.Model() { Key = modelKey, modelType = modelType.AssemblyQualifiedName, ModelActions = new List<Common.Db.ModelAction>(), Commands = new List<Common.Db.DbCommand>(), DataMaps = new List<Common.Db.DataMap>() };
                        _DbConfigs[context.ApplicationContextKey].Models.Add(model);
                        found = _DbConfigs[context.ApplicationContextKey].Models.Find(x => x.Key.Equals(modelKey));
                    }
                    if (found != null)
                    {
                        implementor = new ConfigModelDataGateway<T>(found);
                    }
                    else
                    {

                    }
                }
                else if(eXtensibleConfig.Infer)
                {
                    implementor = new GenericModelDataGateway<T>();
                    
                }

                //if (implementor == null)
                //{
                //    var message = Exceptions.ComposeImplementorResolutionError<T>(option, t, context);
                //    context.SetError(500, message.ToPublish());
                //    EventWriter.WriteError(message.ToLog(), SeverityType.Error);
                //}

            }
            else
            {
                implementor = Activator.CreateInstance(type) as IModelDataGateway<T>;
            }
            if (implementor == null)
            {
                if (type != null)
                {
                    var message = Exceptions.ComposeImplementorInstantiationError<T>(option, t, context, type.FullName);
                    context.SetError(500, message.ToPublish());
                    var props = eXtensibleConfig.GetProperties();
                    props.Add(XFConstants.Context.Ticket, message.Id);
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,props);                    
                }
                else
                {
                    var message = Exceptions.ComposeImplementorResolutionError<T>(option, t, context);
                    context.SetError(500, message.ToPublish());
                    var props = eXtensibleConfig.GetProperties();
                    props.Add(XFConstants.Context.Ticket, message.Id);
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, props);                   
                }


            }
            else 
            {
                if (eXtensibleConfig.Inform)
                {

                    EventWriter.Inform(String.Format("MDG selected: {0}", implementor.GetType().FullName));
                }
                    
                implementor.DataService = this as IDatastoreService;
                implementor.Context = context;

                ICacheable<T> cacheable = implementor as ICacheable<T>;
                if (cacheable != null)
                {
                    cacheable.Cache = Cache;
                }
                // TODO, outsource this
                IModelDataGatewayInitializeable initializable = implementor as IModelDataGatewayInitializeable;
                if (initializable != null)
                {
                    initializable.Initialize<T>(option,context,t,criterion,ResolveDbKey<T>);
                }

            }

            return implementor;
        }




    }
}
