// <copyright company="eXtensible Solutions, LLC" file="MongoDbModelDataGateway`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.DataServices
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using XF.Common;

    [InheritedExport(typeof(ITypeMap))]
    public abstract class MongoDbModelDataGateway<T> : IModelDataGateway<T>, IModelDataGatewayInitializeable where T : class, new()
    {
        private const string DataStorePlatform = "mongodb";

        private static IList<string> queryExclusions = new List<string>
        {
            {"skip"},
            {"take"},
            {"limit"}
        };

        string ITypeMap.Domain
        {
            get { return String.Empty; }
        }

        Type ITypeMap.KeyType
        {
            get { return GetModelType(); }
        }

        Type ITypeMap.TypeResolution
        {
            get { return this.GetType(); }
        }

        private IDatastoreService _DataService;
        IDatastoreService IModelDataGateway<T>.DataService
        {
            get
            {
                return _DataService;
            }
            set
            {
                _DataService = value;
            }
        }

        private IContext _Context = null;

        IContext IModelDataGateway<T>.Context
        {
            get
            {
                return _Context;
            }
            set
            {
                _Context = value;
            }
        }



        public MongoDatabase MongoDb { get; set; }

        public MongoCollection<T> Collection { get; set; }

        protected virtual bool UsesMongoObjectId
        {
            get { return true; }
        }

        protected IQueryable<T> IQueryable
        {
            get { return Collection.AsQueryable<T>(); }
        }


        T IModelDataGateway<T>.Post(T t, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            T result = null;

            try
            {
                result = Post(t, context);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }

            return result;
        }

        T IModelDataGateway<T>.Get(ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            T result = null;
            try
            {
                result = Get(criterion, context);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500,message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }

            return result;
        }

        T IModelDataGateway<T>.Put(T t, ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            T result = null;

            try
            {
                result = Put(t, criterion, context);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName);
                context.SetError(500,message.ToPublish() );
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            return result;
        }

        ICriterion IModelDataGateway<T>.Delete(ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            ICriterion result = null;

            try
            {
                result = Delete(criterion, context);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Put, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish() );
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            return result;
        }

        IEnumerable<T> IModelDataGateway<T>.GetAll(ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            IEnumerable<T> result = null;

            try
            {
                result = GetAll(criterion, context);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            return result;
        }

        IEnumerable<Projection> IModelDataGateway<T>.GetAllProjections(ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            IEnumerable<Projection> result = null;

            try
            {
                result = GetAllProjections(criterion, context);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAllProjections, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            return result;
        }

        U IModelDataGateway<T>.ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            U u = default(U);
            object o = null;

            if (criterion.Contains(XFConstants.Application.ActionExecuteStrategy))
            {
                o = DynamicExecuteAction<U>(t, criterion, context);
            }
            else
            {
                try
                {
                    o = ExecuteAction<U>(t, criterion, context);
                }
                catch (Exception ex)
                {
                    ExceptionMessage message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, t, criterion, context, this.GetType().FullName);
                    context.SetError(500, message.ToPublish());
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                }
            }
            if (o is U)
            {
                u = (U)o;
            }
            return u;
        }


        DataSet IModelDataGateway<T>.ExecuteCommand(DataSet dataSet, ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            DataSet result = null;

            try
            {
                result = ExecuteCommand(dataSet, criterion, context);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteCommand, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            return result;
        }

        U IModelDataGateway<T>.ExecuteMany<U>(IEnumerable<T> list, ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
            U u = default(U);
            object o = null;

            try
            {
                o = ExecuteMany<U>(list, criterion, context);
            }
            catch (Exception ex)
            {
                ExceptionMessage message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }

            if (o is U)
            {
                u = (U)o;
            }
            return u;
        }


        #region overridable model actions


        protected virtual T Post(T t, IContext context)
        {
            if (Collection != null)
            {
                Collection.Insert(t);
            }            
            return t;
        }

        protected virtual T Get(ICriterion criterion, IContext context)
        {
            T t = default(T);
            if(Collection != null)
            {
                IMongoQuery query = GetQuery(criterion, context);
                if (query == null)
                {
                    query = UsesMongoObjectId ? QueryByObjectId(criterion) : QueryByModelId(criterion);
                }            
                t = Collection.FindOneAs<T>(query);
            }
            return t;
        }

        protected virtual IMongoQuery QueryByObjectId(ICriterion criterion)
        {
            IMongoQuery query = new QueryDocument("_id", new ObjectId(criterion.GetValue<string>(ModelIdName)));
            return query;
        }

        protected virtual IMongoQuery QueryByModelId(ICriterion criterion)
        {
            IMongoQuery query = new QueryDocument(ModelIdName, criterion.GetStringValue(ModelIdName));
            return query;
        }

        protected virtual T Put(T t, ICriterion criterion, IContext context)
        {
            if (Collection != null)
            {
                MongoUpdate update = PutQuery(t, criterion, context);
                if (update != null && update.Query != null && update.Update != null)
                {
                    WriteConcernResult result = Collection.Update(update.Query, update.Update);
                    return t != null ? t : Collection.FindOneAs<T>(update.Query);
                }
                else
                {
                    if (t != null )
                    {
                        Collection.Save<T>(t);
                        return t;
                    }
                    else if(criterion != null)
                    {
                        IMongoQuery whereQuery = UsesMongoObjectId ? QueryByObjectId(criterion) : QueryByModelId(criterion);
                        IMongoUpdate patchUpdate = new UpdateDocument();
                        Collection.Update(whereQuery,criterion.ToMongoPatch());
                        return Collection.FindOneAs<T>(whereQuery);
                    }
                } 
            }

         
            return t;
        }

        protected virtual ICriterion Delete(ICriterion criterion, IContext context)
        {
            ICriterion result = new Criterion();
            if (Collection != null)
            {
                WriteConcernResult writeConcern = Collection.Remove(criterion.ToQueryDocument<T>());
                if (writeConcern.Ok)
                {
                    long affected = writeConcern.DocumentsAffected;
                    result.AddItem("documentsAffected", affected);
                }
                else
                {
                    IRequestContext ctx = context as IRequestContext;
                    var message = Exceptions.ComposeDatastoreException<T>(ModelActionOption.Delete, writeConcern.ErrorMessage, null, criterion, context, this.GetType().FullName,"MongoDB");
                    ctx.SetError(500, message.ToPublish());
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                }                
            }

            return result;
        }

        protected virtual IEnumerable<T> GetAll(ICriterion criterion, IContext context)
        {
            if (Collection != null)
            {
                if (criterion != null)
                {
                    var query = criterion.ToMongoQuery();
                    return Collection.FindAs<T>(query).ToList();                
                }
                else
                {
                    return Collection.FindAllAs<T>().ToList();
                }                
            }
            else
            {
                return new List<T>();
            }


        }

        protected virtual IEnumerable<Projection> GetAllProjections(ICriterion criterion, IContext context)
        {
            List<Projection> list = new List<Projection>();
            if (Collection != null)
            {
                if (criterion != null)
                {
                    var query = criterion.ToMongoQuery();
                    var cursor = Collection.FindAs<T>(query).SetFields("_id", "AKA", "ExternalIP");
                    foreach (T item in cursor)
                    {
                   
                    }               
                }                
            }

            return list;
        }

        protected virtual U ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            return default(U);
        }

        protected virtual object ExecuteMany<U>(IEnumerable<T> list, ICriterion criterion, IContext context)
        {
            string message = String.Format("The method or operation is not implemented: {0}.{1} in {2}", GetModelType().FullName, ModelActionOption.ExecuteMany, this.GetType().FullName);
            return new NotImplementedException(message);
        }

        protected virtual DataSet ExecuteCommand(DataSet ds, ICriterion criterion, IContext context)
        {
            return new DataSet() { DataSetName = "EmptySet" };
        }

        #endregion

        #region overrideables

        protected virtual IMongoQuery PostQuery(T t, IContext context)
        {
            return null;
        }

        protected virtual MongoUpdate PutQuery(T t, ICriterion criterion, IContext context)
        {
            return null;
        }

        protected virtual IMongoQuery DeleteQuery(ICriterion criterion, IContext context)
        {
            return null;
        }

        protected virtual IMongoQuery GetQuery(ICriterion criterion, IContext context)
        {
            IMongoQuery query = UsesMongoObjectId ? QueryByObjectId(criterion) : QueryByModelId(criterion);
            return query;
        }

        protected virtual IMongoQuery GetAllQuery(ICriterion criterion, IContext context)
        {
            return null;
        }

        protected virtual IMongoQuery GetAllProjectionsQuery(ICriterion criterion, IContext context)
        {
            return null;
        }

        protected virtual IMongoQuery ExecuteCommandQuery(ICriterion criterion, IContext context)
        {
            return null;
        }

        protected virtual string ResolveConnectionStringKey(IContext context)
        {
            return String.Empty;
        }

        #endregion

        protected virtual string GetCollectionKey()
        {
            string typename = GetModelType().Name.ToLower();
            return typename;
        }

        protected MongoCollection<T> GetCollection(string key = "")
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                key = GetCollectionKey();
            }
            return MongoDb.GetCollection<T>(key);
        }

        

        public virtual Type GetModelType()
        {
            return Activator.CreateInstance<T>().GetType();
        }

        public virtual string ModelIdName
        {
            get { return "Id"; }
        }

        protected static void DropCollections(MongoDatabase db, params string[] collectionNames)
        {
            foreach (var collectionName in collectionNames)
            {
                if (db.CollectionExists(collectionName))
                {
                    db.DropCollection(collectionName);
                }
            }
        }
        

        #region helper methods

        private object DynamicExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        #endregion


        void IModelDataGatewayInitializeable.Initialize<T>(ModelActionOption option, IContext context, T t, ICriterion criterion, ResolveDbKey<T> dbkeyResolver)
        {
            string key = ResolveConnectionStringKey(context);
            if (String.IsNullOrWhiteSpace(key))
            {
                key = dbkeyResolver.Invoke(context);
            }
            if (String.IsNullOrEmpty(key))
            {
                var message = Exceptions.ComposeDbConnectionStringKeyResolutionError<T>(option, t, context);
                context.SetError(500, message.ToPublish());
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            else
            {
                var cn = ConfigurationProvider.ConnectionStrings[key];
                if (cn != null)
                {
                    MongoConnectionInfo info = new MongoConnectionInfo();
                    try
                    {
                        if (info.Initialize(cn))
                        {
                            MongoDb = info.GetDatabase();
                        }

                        if (MongoDb == null)
                        {
                            var message = Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, key);
                            context.SetError(500, message.ToPublish());                            
                            EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                        }
                        else
                        {
                            Collection = GetCollection();
                            if (Collection == null)
                            {
                                context.SetError(500, "MongoDB.Collection is Null");
                            }
                            else if (eXtensibleConfig.CaptureMetrics)
                            {
                                IRequestContext ctx = context as IRequestContext;
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Database.Datasource, String.Format("server:{0};database:{1};", cn.ConnectionString, key)); ;
                            }
                        } 
                    }
                    catch (Exception ex)
                    {
                        var message = Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, key);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));                       
                    }                   
                }
                else
                {
                    var message = Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, key);
                    context.SetError(500, message.ToPublish() );
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                }

            }
        }



    }
}
