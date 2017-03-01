//// <copyright file="SqlServerModelDataGateway`1.cs" company="eXtensible Solutions, LLC">
//// Copyright © 2015 All Right Reserved
//// </copyright>

//namespace XF.DataServices
//{
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.Composition;
//    using System.Configuration;
//    using System.Data.SqlClient;
//    using System.Reflection;
//    using System.Text;
//    using XF.Common;

//    [InheritedExport(typeof(ITypeMap))]
//    public abstract class SqlServerModelDataGateway<T, U> : IModelDataGateway<T>, ISqlCommandContext<T>, IModelDataGatewayInitializeable
//        where T : class, new()
//        where U : class, new()
//    {
//        private const string DataStorePlatform = "sqlserver";

//        private ICache _Cache;
//        protected ICache Cache
//        {
//            get
//            {
//                if (_Cache == null)
//                {
//                    _Cache = CacheStrategyLoader.Load();
//                }
//                return _Cache;
//            }
//        }

//        private const string Module = "XF.Common.Server";
//        private const string Class = "SqlServerModelDataGatewayT";

//        private IDatastoreService _DataService = null;
//        public IDatastoreService DataService
//        {
//            get
//            {
//                return _DataService;
//            }
//            set
//            {
//                _DataService = value;
//            }
//        }

//        private IContext _Context = null;

//        public IContext Context
//        {
//            get
//            {
//                return _Context;
//            }
//            set
//            {
//                _Context = value; ;
//            }
//        }

//        string ITypeMap.Domain
//        {
//            get { throw new NotImplementedException(); }
//        }

//        Type ITypeMap.KeyType
//        {
//            get { return GetModelType(); }
//        }

//        SqlConnection _DbConnection = null;
//        public SqlConnection DbConnection
//        {
//            get
//            {
//                return _DbConnection;
//            }
//            set
//            {
//                _DbConnection = value;
//            }
//        }


//        string ISqlCommandContext<T>.Schema
//        {
//            get { return GetDatabaseSchema(); }
//        }

//        Type ITypeMap.TypeResolution
//        {
//            get { return this.GetType(); }
//        }

//        T IModelDataGateway<T>.Post(T t, IContext context)
//        {
//            IRequestContext ctx = context as IRequestContext;
//            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
//            T result = null;

//            try
//            {
//                result = Post(t, context);
//            }
//            catch (Exception ex)
//            {
//                string message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName);
//                context.SetError(500, message);
//                EventWriter.WriteError(message, SeverityType.Error);
//            }
//            return result;
//        }

//        T IModelDataGateway<T>.Get(ICriterion criterion, IContext context)
//        {
//            IRequestContext ctx = context as IRequestContext;
//            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
//            T result = null;

//            try
//            {
//                result = Get(criterion, context);
//            }
//            catch (Exception ex)
//            {
//                string message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName);
//                context.SetError(500, message);
//                EventWriter.WriteError(message, SeverityType.Error);
//            }
//            return result;
//        }

//        T IModelDataGateway<T>.Put(T t, ICriterion criterion, IContext context)
//        {
//            IRequestContext ctx = context as IRequestContext;
//            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
//            T result = null;

//            try
//            {
//                result = Put(t, criterion, context);
//            }
//            catch (Exception ex)
//            {
//                string message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName);
//                context.SetError(500, message);
//                EventWriter.WriteError(message, SeverityType.Error);
//            }
//            return result;
//        }

//        ICriterion IModelDataGateway<T>.Delete(ICriterion criterion, IContext context)
//        {
//            IRequestContext ctx = context as IRequestContext;
//            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
//            ICriterion result = null;

//            try
//            {
//                result = Delete(criterion, context);
//            }
//            catch (Exception ex)
//            {
//                string message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Put, ex, null, criterion, context, this.GetType().FullName);
//                context.SetError(500, message);
//                EventWriter.WriteError(message, SeverityType.Error);
//            }
//            return result;
//        }

//        IEnumerable<T> IModelDataGateway<T>.GetAll(ICriterion criterion, IContext context)
//        {
//            IRequestContext ctx = context as IRequestContext;
//            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
//            IEnumerable<T> result = null;

