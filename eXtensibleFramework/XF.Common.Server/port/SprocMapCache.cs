// <copyright company="eXtensible Solutions, LLC" file="SprocMapCache.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using XF.Common;

    public class SprocMapCache
    {
        public DateTime Start { get; set; }

        private IDictionary<string, int> _SprocFormatterMap =  new Dictionary<string, int>
        {
            {"demo.dbo",0},
            {"xf.arc",1},
            {"other.all",2},
        };

        private List<ISqlStoredProcedureFormatter> _SprocFormatters = new List<ISqlStoredProcedureFormatter>
        {
            {new DefaultSqlStoredProcedureFormatter()},
        };

        public static SprocMapCache Instance { get; set; }

        public Dictionary<string, List<XF.Common.Discovery.SqlTable>> Tables { get; set; }

        // <connectionstring,SqlStoredProcedure>
        public Dictionary<string, List<Discovery.SqlStoredProcedure>> StoredProcedures { get; private set; }

        #region constructors

        private SprocMapCache()
        {
            Start = DateTime.Now;
        }

        static SprocMapCache()
        {
            Instance = new SprocMapCache();
        }

        #endregion constructors

        public string Get<T>(IContext context, ICriterion criterion, ModelActionOption option, SqlConnection connection) where T : class, new()
        {
            string s = String.Empty;
            if (!IsCacheLoaded(connection))
            {
                LoadCache(connection);
            }
            //string hash = ComposeHash<T>("demo",criterion, option);
            string hash = ComposeHash<T>(context.ApplicationContextKey, criterion, option);

            List<Discovery.SqlStoredProcedure> list = StoredProcedures[connection.ConnectionString];
            if (list != null && list.Count > 0)
            {
                var found = list.FirstOrDefault(x =>
                {
                    return !String.IsNullOrEmpty(x.Hash) && x.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase);
                }
                    );
                if (found != null)
                {
                    s = String.Format("[{0}].[{1}]", found.Schema, found.Name);
                }
            }
            return s;
        }

        private bool TryGetTable(IContext context, SqlConnection connection, string modelName, out Discovery.SqlTable table)
        {
            bool b = false;
            table = null;
            string s = String.Empty;
            if (!IsCacheLoaded(connection))
            {
                LoadCache(connection);
            }
            if (Tables.ContainsKey(connection.ConnectionString))
            {
                List<XF.Common.Discovery.SqlTable> list = Tables[connection.ConnectionString];
                if (list != null && list.Count > 0)
                {
                    var found = list.Find(x => x.TableName.Equals(modelName, StringComparison.OrdinalIgnoreCase));
                    if (found != null)
                    {
                        table = found;
                        b = true;
                    }
                }
            }
            return b;
        }

        public bool TryGetStoredProcedure(IContext context, ICriterion criterion, ModelActionOption option, SqlConnection connection, string hash, out Discovery.SqlStoredProcedure storedProcedure)
        {
            bool b = false;
            storedProcedure = null;
            string s = String.Empty;
            if (!IsCacheLoaded(connection))
            {
                LoadCache(connection);
            }

            List<Discovery.SqlStoredProcedure> list = StoredProcedures[connection.ConnectionString];
            if (list != null && list.Count > 0)
            {
                Discovery.SqlStoredProcedure found = null;
                if (option == ModelActionOption.Post | option == ModelActionOption.Put)
                {
                    found = list.FirstOrDefault(x =>
                    {
                        bool b1 = false;
                        if (!String.IsNullOrEmpty(x.Hash) && x.Hash.Length >= hash.Length)
                        {
                            b1 = hash.Equals(x.Hash.Substring(0, hash.Length), StringComparison.OrdinalIgnoreCase);
                        }
                        return b1;
                    });
                }
                else
                {
                    found = list.FirstOrDefault(x =>
                    {
                        return !String.IsNullOrEmpty(x.Hash) && x.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase);
                    });
                }

                if (found != null)
                {
                    storedProcedure = found;
                    b = true;
                }
            }
            return b;
        }

        public bool TryGetStoredProcedure<T>(IContext context, ICriterion criterion, ModelActionOption option, SqlConnection connection, out Discovery.SqlStoredProcedure storedProcedure) where T : class, new()
        {
            string hash = ComposeHash<T>(context.ApplicationContextKey, criterion, option);
            return TryGetStoredProcedure(context,criterion,option,connection,hash,out storedProcedure );
        }

        public bool TryGetTable<T>(IContext context,SqlConnection connection, out XF.Common.Discovery.SqlTable table) where T : class, new()
        {
            return TryGetTable(context, connection, typeof(T).Name, out table); 
        }


        private string ComposeHash<T>(string applicationContext, ICriterion criteria, ModelActionOption option) where T : class, new()
        {
            string s = String.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(_SprocFormatters[0].ComposeFormat<T>(option));
            if (criteria != null && criteria.Items != null)
            {
                List<TypedItem> list = (criteria.Items.Where(x => !x.Key.Equals("UserIdentity", StringComparison.OrdinalIgnoreCase)
                    & !x.Key.Equals("action.modifier", StringComparison.OrdinalIgnoreCase)).OrderBy(x => x.Key)).ToList();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        sb.Append(String.Format("@{0}", item.Key));
                    }
                }
            }
            s = sb.ToString();
            return s;
        }

        private bool IsCacheLoaded(SqlConnection connection)
        {
            if (StoredProcedures == null || !StoredProcedures.ContainsKey(connection.ConnectionString))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void LoadCache(SqlConnection connection)
        {
            if (Tables == null)
            {
                Tables = new Dictionary<string,List<Discovery.SqlTable>>();
            }

            List<string> Schemas = new List<string>();
            int tableschemasize = 3;

            List<XF.Common.Discovery.SqlTable> tables = new List<Discovery.SqlTable>();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = DiscoveryResources.Discovery_Tables;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    tables.Add(new XF.Common.Discovery.SqlTable(reader));
                    while (reader.NextResult())
                    {
                        tables.Add(new XF.Common.Discovery.SqlTable(reader));
                    }
                }
            }
            foreach (var table in tables)
            {
                if (!Schemas.Contains(table.TableSchema))
                {
                    Schemas.Add(table.TableSchema);
                }
            }

            foreach (var item in tables)
            {
                int j = item.TableSchema.Length;
                if (j > tableschemasize)
                {
                    tableschemasize = j;
                }
            }

            Tables.Add(connection.ConnectionString,tables);


            if (StoredProcedures == null)
            {
                StoredProcedures = new Dictionary<string, List<Discovery.SqlStoredProcedure>>();
            }

            string sql = DiscoveryResources.Discovery_StoredProcedures;
            List<Discovery.SqlParameter> list = new List<Discovery.SqlParameter>();

            using (DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Discovery.SqlParameter(reader));
                    }
                }
            }


            Dictionary<string, List<Discovery.SqlParameter>> d = new Dictionary<string, List<Discovery.SqlParameter>>();
            foreach (var item in list)
            {
                if (!d.ContainsKey(item.StoredProcedureName))
                {
                    d.Add(item.StoredProcedureName, new List<Discovery.SqlParameter>());
                }
                d[item.StoredProcedureName].Add(item);
            }

            List<Discovery.SqlStoredProcedure> l = new List<Discovery.SqlStoredProcedure>();
            foreach (var item in d)
            {
                Discovery.SqlStoredProcedure sproc = new Discovery.SqlStoredProcedure(item.Value);
                if (sproc.IsModelAction)
                {
                    l.Add(sproc);
                }
            }
            StoredProcedures.Add(connection.ConnectionString, l);
        }
    }
}