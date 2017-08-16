// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.IO;

    public static class SqlResolver
    {
        private static XF.Common.Db.DbConfigCollection Configs = null;

        static SqlResolver()
        {
            List<XF.Common.Db.DbConfig> list = new List<Db.DbConfig>();

            string[] filepaths = null;
            // look for dbConfigs in special folder
            if (Directory.Exists(eXtensibleConfig.DbConfigs))
            {
                filepaths = Directory.GetFiles(eXtensibleConfig.DbConfigs, "*.dbconfig.xml", SearchOption.AllDirectories);
            }
            if (filepaths == null)
            {
                filepaths = Directory.GetFiles(eXtensibleConfig.BaseDirectory, "$.dbconfig.xml", SearchOption.TopDirectoryOnly);
            }
            if (filepaths != null && filepaths.Length > 0)
            {
                foreach (var filepath in filepaths)
                {
                    if (File.Exists(filepath))
                    {
                        try
                        {
                            var config = GenericSerializer.ReadGenericItem<XF.Common.Db.DbConfig>(filepath);
                            list.Add(config);
                        }
                        catch(Exception ex)
                        {
                            EventWriter.WriteError(ex.Message, SeverityType.Error, "DataAccess", eXtensibleConfig.GetProperties());
                            EventWriter.Inform(String.Format("Unable to load: ",filepath));
                        }
                    }
                }
            }
            Configs = new Db.DbConfigCollection();
            foreach (var item in list)
            {
                Configs.Add(item);
            }
        }
        public static SqlConnection GetConnection(string key = "")
        {
            string s = GetConnectionString(key);
            return new SqlConnection(s);
        }

        public static string GetConnectionString(string key)
        {
            string s = (!String.IsNullOrEmpty(key)) ? key : "default";
            ConnectionStringSettings settings = ConfigurationProvider.ConnectionStrings[s];
            return (settings != null) ? settings.ConnectionString : String.Empty;
        }

        public static SqlConnection ResolveConnection<T>(T t)
        {
            return GetConnection("demo");
        }

        public static SqlConnection ResolveConnection<T>(ICriterion criterion)
        {
            return GetConnection("demo");
        }

        public static SqlConnection ResolveConnection<T>()
        {
            return GetConnection("demo");
        }

        public static SqlCommand ResolvePostCommand<T>(SqlConnection cn, string schema, T t) where T : class, new()
        {
            return ResolvePostCommand<T>(cn, schema, t, null);
        }

        public static SqlCommand ResolvePostCommand<T>(SqlConnection cn, string schema, T t, List<DataMap> dataMaps) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.Post, cn, schema, t, dataMaps);
        }

        public static SqlCommand ResolvePostCommand<T>(IContext ctx, SqlConnection cn, string schema, T t, List<DataMap> dataMaps) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(ctx, ModelActionOption.Post, cn, schema, t, dataMaps);
        }

        #region GET command
        public static SqlCommand ResolveGetCommand<T>(SqlConnection cn, string schema, ICriterion criterion, IContext context) where T : class, new()
        {
            // if no config

            return ResolveGetCommand<T>(cn, schema, criterion);
        }
        public static SqlCommand ResolveGetCommand<T>(SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {


            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.Get, cn, schema, criterion);
            //string model = Activator.CreateInstance<T>().GetType().Name;
            //string sprocname = String.Format("[{0}].[{1}_{2}]", schema, model, ModelActionOption.Read.ToString());
            //return CreateCommand(cn, sprocname);
        }

        public static SqlCommand ResolveGetCommand<T>(IContext ctx, SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(ctx, ModelActionOption.Get, cn, schema, criterion);
        }


        #endregion

        public static SqlCommand ResolvePutCommand<T>(SqlConnection cn, string schema, T t) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.Put, cn, schema,t,null);
        }

        public static SqlCommand ResolvePutCommand<T>(IContext ctx, SqlConnection cn, string schema, T t) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(ctx, ModelActionOption.Put, cn, schema, t, null);
        }


        public static SqlCommand ResolveDeleteCommand<T>(SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.Delete, cn, schema, criterion);
        }

        public static SqlCommand ResolveDeleteCommand<T>(IContext ctx, SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(ctx, ModelActionOption.Delete, cn, schema, criterion);
        }


        public static SqlCommand ResolveGetAllCommand<T>(SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.GetAll, cn, schema, criterion);
        }

        public static SqlCommand ResolveGetAllCommand<T>(IContext ctx, SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(ctx, ModelActionOption.GetAll, cn, schema, criterion);
        }

        public static SqlCommand ResolveGetAllProjectionsCommand<T>(SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.GetAllProjections, cn, schema, criterion);
            //string model = Activator.CreateInstance<T>().GetType().Name;
            //string sprocname = String.Format("[{0}].[{1}s_{2}]", schema, model, ModelActionOption.ReadListDisplay.ToString());
            //return CreateCommand(cn, sprocname);
        }

        public static SqlCommand ResolveGetAllProjectionsCommand<T>(IContext ctx, SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(ctx, ModelActionOption.GetAllProjections, cn, schema, criterion);
            //string model = Activator.CreateInstance<T>().GetType().Name;
            //string sprocname = String.Format("[{0}].[{1}s_{2}]", schema, model, ModelActionOption.ReadListDisplay.ToString());
            //return CreateCommand(cn, sprocname);
        }

        private static SqlCommand CreateCommand(SqlConnection cn, string storedProcedureName)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureName;
            return cmd;
        }
    }
}