//            try
//            {
//                result = GetAll(criterion, context);
//            }
//            catch (Exception ex)
//            {
//                string message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName);
//                context.SetError(500, message);
//                EventWriter.WriteError(message, SeverityType.Error);
//            }
//            return result;
//        }

//        IEnumerable<DisplayItem> IModelDataGateway<T>.GetAllDisplay(ICriterion criterion, IContext context)
//        {
//            IRequestContext ctx = context as IRequestContext;
//            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);
//            IEnumerable<DisplayItem> result = null;

//            try
//            {
//                result = GetAllDisplay(criterion, context);
//            }
//            catch (Exception ex)
//            {
//                string message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAllDisplay, ex, null, criterion, context, this.GetType().FullName);
//                context.SetError(500, message);
//                EventWriter.WriteError(message, SeverityType.Error);
//            }
//            return result;
//        }

//        U IModelDataGateway<T>.ExecuteAction<U>(T t, ICriterion criterion, IContext context)
//        {
//            IRequestContext ctx = context as IRequestContext;
//            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.DbType, DataStorePlatform);

//            U u = default(U);
//            object o = null;

//            if (criterion.Contains(XFConstants.Application.ActionExecuteStrategy))
//            {
//                o = DynamicExecuteAction<U>(t, criterion, context);
//            }
//            else
//            {
//                try
//                {
//                    o = ExecuteAction<U>(t, criterion, context);
//                }
//                catch (Exception ex)
//                {
//                    string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
//                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.ExecuteAction.ToString(), Module, Class, "86");
//                    string message = String.Format("ExecuteAction Error: {0} \t{1}", s, props);
//                    context.SetError(500, message);
//                    EventWriter.WriteError(message, SeverityType.Error);
//                }
//            }
//            if (o is U)
//            {
//                u = (U)o;
//            }
//            return u;
//        }

//        #region abstract methods

//        protected virtual U ConvertToU(T t)
//        {
//            return default(U);
//        }

//        protected virtual T ConvertToT(U model)
//        {
//            return default(T);
//        }

//        #endregion


//        SqlCommand ISqlCommandContext<T>.InsertCommand(SqlConnection cn, T t, IContext context)
//        {
//            return InsertDbCommand(cn, t, context);
//        }

//        SqlCommand ISqlCommandContext<T>.SelectOneCommand(SqlConnection cn, ICriterion criterion, IContext context)
//        {
//            return SelectOneDbCommand(cn, criterion, context);
//        }

//        SqlCommand ISqlCommandContext<T>.UpdateCommand(SqlConnection cn, T t, ICriterion criterion, IContext context)
//        {
//            return UpdateDbCommand(cn, t, criterion, context);
//        }

//        SqlCommand ISqlCommandContext<T>.DeleteCommand(SqlConnection cn, ICriterion criterion, IContext context)
//        {
//            return DeleteDbCommand(cn, criterion, context);
//        }

//        SqlCommand ISqlCommandContext<T>.SelectManyCommand(SqlConnection cn, ICriterion criterion, IContext context)
//        {
//            return SelectManyDbCommand(cn, criterion, context);
//        }

//        SqlCommand ISqlCommandContext<T>.SelectManyDisplayCommand(SqlConnection cn, ICriterion criterion, IContext context)
//        {
//            return SelectManyDisplayDbCommand(cn, criterion, context);
//        }



//        #region overrideable methods

//        public virtual T Post(T t, IContext context)
//        {
//            T item = default(T);
//            if (DbConnection != null)
//            {
//                IRequestContext ctx = context as IRequestContext;
//                List<T> list = new List<T>();
//                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
//                SqlCommand cmd = null;

