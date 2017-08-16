// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Serialization;
    using XF.Common;

    [Serializable]
    [XmlRoot("SqlContext")]
    public class SqlServerContext
    {
        [XmlAttribute("context")]
        public string Context { get; set; }

        [XmlAttribute("defaultStrategy")]
        public StrategyOption DefaultStrategy { get; set; }

        [XmlAttribute("defaultDbSchema")]
        public string DefaultDbSchema { get; set; }

        public List<Model> Models { get; set; }

        public SqlServerContext()
        {
            Models = new List<Model>();
        }

        public StrategyOption ResolveStrategy<T>(ModelActionOption option, out string implementorType)
        {
            implementorType = String.Empty;
            StrategyOption strategy = StrategyOption.None;
            Model m = Models.Find(x => x.modelType.Equals(typeof(T).FullName, StringComparison.OrdinalIgnoreCase));
            if (m != null)
            {
                if (option == ModelActionOption.ExecuteAction)
                {
                    if (String.IsNullOrEmpty(m.actionExecutorType))
                    {
                        // log
                    }
                    else
                    {
                        implementorType = m.actionExecutorType;
                    }
                }
                else
                {
                    ModelAction a = m.ModelActions.Find(x => x.Verb.Equals(option));
                    if (a != null)
                    {
                        strategy = a.Strategy;
                        switch (a.Strategy)
                        {
                            case StrategyOption.Factory:
                                implementorType = m.factoryType;
                                break;

                            case StrategyOption.ActionExecutor:
                                implementorType = m.actionExecutorType;
                                break;

                            case StrategyOption.Custom:
                                implementorType = m.customType;
                                break;

                            case StrategyOption.None:
                            case StrategyOption.Config:
                            default:
                                break;
                        }
                    }
                    else
                    {
                        strategy = DefaultStrategy;
                        if (strategy == StrategyOption.ActionExecutor)
                        {
                            implementorType = m.actionExecutorType;
                        }
                        else if (strategy == StrategyOption.Factory)
                        {
                            implementorType = m.factoryType;
                        }
                        else if (strategy == StrategyOption.Custom)
                        {
                            implementorType = m.customType;
                        }
                    }
                }
            }
            else
            {
                strategy = DefaultStrategy;
            }

            if (option == ModelActionOption.ExecuteAction)
            {
                strategy = StrategyOption.ActionExecutor;
            }
            return strategy;
        }

        //public DbCommand ResolveCommand<T>(DbCommandResolver resolver, IContext context, ModelActionOption option, ICriterion criteria) where T : new()
        //{
        //    DbCommand cmd = null;
        //    Model m = Models.Find(x => x.modelType.Equals(typeof(T).FullName, StringComparison.OrdinalIgnoreCase));

        //    string schema = (m != null && !String.IsNullOrEmpty(m.dbSchema)) ? m.dbSchema : (!String.IsNullOrEmpty(DefaultDbSchema)) ? DefaultDbSchema : "dbo";

        //    if (m != null)
        //    {
        //        ModelAction a = m.ModelActions.Find(x => x.Verb.Equals(option));
        //        if (a != null)
        //        {
        //            if (a.Strategy == StrategyOption.None)
        //            {
        //            }
        //            else if (a.Strategy == StrategyOption.Config)
        //            {
        //                string cmdKey = String.Empty;
        //                string switchKey = String.Empty;
        //                string caseKey = String.Empty;
        //                Switch found = null;
        //                if (!String.IsNullOrEmpty(a.SqlCommandKey))
        //                {
        //                    cmdKey = a.SqlCommandKey;
        //                }
        //                else if (criteria == null)
        //                {
        //                    found = a.Switches.Find(x => x.CriteriaKey.Equals(XAFConstants.SYSTEMNULL, StringComparison.OrdinalIgnoreCase));
        //                    if (found != null && found.Cases != null && found.Cases.Count >= 1)
        //                    {
        //                        cmdKey = found.Cases[0].SqlCommandKey;
        //                    }
        //                }
        //                else
        //                {
        //                    switchKey = criteria.GetValue<string>(XAFConstants.STRATEGYKEY);
        //                    found = a.Switches.Find(x => x.CriteriaKey.Equals(switchKey, StringComparison.OrdinalIgnoreCase));
        //                    caseKey = criteria.GetValue<string>(switchKey);
        //                }

        //                if (found != null && found.Cases != null && m.SqlCommands != null)
        //                {
        //                    if (found.Cases.Count == 1)
        //                    {
        //                        cmdKey = found.Cases[0].SqlCommandKey;
        //                    }
        //                    else
        //                    {
        //                        Case obj = found.Cases.FirstOrDefault(x => x.Value.Equals(caseKey, StringComparison.OrdinalIgnoreCase));
        //                        if (obj != null && !String.IsNullOrEmpty(obj.SqlCommandKey))
        //                        {
        //                            cmdKey = obj.SqlCommandKey;
        //                        }
        //                    }
        //                }
        //                SQLCommand sqlcmd = m.SqlCommands.Find(x => x.Key.Equals(cmdKey, StringComparison.OrdinalIgnoreCase));
        //                if (sqlcmd != null)
        //                {
        //                    cmd = resolver.Resolve<T>(context, sqlcmd, criteria, schema);
        //                }
        //                else
        //                {
        //                    //Logger.Write(message, new string[] { Constants.Category.### }, -1, -1, TraceEventTypeOption.Warning, String.Empty, extendedproperties);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            cmd = resolver.Resolve<T>(context, option, criteria, schema);
        //        }
        //    }
        //    else // Model not found in config, so try Convention over Configuration
        //    {
        //        cmd = resolver.Resolve<T>(context, criteria, option, schema);
        //    }
        //    if (cmd == null)
        //    {
        //        cmd = resolver.Resolve<T>(context, criteria, option, schema);
        //    }

        //    return cmd;
        //}

        internal SqlCommand ResolveCommand<T>(IContext context, ModelActionOption option, SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            SqlCommand cmd = null;
            Model m = Models.Find(x => x.modelType.Equals(typeof(T).FullName, StringComparison.OrdinalIgnoreCase));
            string dbSchema = (m != null && !String.IsNullOrEmpty(m.dbSchema)) ? m.dbSchema : (!String.IsNullOrEmpty(DefaultDbSchema)) ? DefaultDbSchema : schema;
            if (m != null)
            {
                ModelAction a = m.ModelActions.Find(x => x.Verb.Equals(option));
                if (a != null)
                {
                    if (a.Strategy == StrategyOption.None)
                    {
                    }
                    else if (a.Strategy == StrategyOption.Config)
                    {
                        string cmdKey = String.Empty;
                        string switchKey = String.Empty;
                        string caseKey = String.Empty;
                        Switch found = null;
                        if (!String.IsNullOrEmpty(a.SqlCommandKey))
                        {
                            cmdKey = a.SqlCommandKey;
                        }
                        else if (criterion == null)
                        {
                            found = a.Switches.Find(x => x.CriteriaKey.Equals(XFConstants.SystemNull, StringComparison.OrdinalIgnoreCase));
                            if (found != null && found.Cases != null && found.Cases.Count >= 1)
                            {
                                cmdKey = found.Cases[0].SqlCommandKey;
                            }
                        }
                        else
                        {
                            switchKey = criterion.GetValue<string>(XFConstants.StrategyKey);
                            found = a.Switches.Find(x => x.CriteriaKey.Equals(switchKey, StringComparison.OrdinalIgnoreCase));
                            caseKey = criterion.GetValue<string>(switchKey);
                        }

                        if (found != null && found.Cases != null && m.SqlCommands != null)
                        {
                            if (found.Cases.Count == 1)
                            {
                                cmdKey = found.Cases[0].SqlCommandKey;
                            }
                            else
                            {
                                Case obj = found.Cases.FirstOrDefault(x => x.Value.Equals(caseKey, StringComparison.OrdinalIgnoreCase));
                                if (obj != null && !String.IsNullOrEmpty(obj.SqlCommandKey))
                                {
                                    cmdKey = obj.SqlCommandKey;
                                }
                            }
                        }
                        SQLCommand sqlcmd = m.SqlCommands.Find(x => x.Key.Equals(cmdKey, StringComparison.OrdinalIgnoreCase));
                        if (sqlcmd != null)
                        {
                            //cmd = resolver.Resolve<T>(context, sqlcmd, criteria, schema);
                        }
                        else
                        {
                            //Logger.Write(message, new string[] { Constants.Category.### }, -1, -1, TraceEventTypeOption.Warning, String.Empty, extendedproperties);
                        }
                    }
                }
            }
            else // Model not found in config, so try Convention over Configuration (sproc based)
            {
                //string sprocname = GetStoredProcedure<T>(context, criterion, option, cn);
                //cmd = CreateCommand(cn, sprocname);
                // now append any appropriate parameters
                cmd = CreateCommand<T>(context, option, cn, criterion);
            }

            if (cmd == null) // dynamic sql based
            {
            }

            return cmd;
        }

        internal SqlCommand ResolveCommand<T>(IContext context, ModelActionOption option, SqlConnection cn, string schema, T t, List<DataMap> dataMaps) where T : class, new()
        {
            SqlCommand cmd = null;
            Model m = Models.Find(x => x.modelType.Equals(typeof(T).FullName, StringComparison.OrdinalIgnoreCase));
            string dbSchema = (m != null && !String.IsNullOrEmpty(m.dbSchema)) ? m.dbSchema : (!String.IsNullOrEmpty(DefaultDbSchema)) ? DefaultDbSchema : schema;
            if (m != null)
            {
                ModelAction a = m.ModelActions.Find(x => x.Verb.Equals(option));
                if (a != null)
                {
                    if (a.Strategy == StrategyOption.None)
                    {
                    }
                    else if (a.Strategy == StrategyOption.Config)
                    {
                        string cmdKey = String.Empty;

                        if (!String.IsNullOrEmpty(a.SqlCommandKey))
                        {
                            cmdKey = a.SqlCommandKey;
                        }

                        SQLCommand sqlcmd = m.SqlCommands.Find(x => x.Key.Equals(cmdKey, StringComparison.OrdinalIgnoreCase));
                        if (sqlcmd != null)
                        {
                            //cmd = resolver.Resolve<T>(context, sqlcmd, criteria, schema);
                        }
                        else
                        {
                            //Logger.Write(message, new string[] { Constants.Category.### }, -1, -1, TraceEventTypeOption.Warning, String.Empty, extendedproperties);
                        }
                    }
                }
            }
            else // Model not found in config, so try Convention over Configuration (sproc based)
            {
                //string sprocname = GetStoredProcedure<T>(context, criterion, option, cn);
                //cmd = CreateCommand(cn, sprocname);
                // now append any appropriate parameters
                cmd = CreateCommand<T>(context, option, cn, t, dataMaps);
            }

            if (cmd == null) // dynamic sql based
            {
            }

            return cmd;
        }

        private string GetStoredProcedure<T>(IContext context, ICriterion criteria, ModelActionOption option, SqlConnection connection) where T : class, new()
        {
            return SprocMapCache.Instance.Get<T>(context, criteria, option, connection);
        }

        private SqlCommand CreateCommand<T>(IContext context, ModelActionOption option, SqlConnection connection, ICriterion criteria) where T : class, new()
        {
            SqlCommand cmd = null;
            Discovery.SqlStoredProcedure sproc;
            if (SprocMapCache.Instance.TryGetStoredProcedure<T>(context, criteria, option, connection, out sproc))
            {
                cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = String.Format("[{0}].[{1}]", sproc.Schema, sproc.Name);

                if (criteria != null && sproc.Parameters != null && sproc.Parameters.Count > 0)
                {
                    List<PropertyInfo> list = new List<PropertyInfo>(typeof(T).GetProperties());
                    foreach (var item in sproc.Parameters)
                    {
                        string paramName = item.ParamName.Replace("@", String.Empty);
                        SqlParameter sqlparam = cmd.CreateParameter();
                        sqlparam.ParameterName = paramName;

                        //sqlparam.DbType = (DbType)Enum.Parse(typeof(DbType), item.Datatype, true);

                        if (paramName.Equals(XFConstants.Application.UserIdentityParamName, StringComparison.OrdinalIgnoreCase))
                        {
                            string useridentityvalue = context.GetValue<string>(XFConstants.Application.UserIdentityParamName);
                            if (!String.IsNullOrEmpty(useridentityvalue))
                            {
                                sqlparam.Value = useridentityvalue;
                            }
                        }
                        else
                        {
                            if (criteria.Contains(paramName))
                            {
                                TypedItem found = criteria.Items.First(x => x.Key.Equals(paramName, StringComparison.OrdinalIgnoreCase));
                                if (found != null)
                                {
                                    if (item.Datatype == "DateTime" && found.Value.GetType().Equals(typeof(DateTime)))
                                    {
                                        DateTime target = (DateTime)found.Value;
                                        if (target >= new DateTime(1753, 1, 1))
                                        {
                                            sqlparam.Value = found.Value;
                                        }
                                    }
                                    else
                                    {
                                        sqlparam.Value = found.Value;
                                    }
                                }
                            }
                        }
                        cmd.Parameters.Add(sqlparam);
                    }
                }
            }

            return cmd;
        }

        private SqlCommand CreateCommand<T>(IContext context, ModelActionOption option, SqlConnection connection, T t, List<DataMap> dataMaps) where T : class, new()
        {
            SqlCommand cmd = null;
            Discovery.SqlStoredProcedure sproc;
            if (SprocMapCache.Instance.TryGetStoredProcedure<T>(context, null, option, connection, out sproc))
            {
                cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = String.Format("[{0}].[{1}]", sproc.Schema, sproc.Name);

                if (sproc.Parameters != null && sproc.Parameters.Count > 0)
                {
                    List<PropertyInfo> list = new List<PropertyInfo>(typeof(T).GetProperties());

                    //foreach (DbParameter item in cmd.Parameters)
                    foreach (var item in sproc.Parameters)
                    {
                        string paramName = item.ParamName.Replace("@", String.Empty);
                        SqlParameter sqlparam = cmd.CreateParameter();
                        sqlparam.ParameterName = paramName;

                        //sqlparam.DbType = (DbType)Enum.Parse(typeof(DbType),item.Datatype,true);

                        if (paramName.Equals(XFConstants.Application.UserIdentityParamName, StringComparison.OrdinalIgnoreCase) && context != null)
                        {
                            string useridentityvalue = context.GetValue<string>(XFConstants.Application.UserIdentityParamName);
                            if (!String.IsNullOrEmpty(useridentityvalue))
                            {
                                sqlparam.Value = useridentityvalue;
                            }
                        }
                        else
                        {
                            string s = paramName;
                            if (dataMaps != null)
                            {
                                var found = dataMaps.Find(x => x.ColumnName.Equals(paramName, StringComparison.OrdinalIgnoreCase));
                                s = (found != null) ? found.PropertyName : paramName;
                            }

                            PropertyInfo info = list.Find(x => x.CanRead == true && x.Name.Equals(s, StringComparison.OrdinalIgnoreCase));
                            if (info != null)
                            {
                                if (info.PropertyType.Equals(typeof(DateTime)))
                                {
                                    DateTime target = (DateTime)(object)info.GetValue(t, null);
                                    if (target >= new DateTime(1753, 1, 1))
                                    {
                                        sqlparam.Value = info.GetValue(t, null);
                                    }
                                }
                                else
                                {
                                    sqlparam.Value = info.GetValue(t, null);
                                }
                            }
                        }
                        cmd.Parameters.Add(sqlparam);
                    }
                }
            }
            return cmd;
        }
    }
}