// <copyright company="eXtensible Solutions, LLC" file="SqlServerConfigManager.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class SqlServerConfigManager
    {
        public DateTime Start { get; set; }

        public static SqlServerConfigManager Instance { get; set; }

        #region Contexts (List<SqlServerContext>)

        private List<SqlServerContext> _Contexts = new List<SqlServerContext>();

        /// <summary>
        /// Gets or sets the List<SqlServerContext> value for Contexts
        /// </summary>
        /// <value> The List<SqlServerContext> value.</value>

        public List<SqlServerContext> Contexts
        {
            get { return _Contexts; }
            set
            {
                if (_Contexts != value)
                {
                    _Contexts = value;
                }
            }
        }

        #endregion Contexts (List<SqlServerContext>)

        static SqlServerConfigManager()
        {
            Instance = new SqlServerConfigManager();
        }

        private SqlServerConfigManager()
        {
            Start = DateTime.Now;
            GenerateDefault();
        }

        public SqlCommand ResolveCommand<T>(IContext context, ModelActionOption option, SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            SqlCommand cmd = null;
            string contextKey = (context != null) ? context.ApplicationContextKey : XFConstants.Context.DefaultApplication;
            var found = Contexts.Find(x => x.Context.Equals(contextKey, StringComparison.OrdinalIgnoreCase));
            if (found == null)
            {
            }
            else
            {
                cmd = found.ResolveCommand<T>(context, option, cn, schema, criterion);
            }

            return cmd;
        }

        public SqlCommand ResolveCommand<T>(IContext context, ModelActionOption option, SqlConnection cn, string schema, T t, List<DataMap> dataMaps) where T : class, new()
        {
            SqlCommand cmd = null;
            string contextKey = (context != null) ? context.ApplicationContextKey : XFConstants.Context.DefaultApplication;
            var found = Contexts.Find(x => x.Context.Equals(contextKey, StringComparison.OrdinalIgnoreCase));
            if (found == null)
            {
            }
            else
            {
                cmd = found.ResolveCommand<T>(context, option, cn, schema, t, dataMaps);
            }

            return cmd;
        }

        private void GenerateDefault()
        {
            SqlServerContext context = new SqlServerContext() { Context = XFConstants.Context.DefaultApplication, DefaultStrategy = StrategyOption.Config };
            Contexts.Add(context);
        }
    }
}