//                using (SqlConnection cn = DbConnection)
//                {
//                    try
//                    {
//                        cn.Open();
//                        cmd = resolver.InsertCommand(cn, t, context);
//                        if (cmd == null)
//                        {
//                            cmd = SqlResolver.ResolveCreateCommand<T>(cn, resolver.Schema, t, GetDataMaps());
//                        }
//                        if (cmd != null)
//                        {
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.SqlServer.SqlCommand, cmd.CommandText);
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
//                            }
//                            using (SqlDataReader reader = cmd.ExecuteReader())
//                            {
//                                try
//                                {
//                                    BorrowReader(reader, list);
//                                    if (list.Count > 0)
//                                    {
//                                        item = list[0];
//                                    }
//                                }
//                                catch (Exception ex)
//                                {
//                                    string message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName);
//                                    Context.SetError(500, message);
//                                    EventWriter.WriteError(message, SeverityType.Error);
//                                }
//                            }
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
//                            }
//                        }
//                        else
//                        {
//                            string message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Post, t, null, context, this.GetType().FullName);
//                            context.SetError(500, message);
//                            EventWriter.WriteError(message, SeverityType.Error);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
//                        string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
//                        Context.SetError(500, Exceptions.ComposeSqlException<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName, database));
//                    }
//                    if (list.Count >= 1)
//                    {
//                        item = list[0];
//                    }
//                }
//            }


//            return item;
//        }

//        public virtual T Get(ICriterion criterion, IContext context)
//        {

//            T item = default(T);

//            if (DbConnection != null)
//            {
//                IRequestContext ctx = context as IRequestContext;
//                List<T> list = new List<T>();
//                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
//                SqlCommand cmd = null;

//                using (SqlConnection cn = DbConnection)
//                {
//                    try
//                    {
//                        cn.Open();
//                        cmd = resolver.SelectOneCommand(cn, criterion, context);
//                        if (cmd == null)
//                        {
//                            cmd = SqlResolver.ResolveReadCommand<T>(cn, resolver.Schema, criterion);
//                        }
//                        if (cmd != null)
//                        {
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.SqlServer.SqlCommand, cmd.CommandText);
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
//                            }
//                            using (SqlDataReader reader = cmd.ExecuteReader())
//                            {
//                                try
//                                {
//                                    BorrowReader(reader, list);
//                                }
//                                catch (Exception ex)
//                                {
//                                    string message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName);
//                                    Context.SetError(500, message);
//                                    EventWriter.WriteError(message, SeverityType.Error);
//                                }
//                            }
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
//                            }
//                        }
//                        else
//                        {
//                            string message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAllDisplay, null, criterion, context, this.GetType().FullName);
//                            context.SetError(500, message);
//                            EventWriter.WriteError(message, SeverityType.Error);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
//                        string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
//                        Context.SetError(500, Exceptions.ComposeSqlException<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName, database));
//                    }
//                }
//                if (list.Count > 0)
//                {
//                    item = list[0];
//                }
//            }

//            return item;
//        }

//        public virtual T Put(T t, ICriterion criterion, IContext context)
//        {

//            T item = default(T);

//            if (DbConnection != null)
//            {
//                IRequestContext ctx = context as IRequestContext;
//                List<T> list = new List<T>();
//                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
//                SqlCommand cmd = null;

//                using (SqlConnection cn = DbConnection)
//                {
//                    try
//                    {
//                        cn.Open();
//                        cmd = resolver.UpdateCommand(cn, t, criterion, context);
//                        if (cmd == null)
//                        {
//                            cmd = SqlResolver.ResolveUpdateCommand<T>(cn, resolver.Schema, t);
//                        }
//                        if (cmd != null)
//                        {
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.SqlServer.SqlCommand, cmd.CommandText);
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
//                            }
//                            using (SqlDataReader reader = cmd.ExecuteReader())
//                            {
//                                try
//                                {
//                                    BorrowReader(reader, list);
//                                }
//                                catch (Exception ex)
//                                {
//                                    string message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName);
//                                    Context.SetError(500, message);
//                                    EventWriter.WriteError(message, SeverityType.Error);
//                                }

