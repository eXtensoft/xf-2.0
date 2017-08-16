// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.DataServices
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data;
    using System.Linq;
    using XF.Common;

    [InheritedExport(typeof(ITypeMap))]
    public abstract class MongoDbModelDataGateway<T,U> : IModelDataGateway<T>, IModelDataGatewayInitializeable where T : class, new() where U : class, new()
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

        public MongoCollection<U> Collection { get; set; }

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
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
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
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
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
                context.SetError(500, message.ToPublish());
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
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
                EventWriter.WriteError(message, SeverityType.Error);
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
                context.SetError(500, message.ToPublish() );
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
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
                context.SetError(500, message.ToPublish() );
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
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
                    var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, t, criterion, context, this.GetType().FullName);
                    context.SetError(500, message.ToPublish());
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error);
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
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
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
                    var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, null, criterion, context, this.GetType().FullName);
                    context.SetError(500, message.ToPublish());
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error);
                }

            if (o is U)
            {
                u = (U)o;
            }
            return u;
        }

        #region abstract methods

        protected virtual U ConvertToU(T t)
        {
            return default(U);
        }

        protected virtual T ConvertToT(U model)
        {
            return default(T);
        }

        #endregion

        #region overridable model actions


        protected virtual T Post(T t, IContext context)
        {
            Collection.Insert(t);
            return t;
        }

        protected virtual T Get(ICriterion criterion, IContext context)
        {
            IMongoQuery query = GetQuery(criterion, context);
            return Collection.FindOneAs<T>(query);
        }

        protected virtual IMongoQuery QueryByObjectId(ICriterion criterion)
        {
            IMongoQuery query = new QueryDocument(ModelIdName, new ObjectId(criterion.GetValue<string>(ModelIdName)));
            return query;
        }

        protected virtual IMongoQuery QueryByModelId(ICriterion criterion)
        {
            IMongoQuery query = new QueryDocument(ModelIdName, criterion.GetStringValue(ModelIdName));
            return query;
        }

        protected virtual T Put(T t, ICriterion criterion, IContext context)
        {
            U item = ConvertToU(t);
            Collection.Insert<U>(item);
            return t;
        }

        protected virtual ICriterion Delete(ICriterion criterion, IContext context)
        {
            ICriterion result = new Criterion();
            
            //WriteConcernResult writeConcern = Collection.Remove(Query.EQ("_id", new ObjectId(criterion.GetValue<string>("Id"))));
            WriteConcernResult writeConcern = Collection.Remove(criterion.ToQueryDocument<T>());
            //WriteConcernResult writeConcern = Collection.Remove(Query.EQ("_id", new ObjectId(criterion.GetValue<string>("Id"))));
            if (writeConcern.Ok)
            {
                long affected = writeConcern.DocumentsAffected;
                result.AddItem("documentsAffected", affected);
            }
            else
            {
                IRequestContext ctx = context as IRequestContext;
                var message = Exceptions.ComposeDatastoreException<T>(ModelActionOption.Delete, writeConcern.ErrorMessage, null, criterion, context, this.GetType().FullName, "MongoDB");
                ctx.SetError(500, message.ToPublish());
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
            }
            return result;
        }

        protected virtual IEnumerable<T> GetAll(ICriterion criterion, IContext context)
        {
            List<T> list = new List<T>();
            MongoCursor<U> cursor = null;
            if (criterion != null)
            {
                var query = criterion.ToMongoQuery();
                cursor = Collection.FindAs<U>(query);
            }
            else
            {
                cursor = Collection.FindAllAs<U>();
            }
            if (cursor != null)
            {
                foreach (var item in cursor)
                {
                    list.Add(ConvertToT(item));
                }
            }

            return list;
        }

        protected virtual IEnumerable<Projection> GetAllProjections(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual U ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual DataSet ExecuteCommand(DataSet dataSet, ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual U ExecuteMany<U>(IEnumerable<T> list, ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region overrideables

        protected virtual IMongoQuery PostQuery(T t, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery PutQuery(T t, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery DeleteQuery(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery GetQuery(ICriterion criterion, IContext context)
        {
            IMongoQuery query = UsesMongoObjectId ? QueryByObjectId(criterion) : QueryByModelId(criterion);
            return query;
        }

        protected virtual IMongoQuery GetAllQuery(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery GetProjectionsQuery(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery ExecuteCommandQuery(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
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

        protected MongoCollection<U> GetCollection(string key = "")
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                key = GetCollectionKey();
            }
            return MongoDb.GetCollection<U>(key);
        }



        public virtual Type GetModelType()
        {
            return Activator.CreateInstance<T>().GetType();
        }

        public virtual string ModelIdName
        {
            get { return "Id"; }
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
                context.SetError(500, message.ToPublish() );
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
            }
            else
            {
                var cn = ConfigurationProvider.ConnectionStrings[key];
                if (cn != null)
                {
                    MongoConnectionInfo info = new MongoConnectionInfo();
                    if (info.Initialize(cn))
                    {
                        MongoDb = info.GetDatabase();
                    }

                    if (MongoDb == null)
                    {
                        var message = Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, key);
                        context.SetError(500, message.ToPublish());
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error);
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
                else
                {
                    var message = Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, key);
                    context.SetError(500, message.ToPublish());
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error);
                }

            }
        }


    }
}
