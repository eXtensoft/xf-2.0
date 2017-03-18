// <copyright company="eXtensible Solutions, LLC" file="SqlServerModelDataGateway`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;
    using XF.Common;
    using XF.Common.BulkCopy;

    [InheritedExport(typeof(ITypeMap))]
    public abstract class SqlServerModelDataGateway<T> : IModelDataGateway<T>, ISqlCommandContext<T>, IModelDataGatewayInitializeable
        where T : class, new()
    {
        private const string DataStorePlatform = "sqlserver";

        private ICache _Cache;
        protected ICache Cache
        {
            get
            {
                if (_Cache == null)
                {
                    _Cache = CacheStrategyLoader.Load();
                }
                return _Cache;
            }
        }

        private const string Module = "XF.Common.Server";
        private const string Class = "SqlServerModelDataGateway`1";

        private IDatastoreService _DataService = null;
        public IDatastoreService DataService
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

        public IContext Context
        {
            get
            {
                return _Context;
            }
            set
            {
                _Context = value; ;
            }
        }

        string ITypeMap.Domain
        {
            get { return String.Empty; }
        }

        Type ITypeMap.KeyType
        {
            get { return GetModelType(); }
        }

        SqlConnection _DbConnection = null;
        public SqlConnection DbConnection
        {
            get
            {
                return _DbConnection;
            }
            set
            {
                _DbConnection = value;
            }
        }


        string ISqlCommandContext<T>.Schema
        {
            get { return GetDatabaseSchema(); }
        }

        Type ITypeMap.TypeResolution
        {
            get { return this.GetType(); }
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
                context.SetError(500, message.ToPublish());
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
                context.SetError(500, message.ToPublish());
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
                context.SetError(500, message.ToPublish());
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
                    var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, t, criterion, context, this.GetType().FullName);
                    context.SetError(500, message.ToPublish());
                    context.SetStacktrace(ex.StackTrace);
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                }
            }
            if (o is U)
            {
                u = (U)o;
            }
            else if(o is NotImplementedException)
            {
                NotImplementedException ex = o as NotImplementedException;
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex,t,criterion,context,this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            return u;
        }



        DataSet IModelDataGateway<T>.ExecuteCommand(DataSet ds, ICriterion criterion, IContext context)
        {
            return ExecuteCommand(ds, criterion, context);
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
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteMany, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            
            if (o is U)
            {
                u = (U)o;
            }
            else if (o is NotImplementedException)
            {
                NotImplementedException ex = o as NotImplementedException;
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteMany, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }

            return u;
        }



        SqlCommand ISqlCommandContext<T>.PostSqlCommand(SqlConnection cn, T t, IContext context)
        {
            return PostSqlCommand(cn, t, context);
        }

        SqlCommand ISqlCommandContext<T>.GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return GetSqlCommand(cn, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.PutSqlCommand(SqlConnection cn, T t, ICriterion criterion, IContext context)
        {
            return PutSqlCommand(cn, t, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return DeleteSqlCommand(cn, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return GetAllSqlCommand(cn, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return GetAllProjectionsSqlCommand(cn, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.ExecuteCommandSqlCommand(SqlConnection cn, DataSet dataSet, ICriterion criterion, IContext context)
        {
            return ExecuteCommandSqlCommand(cn, dataSet, criterion, context);
        }


        #region overrideable methods

        public virtual T Post(T t, IContext context)
        {
            T item = default(T);
            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                List<T> list = new List<T>();
                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
                SqlCommand cmd = null;
                
                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = resolver.PostSqlCommand(cn, t, context);
                        if (cmd == null)
                        {
                            cmd = SqlResolver.ResolvePostCommand<T>(context, cn, resolver.Schema, t, GetDataMaps());
                        }
                        if (cmd != null)
                        {
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.Database.Command, cmd.CommandText);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
                            }
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    BorrowReader(reader, list);
                                }
                                catch (Exception ex)
                                {
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName);
                                    context.SetError(500, message.ToPublish());
                                    context.SetStacktrace(ex.StackTrace);
                                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                                }                            
                            }
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Count, list.Count);
                            }
                        }
                        else
                        {
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Post, t, null, context, this.GetType().FullName);
                            context.SetError(500, message.ToPublish());
                            EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (eXtensibleConfig.CaptureMetrics)
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                        }
                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database) ;
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName, database);
                        context.SetError(500, message.ToPublish() );
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }
                    if (list.Count >= 1)
                    {
                        item = list[0];
                    }
                }   
            }
            

            return item;
        }

        public virtual T Get(ICriterion criterion, IContext context)
        {
            
            T item = default(T);

            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                List<T> list = new List<T>();
                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = resolver.GetSqlCommand(cn, criterion, context);
                        if (cmd == null)
                        {
                            cmd = SqlResolver.ResolveGetCommand<T>(context, cn, resolver.Schema, criterion);
                        }
                        if (cmd != null)
                        {
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.Database.Command, cmd.CommandText);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
                            }
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    BorrowReader(reader, list);
                                }
                                catch (Exception ex)
                                {
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName);
                                    context.SetError(500,message.ToPublish());
                                    context.SetStacktrace(ex.StackTrace);
                                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                                }                           
                            }
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Count, list.Count);
                            }
                        }
                        else
                        {
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Get, null, criterion, context,this.GetType().FullName);
                            context.SetError(500, message.ToPublish());
                            EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (eXtensibleConfig.CaptureMetrics)
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                        }
                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName, database);
                        context.SetError(500, message.ToPublish() );
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }
                }
                if (list.Count > 0)
                {
                    item = list[0];
                }                
            }

            return item;
        }

        public virtual T Put(T t, ICriterion criterion, IContext context)
        {
            
            T item = default(T);

            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                List<T> list = new List<T>();
                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = resolver.PutSqlCommand(cn, t, criterion, context);
                        if (cmd == null)
                        {
                            //cmd = SqlResolver.ResolvePutCommand<T>(cn, resolver.Schema, t);
                            cmd = SqlResolver.ResolvePutCommand<T>(context, cn, resolver.Schema, t);
                        }
                        if (cmd != null)
                        {
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.Database.Command, cmd.CommandText);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
                            }
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    BorrowReader(reader, list);
                                }
                                catch (Exception ex)
                                {
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Put, ex, t, null, context,this.GetType().FullName);
                                    context.SetError(500, message.ToPublish());
                                    context.SetStacktrace(ex.StackTrace);
                                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                                }
                            
                            }
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Count, list.Count);
                            }
                        }
                        else
                        {
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Put, t, null, context,this.GetType().FullName);
                            context.SetError(500, message.ToPublish());
                            EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (eXtensibleConfig.CaptureMetrics)
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                        }
                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                        string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName, database);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }
                }
                if (list.Count >= 1)
                {
                    item = list[0];
                }                
            }

            return item;
        }

        public virtual IEnumerable<T> GetAll(ICriterion criterion, IContext context)
        {
            
            List<T> list = new List<T>();

            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = resolver.GetAllSqlCommand(cn, criterion, context);
                        if (cmd == null)
                        {
                            cmd = SqlResolver.ResolveGetAllCommand<T>( context, cn, resolver.Schema, criterion);
                        }
                        if (cmd != null)
                        {
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.Database.Command, cmd.CommandText);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
                            }
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    BorrowReader(reader, list);
                                }
                                catch (Exception ex)
                                {
                                    string s = cn.ConnectionString;
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.GetAll, ex, null, criterion, context,this.GetType().FullName);
                                    context.SetError(500, message.ToPublish());
                                    context.SetStacktrace(ex.StackTrace);
                                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                                }
                            
                            }
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Count, list.Count);
                            }
                        }
                        else
                        {
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAll, null, criterion, context, this.GetType().FullName);
                            context.SetError(500, message.ToPublish());
                            EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,context.ToDictionary(message.Id));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (eXtensibleConfig.CaptureMetrics)
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                        }
                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.GetAll, ex, null, criterion, context,this.GetType().FullName,database);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error,XFConstants.Category.DataAccess,context.ToDictionary(message.Id));
                    }
                }                
            }


            return list;
        }

        public virtual ICriterion Delete(ICriterion criterion, IContext context)
        {
            
            Criterion item = new Criterion();

            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
                SqlCommand cmd = null;
                int i = 0;
                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = resolver.DeleteSqlCommand(cn, criterion, context);
                        if (cmd == null)
                        {
                            cmd = SqlResolver.ResolveDeleteCommand<T>(context, cn, resolver.Schema, criterion);
                        }
                        if (cmd != null)
                        {
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.Database.Command, cmd.CommandText);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
                            }
                            i = cmd.ExecuteNonQuery();
                            bool b = (i == 1) ? true : false;
                            item.AddItem("Success", b);
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Count, i);
                            }
                        }
                        else
                        {
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Delete, null, criterion, context,this.GetType().FullName);
                            context.SetError(500, message.ToPublish());
                            EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (eXtensibleConfig.CaptureMetrics)
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                        }
                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.Delete, ex, null, criterion, context, this.GetType().FullName, database);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }
                }                
            }

            return item;
        }

        public virtual IEnumerable<Projection> GetAllProjections(ICriterion criterion, IContext context)
        {
            
            List<Projection> list = new List<Projection>();

            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = resolver.GetAllProjectionsSqlCommand(cn, criterion, context);
                        if (cmd == null)
                        {
                            cmd = SqlResolver.ResolveGetAllProjectionsCommand<T>(context, cn, resolver.Schema, criterion);
                        }
                        if (cmd != null)
                        {
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(cmd.CommandType.ToString(),XFConstants.Metrics.Database.Command, cmd.CommandText);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
                            }                                               
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    BorrowReaderProjections(reader, list);
                                }
                                catch (Exception ex)
                                {
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.GetAllProjections, ex, null, criterion, context, this.GetType().FullName);
                                    context.SetError(500, message.ToPublish());
                                    context.SetStacktrace(ex.StackTrace);
                                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                                }                           
                            }
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Count, list.Count);
                            }
                        }
                        else
                        {
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAllProjections, null, criterion, context,this.GetType().FullName);
                            context.SetError(500, message.ToPublish());
                            EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (eXtensibleConfig.CaptureMetrics)
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                        }
                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.GetAllProjections, ex, null, criterion, context, this.GetType().FullName, database);
                        context.SetError(500,message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error);
                    }
                }                
            }


            return list;
        }

        public virtual object ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            return DynamicExecuteAction<U>(t, criterion, context);
        }

        public virtual object ExecuteMany<U>(IEnumerable<T> list, ICriterion criterion, IContext context)
        {
            string message = String.Format("The method or operation is not implemented: {0}.{1} in {2}", GetModelType().FullName, ModelActionOption.ExecuteMany, this.GetType().FullName);
            return new NotImplementedException(message);
        }

        public virtual DataSet ExecuteCommand(DataSet dataSet, ICriterion criterion, IContext context)
        {
            DataSet ds = new DataSet() { DataSetName = "DataSet" };

            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = resolver.ExecuteCommandSqlCommand(cn, dataSet, criterion, context);
                        if (cmd != null)
                        {
                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.Database.Command, cmd.CommandText);
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
                            }
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                
                                try
                                {
                                    adapter.Fill(ds);
                                }
                                catch (Exception ex)
                                {
                                    string s = cn.ConnectionString;
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.ExecuteCommand, ex, null, criterion, context, this.GetType().FullName);
                                    context.SetError(500, message.ToPublish());
                                    context.SetStacktrace(ex.StackTrace);
                                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                                }
                            }

                            if (eXtensibleConfig.CaptureMetrics)
                            {
                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                            }
                        }
                        else
                        {
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.ExecuteCommand, null, criterion, context, this.GetType().FullName);
                            context.SetError(500, message.ToPublish());
                            EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (eXtensibleConfig.CaptureMetrics)
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
                        }
                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.ExecuteCommand, ex, null, criterion, context, this.GetType().FullName, database);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }
                }
            }


            return ds;
        }


        private object DynamicExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            object o = null;

            if (criterion.Contains(XFConstants.Application.ActionExecuteStrategy))
            {
                string method = criterion.GetStringValue(XFConstants.Application.ActionExecuteMethodName);
                Type impl = GetType();
                MethodInfo[] infos = impl.GetMethods();
                MethodInfo info = this.GetType().GetMethod(method);
                if (info == null)
                {
                }
                else
                {
                    List<object> paramList = new List<object>();
                    int j = 1;
                    string key = String.Format("{0}:{1}", XFConstants.Application.StrategyKey, j.ToString());
                    foreach (var item in criterion.Items)
                    {
                        if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                        {
                            paramList.Add(item.Value);
                            j++;
                            key = String.Format("{0}:{1}", XFConstants.Application.StrategyKey, j.ToString());
                        }
                    }
                    try
                    {
                        o = info.Invoke(this, paramList.ToArray());
                    }
                    catch (Exception ex)
                    {
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.ExecuteAction, ex, t, criterion, context, this.GetType().FullName);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }

                    if (o is U)
                    {
                        return (U)o;
                    }
                }
            }
            else
            {
                string message = String.Format("The method or operation is not implemented: {0}.{1} in {2}",GetModelType().FullName, ModelActionOption.ExecuteAction,this.GetType().FullName);
                o = new NotImplementedException(message);
            }
            return o;
        }

       

        public virtual SqlCommand PostSqlCommand(SqlConnection cn, T t, IContext context)
        {
            return null;
        }

        public virtual SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand PutSqlCommand(SqlConnection cn, T t, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand ExecuteCommandSqlCommand(SqlConnection cn, DataSet dataSet, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual void BorrowReader(SqlDataReader reader, List<T> list)
        {
            IListBorrower<T> borrower = new Borrower<T>(GetDataMaps());
            if (borrower != null)
            {
                try
                {
                    borrower.BorrowReader(reader, list);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

        public virtual void BorrowReaderProjections(SqlDataReader reader, List<Projection> list)
        {
            try
            {
                string s = GetModelType().FullName;
                bool hasGroupField = reader.FieldExists("Group");
                bool hasIntValField = reader.FieldExists("IntVal");
                bool hasAltDisplayField = reader.FieldExists("DisplayAlt");
                bool hasStatusField = reader.FieldExists("Status");
                while (reader.Read())
                {
                    Projection item = new Projection() { Typename = s };
                    item.Id = (reader["Id"] != DBNull.Value) ? reader["Id"].ToString() : String.Empty;
                    item.Display = (reader["Display"] != DBNull.Value) ? reader["Display"].ToString() : String.Empty;
                    if (hasAltDisplayField)
                    {
                        item.DisplayAlt = (reader["DisplayAlt"] != DBNull.Value) ? reader["DisplayAlt"].ToString() : String.Empty;
                    }                    
                    if (hasGroupField)
                    {
                        item.Group = (reader["Group"] != DBNull.Value) ? reader["Group"].ToString() : String.Empty;
                    }
                    if (hasIntValField)
                    {
                        int i;
                        if (reader["IntVal"] != DBNull.Value && Int32.TryParse(reader["IntVal"].ToString(), out i))
                        {
                            item.IntVal = i;
                        }
                    }
                    if (hasStatusField)
                    {
                        item.Status = (reader["Status"] != DBNull.Value) ? reader["Status"].ToString() : String.Empty;
                    }

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual List<DataMap> GetDataMaps()
        {
            return null;
        }

        public virtual Type GetModelType()
        {
            return GetModelType<T>();
        }

        public virtual string GetDatabaseSchema()
        {
            return XFConstants.Application.Defaults.SqlServerSchema;
        }

        protected virtual string ResolveConnectionStringKey(IContext context)
        {
            return String.Empty;
        }

        #endregion

        #region helper methods
        private static Type GetModelType<T>()
        {
            return Activator.CreateInstance<T>().GetType();
        }

        protected bool ExecuteBulkCopy(IEnumerable<T> list, string tableName)
        {
            return ExecuteBulkCopy(list, tableName, null);
        }

        protected bool ExecuteBulkCopy(IEnumerable<T> list, string tableName, Dictionary<string,string> maps)
        {
            return ExecuteBulkCopy(list, tableName, maps, 10000);
        }

        protected bool ExecuteBulkCopy(IEnumerable<T> list, string tableName, Dictionary<string, string> maps = null, int batchSize = 10000)
        {
            bool b = false;
            try
            {
                var reader = list.AsDataReader();
                using (SqlConnection cn = DbConnection)
                {
                    cn.Open();
                    SqlBulkCopy sbc = new SqlBulkCopy(cn, SqlBulkCopyOptions.Default, null);
                    if (maps == null)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);
                            sbc.ColumnMappings.Add(columnName, columnName);
                        }                   
                    }
                    else
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {                            
                            string sourceName = reader.GetName(i);
                            string destinationName = (maps.ContainsKey(sourceName)) ? maps[sourceName] : sourceName;
                            sbc.ColumnMappings.Add(sourceName, destinationName);
                        }
                    }
                    sbc.BatchSize = batchSize;
                    sbc.DestinationTableName = tableName;
                    sbc.WriteToServer(reader);
                    sbc.Close();
                    b = true;
                }
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralException(ex, "Error in ExecuteBulkCopy", this.GetType().FullName);
                Context.SetError(500, message.ToPublish());
                Context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, Context.ToDictionary(message.Id));
            }
            return b;
        }

        #endregion

        void IModelDataGatewayInitializeable.Initialize<T>(ModelActionOption option, IContext context, T t, ICriterion criterion, ResolveDbKey<T> dbkeyResolver)
        {
            string connectionStringKey = ResolveConnectionStringKey(context);
            if (String.IsNullOrWhiteSpace(connectionStringKey))
            {
                connectionStringKey = dbkeyResolver.Invoke(context);
            }
            if (String.IsNullOrEmpty(connectionStringKey))
            {
                int connectionStringCount = ConfigurationProvider.ConnectionStrings.Count;
                if (connectionStringCount > 0)
                {
                    connectionStringKey = ConfigurationProvider.ConnectionStrings[connectionStringCount - 1].Name;                    
                }
            }               
            if (String.IsNullOrEmpty(connectionStringKey))
            {
                var message = Exceptions.ComposeDbConnectionStringKeyResolutionError<T>(option, t, context);
                context.SetError(500, message.ToPublish());
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }
            else
            {
                ConnectionStringSettings settings = ConfigurationProvider.ConnectionStrings[connectionStringKey];
                if (settings == null)
                {
                    var message = Exceptions.ComposeDbConnectionNullSettingsError<T>(option, t, context, connectionStringKey);
                    context.SetError(500, message.ToPublish());
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                }
                else
                {
                    SqlConnection connection = null;
                    try
                    {
                        connection = new SqlConnection(settings.ConnectionString);
                    }
                    catch (Exception ex)
                    {                        
                        var message = Exceptions.ComposeDbConnectionStringFormatError<T>(option, t, context, connectionStringKey, settings.ConnectionString);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    } 
                    if (connection == null)
                    {
                        var message = Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, connectionStringKey);
                        context.SetError(500, message.ToPublish());
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }
                    else
                    {
                        DbConnection = connection;
                        if (eXtensibleConfig.CaptureMetrics)
                        {
                            IRequestContext ctx = context as IRequestContext;
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Database.Datasource, String.Format("server:{0};database:{1};", connection.DataSource, connection.Database)); 
                        }
                    }
                }

            }            
        }



    }
}