//                            }
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {

//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
//                            }
//                        }
//                        else
//                        {
//                            string message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Put, t, null, context, this.GetType().FullName);
//                            context.SetError(500, message);
//                            EventWriter.WriteError(message, SeverityType.Error);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
//                        string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
//                        string message = Exceptions.ComposeSqlException<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName, database);
//                        Context.SetError(500, message);
//                        EventWriter.WriteError(message, SeverityType.Error);
//                    }
//                }
//                if (list.Count >= 1)
//                {
//                    item = list[0];
//                }
//            }

//            return item;
//        }

//        public virtual IEnumerable<T> GetAll(ICriterion criterion, IContext context)
//        {

//            List<T> list = new List<T>();

//            if (DbConnection != null)
//            {
//                IRequestContext ctx = context as IRequestContext;
//                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
//                SqlCommand cmd = null;

//                using (SqlConnection cn = DbConnection)
//                {
//                    try
//                    {
//                        cn.Open();
//                        cmd = resolver.SelectManyCommand(cn, criterion, context);
//                        if (cmd == null)
//                        {
//                            cmd = SqlResolver.ResolveReadListCommand<T>(cn, resolver.Schema, criterion);
//                        }
//                        if (cmd != null)
//                        {
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.SqlServer.SqlCommand, cmd.CommandText);
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
//                            }
//                            using (SqlDataReader reader = cmd.ExecuteReader())
//                            {
//                                try
//                                {
//                                    BorrowReader(reader, list);
//                                }
//                                catch (Exception ex)
//                                {
//                                    string s = cn.ConnectionString;
//                                    string message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName);
//                                    Context.SetError(500, message);
//                                    EventWriter.WriteError(message, SeverityType.Error);
//                                }

//                            }
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
//                            }
//                        }
//                        else
//                        {
//                            string message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAll, null, criterion, context, this.GetType().FullName);
//                            context.SetError(500, message);
//                            EventWriter.WriteError(message, SeverityType.Error);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
//                        string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
//                        string message = Exceptions.ComposeSqlException<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName, database);
//                        Context.SetError(500, message);
//                        EventWriter.WriteError(message, SeverityType.Error);
//                    }
//                }
//            }


//            return list;
//        }

//        public virtual ICriterion Delete(ICriterion criterion, IContext context)
//        {

//            Criterion item = new Criterion();

//            if (DbConnection != null)
//            {
//                IRequestContext ctx = context as IRequestContext;
//                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
//                SqlCommand cmd = null;

//                using (SqlConnection cn = DbConnection)
//                {
//                    try
//                    {
//                        cn.Open();
//                        cmd = resolver.DeleteCommand(cn, criterion, context);
//                        if (cmd == null)
//                        {
//                            cmd = SqlResolver.ResolveDeleteCommand<T>(cn, resolver.Schema, criterion);
//                        }
//                        if (cmd != null)
//                        {
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.SqlServer.SqlCommand, cmd.CommandText);
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
//                            }
//                            int i = cmd.ExecuteNonQuery();
//                            bool b = (i == 1) ? true : false;
//                            item.AddItem("Success", b);
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
//                            }
//                        }
//                        else
//                        {
//                            string message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Delete, null, criterion, context, this.GetType().FullName);
//                            context.SetError(500, message);
//                            EventWriter.WriteError(message, SeverityType.Error);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
//                        string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
//                        string message = Exceptions.ComposeSqlException<T>(ModelActionOption.Delete, ex, null, criterion, context, this.GetType().FullName, database);
//                        Context.SetError(500, message);
//                        EventWriter.WriteError(message, SeverityType.Error);
//                    }
//                }
//            }

//            return item;
//        }

//        public virtual IEnumerable<DisplayItem> GetAllDisplay(ICriterion criterion, IContext context)
//        {

//            List<DisplayItem> list = new List<DisplayItem>();

//            if (DbConnection != null)
//            {
//                IRequestContext ctx = context as IRequestContext;
//                ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
//                SqlCommand cmd = null;

