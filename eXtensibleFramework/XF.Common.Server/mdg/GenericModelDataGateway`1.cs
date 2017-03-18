

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GenericModelDataGateway<T> : IModelDataGatewayInitializeable, IModelDataGateway<T> where T : class, new()
    {
        private const string DataStorePlatform = "sqlserver";
        private const string Module = "XF.Common.Server";
        private const string Class = "GenericModelDataGateway`1";

        SqlConnection _DbConnection = null;
        SqlConnection DbConnection
        {
            get { return _DbConnection; }
            set { _DbConnection = value; }
        }

        private IDatastoreService _DataService = null;
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

            if (o is U)
            {
                u = (U)o;
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
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, null, criterion, context, this.GetType().FullName);
                context.SetError(500, message.ToPublish());
                context.SetStacktrace(ex.StackTrace);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
            }

            if (o is U)
            {
                u = (U)o;
            }
            return u;
        }


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


        #region overridable methods

        public virtual T Post(T t, IContext context)
        {
            T item = default(T);
            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                List<T> list = new List<T>();
                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = PostSqlCommand(cn, t, context);

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
                        string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName, database);
                        context.SetError(500, message.ToPublish());
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

                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = GetSqlCommand(cn, criterion, context);

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
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Get, null, criterion, context, this.GetType().FullName);
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
                        context.SetError(500, message.ToPublish());
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
                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = PutSqlCommand(cn, t, criterion, context);
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
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName);
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
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Put, t, null, context, this.GetType().FullName);
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

        public virtual ICriterion Delete(ICriterion criterion, IContext context)
        {
            Criterion item = new Criterion();
            if (criterion != null)
            {

                if (DbConnection != null)
                {
                    IRequestContext ctx = context as IRequestContext;
                    //ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
                    SqlCommand cmd = null;
                    int i = 0;
                    using (SqlConnection cn = DbConnection)
                    {
                        try
                        {
                            cn.Open();
                            cmd = DeleteSqlCommand(cn, criterion, context);

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
                                var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Delete, null, criterion, context, this.GetType().FullName);
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
            }
            else if (eXtensibleConfig.Inform)
            {
                EventWriter.Inform(String.Format("No criterion was provided for {0}.Delete method in {1}", GetModelType().FullName, this.GetType().FullName));
            }

            return item;
        }

        public virtual IEnumerable<T> GetAll(ICriterion criterion, IContext context)
        {
            List<T> list = new List<T>();

            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                SqlCommand cmd = null;
                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = GetAllSqlCommand(cn, criterion, context);
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
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName);
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
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName, database);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }
                }
            }

            return list;
        }

        public virtual IEnumerable<Projection> GetAllProjections(ICriterion criterion, IContext context)
        {
            List<Projection> list = new List<Projection>();

            if (DbConnection != null)
            {
                IRequestContext ctx = context as IRequestContext;
                SqlCommand cmd = null;

                using (SqlConnection cn = DbConnection)
                {
                    try
                    {
                        cn.Open();
                        cmd = GetAllProjectionsSqlCommand(cn, criterion, context);

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
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAllProjections, null, criterion, context, this.GetType().FullName);
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
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error);
                    }
                }
            }


            return list;
        }

        public virtual U ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.ExecuteAction.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
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
                        cmd = ExecuteCommandSqlCommand(cn, dataSet, criterion, context);
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
                                    var message = Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName);
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
                            var message = Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAll, null, criterion, context, this.GetType().FullName);
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
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName, database);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }
                }
            }


            return ds;
        }

        public virtual U ExecuteMany<U>(IEnumerable<T> list, ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.ExecuteMany.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }



        protected virtual string ResolveConnectionStringKey(IContext context)
        {
            return String.Empty;
        }

        protected virtual List<DataMap> GetDataMaps()
        {
            return null;
        }

        #region readers

        protected virtual void BorrowReader(SqlDataReader reader, List<T> list)
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

        protected virtual void BorrowReaderProjections(SqlDataReader reader, List<Projection> list)
        {
            try
            {
                string s = GetModelType().FullName;
                bool hasGroupField = reader.FieldExists("Group");
                bool hasIntValField = reader.FieldExists("IntVal");
                bool hasAltDisplayField = reader.FieldExists("DisplayAlt");
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

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        #endregion

        #region idb commands

        protected virtual SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            ModelActionOption option = ModelActionOption.GetAll;
            SqlCommand cmd = null;
            XF.Common.Discovery.SqlTable table = null;
            if (SprocMapCache.Instance.TryGetTable<T>(context, cn, out table))
            {
                cmd = XF.Common.Discovery.InlineSqlCommandGenerator.Generate(cn, table, option, criterion);
            }
            return cmd;
        }

        protected virtual SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            ModelActionOption option = ModelActionOption.Delete;
            SqlCommand cmd = null;
            XF.Common.Discovery.SqlTable table = null;
            if (SprocMapCache.Instance.TryGetTable<T>(context, cn, out table))
            {
                cmd = XF.Common.Discovery.InlineSqlCommandGenerator.Generate(cn, table, option, criterion);
            }
            return cmd;
        }

        protected virtual SqlCommand PutSqlCommand(SqlConnection cn, T t, ICriterion criterion, IContext context)
        {
            ModelActionOption option = ModelActionOption.Put;
            SqlCommand cmd = null;
            XF.Common.Discovery.SqlTable table = null;

            if (criterion != null)
            {
                if (SprocMapCache.Instance.TryGetTable<T>(context, cn, out table))
                {
                    cmd = XF.Common.Discovery.InlineSqlCommandGenerator.Generate(cn, table, option, criterion);
                }                
            }
            else if (t != null)
            {
                if (SprocMapCache.Instance.TryGetTable<T>(context, cn, out table))
                {
                    cmd = XF.Common.Discovery.InlineSqlCommandGenerator.Generate<T>(cn, table, option, t);
                }                
            }

            return cmd;
        }

        protected virtual SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            ModelActionOption option = ModelActionOption.Get;
            SqlCommand cmd = null;
            XF.Common.Discovery.SqlTable table = null;
            if (SprocMapCache.Instance.TryGetTable<T>(context, cn, out table))
            {
                cmd = XF.Common.Discovery.InlineSqlCommandGenerator.Generate(cn, table, option, criterion);
            }
            return cmd;
        }

        protected virtual SqlCommand PostSqlCommand(SqlConnection cn, T t, IContext context)
        {
            ModelActionOption option = ModelActionOption.Post;
            SqlCommand cmd = null;
            XF.Common.Discovery.SqlTable table = null;
            if (SprocMapCache.Instance.TryGetTable<T>(context, cn, out table))
            {
                cmd = XF.Common.Discovery.InlineSqlCommandGenerator.Generate<T>(cn, table, option, t);
            }
            return cmd;
        }

        protected virtual SqlCommand GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual SqlCommand ExecuteCommandSqlCommand(SqlConnection cn, DataSet dataSet, ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected Type GetModelType()
        {
            return Activator.CreateInstance<T>().GetType();
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
                    var found = ConfigurationProvider.ConnectionStrings[connectionStringCount - 1];
                    connectionStringKey = found.Name;
                    if (eXtensibleConfig.Inform)
                    {
                        EventWriter.Inform(String.Format("Could not resolve the database key, selecting a connection string from configuration (key={0})",found.Name));
                    }
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