//                using (SqlConnection cn = DbConnection)
//                {
//                    try
//                    {
//                        cn.Open();
//                        cmd = resolver.SelectManyDisplayCommand(cn, criterion, context);
//                        if (cmd == null)
//                        {
//                            cmd = SqlResolver.ResolveReadListDisplayCommand<T>(cn, resolver.Schema, criterion);
//                        }
//                        if (cmd != null)
//                        {
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(cmd.CommandType.ToString(), XFConstants.Metrics.SqlServer.SqlCommand, cmd.CommandText);
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.Begin, DateTime.Now);
//                            }
//                            using (SqlDataReader reader = cmd.ExecuteReader())
//                            {
//                                try
//                                {
//                                    BorrowReaderDisplay(reader, list);
//                                }
//                                catch (Exception ex)
//                                {
//                                    string message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.GetAllDisplay, ex, null, criterion, context, this.GetType().FullName);
//                                    Context.SetError(500, message);
//                                    EventWriter.WriteError(message, SeverityType.Error);
//                                }
//                            }
//                            if (eXtensibleConfig.CaptureMetrics)
//                            {
//                                ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.Command.End, DateTime.Now);
//                            }
//                        }
//                        else
//                        {
//                            string message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAllDisplay, null, criterion, context, this.GetType().FullName);
//                            context.SetError(500, message);
//                            EventWriter.WriteError(message, SeverityType.Error);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
//                        string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
//                        string message = Exceptions.ComposeSqlException<T>(ModelActionOption.GetAllDisplay, ex, null, criterion, context, this.GetType().FullName, database);
//                        Context.SetError(500, message);
//                        EventWriter.WriteError(message, SeverityType.Error);
//                    }
//                }
//            }


//            return list;
//        }

//        public virtual object ExecuteAction<U>(T t, ICriterion criterion, IContext context)
//        {
//            return DynamicExecuteAction<U>(t, criterion, context);
//        }

//        private object DynamicExecuteAction<U>(T t, ICriterion criterion, IContext context)
//        {
//            IRequestContext ctx = context as IRequestContext;
//            object o = null;

//            if (criterion.Contains(XFConstants.Application.ActionExecuteStrategy))
//            {
//                string method = criterion.GetStringValue(XFConstants.Application.ActionExecuteMethodName);
//                Type impl = GetType();
//                MethodInfo[] infos = impl.GetMethods();
//                MethodInfo info = this.GetType().GetMethod(method);
//                if (info == null)
//                {
//                }
//                else
//                {
//                    List<object> paramList = new List<object>();
//                    int j = 1;
//                    string key = String.Format("{0}:{1}", XFConstants.Application.StrategyKey, j.ToString());
//                    foreach (var item in criterion.Items)
//                    {
//                        if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
//                        {
//                            paramList.Add(item.Value);
//                            j++;
//                            key = String.Format("{0}:{1}", XFConstants.Application.StrategyKey, j.ToString());
//                        }
//                    }
//                    try
//                    {
//                        o = info.Invoke(this, paramList.ToArray());
//                    }
//                    catch (Exception ex)
//                    {
//                        string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
//                        context.SetError(500, s);
//                        EventWriter.WriteError(s, SeverityType.Error);
//                    }

//                    if (o is U)
//                    {
//                        return (U)o;
//                    }
//                }
//            }
//            return o;
//        }

//        public virtual SqlCommand InsertDbCommand(SqlConnection cn, T t, IContext context)
//        {
//            return null;
//        }

//        public virtual SqlCommand SelectOneDbCommand(SqlConnection cn, ICriterion criterion, IContext context)
//        {
//            return null;
//        }

//        public virtual SqlCommand UpdateDbCommand(SqlConnection cn, T t, ICriterion criterion, IContext context)
//        {
//            return null;
//        }

//        public virtual SqlCommand DeleteDbCommand(SqlConnection cn, ICriterion criterion, IContext context)
//        {
//            return null;
//        }

//        public virtual SqlCommand SelectManyDbCommand(SqlConnection cn, ICriterion criterion, IContext context)
//        {
//            return null;
//        }

//        public virtual SqlCommand SelectManyDisplayDbCommand(SqlConnection cn, ICriterion criterion, IContext context)
//        {
//            return null;
//        }

//        public virtual void BorrowReader(SqlDataReader reader, List<T> list)
//        {
//            IListBorrower<T> borrower = new Borrower<T>(GetDataMaps());
//            if (borrower != null)
//            {
//                try
//                {
//                    borrower.BorrowReader(reader, list);
//                }
//                catch (Exception ex)
//                {
//                    throw;
//                }

//            }
//        }

//        public virtual void BorrowReaderDisplay(SqlDataReader reader, List<DisplayItem> list)
//        {
//            try
//            {
//                string s = GetModelType().FullName;
//                bool hasGroupField = reader.FieldExists("Group");
//                bool hasIntValField = reader.FieldExists("IntVal");
//                bool hasAltDisplayField = reader.FieldExists("ItemDisplayAlt");
//                while (reader.Read())
//                {
//                    DisplayItem item = new DisplayItem() { Typename = s };
//                    item.ItemId = (reader["ItemId"] != DBNull.Value) ? reader["ItemId"].ToString() : String.Empty;
//                    item.ItemDisplay = (reader["ItemDisplay"] != DBNull.Value) ? reader["ItemDisplay"].ToString() : String.Empty;
//                    if (hasAltDisplayField)
//                    {
//                        item.ItemDisplayAlt = (reader["ItemDisplayAlt"] != DBNull.Value) ? reader["ItemDisplayAlt"].ToString() : String.Empty;
//                    }
//                    if (hasGroupField)
//                    {
//                        item.Group = (reader["Group"] != DBNull.Value) ? reader["Group"].ToString() : String.Empty;
//                    }
//                    if (hasIntValField)
//                    {
//                        int i;
//                        if (reader["IntVal"] != DBNull.Value && Int32.TryParse(reader["IntVal"].ToString(), out i))
//                        {
//                            item.IntVal = i;
//                        }
//                    }

//                    list.Add(item);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public virtual List<DataMap> GetDataMaps()
//        {
//            return null;
//        }

//        public virtual Type GetModelType()
//        {
//            return GetModelType<T>();
//        }

//        public virtual string GetDatabaseSchema()
//        {
//            return XFConstants.Application.Defaults.SqlServerSchema;
//        }

//        protected virtual string ResolveConnectionStringKey(IContext context)
//        {
//            return String.Empty;
//        }

//        #endregion

//        #region helper methods
//        private static Type GetModelType<T>()
//        {
//            return Activator.CreateInstance<T>().GetType();
//        }



//        #endregion

//        void IModelDataGatewayInitializeable.Initialize<T>(ModelActionOption option, IContext context, T t, ICriterion criterion, ResolveDbKey<T> dbkeyResolver)
//        {
//            string connectionStringKey = ResolveConnectionStringKey(context);
//            if (String.IsNullOrWhiteSpace(connectionStringKey))
//            {
//                connectionStringKey = dbkeyResolver.Invoke(context);
//            }
//            if (String.IsNullOrEmpty(connectionStringKey))
//            {
//                int connectionStringCount = ConfigurationManager.ConnectionStrings.Count;
//                if (connectionStringCount > 0)
//                {
//                    connectionStringKey = ConfigurationManager.ConnectionStrings[connectionStringCount - 1].Name;
//                }
//            }
//            if (String.IsNullOrEmpty(connectionStringKey))
//            {
//                string message = Exceptions.ComposeDbConnectionStringKeyResolutionError<T>(option, t, context);
//                context.SetError(500, message);
//                EventWriter.WriteError(message, SeverityType.Error);
//            }
//            else
//            {
//                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringKey];
//                if (settings == null)
//                {
//                    string message = Exceptions.ComposeDbConnectionNullSettingsError<T>(option, t, context, connectionStringKey);
//                    context.SetError(500, message);
//                    EventWriter.WriteError(message, SeverityType.Error);
//                }
//                else
//                {
//                    SqlConnection connection = new SqlConnection(settings.ConnectionString);
//                    if (connection == null)
//                    {
//                        string message = Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, connectionStringKey);
//                        context.SetError(500, message);
//                        EventWriter.WriteError(message, SeverityType.Error);
//                    }
//                    else
//                    {
//                        DbConnection = connection;
//                        if (eXtensibleConfig.CaptureMetrics)
//                        {
//                            IRequestContext ctx = context as IRequestContext;
//                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.SqlServer.Datasource, String.Format("server:{0};database:{1};", connection.DataSource, connection.Database));
//                        }
//                    }
//                }

//            }
//        }

//    }
//}
