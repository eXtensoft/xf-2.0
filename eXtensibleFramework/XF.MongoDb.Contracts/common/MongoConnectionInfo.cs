// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using MongoDB.Driver;
    using System;
    using System.Configuration;

    public class MongoConnectionInfo
    {
        public string Name { get; private set; }

        public string Provider { get; private set; }

        public string ConnectionString { get; private set; }

        public string Server { get; private set; }

        public string Database { get; private set; }
        
        public bool IsInitialized { get; set; }

        public bool IsReplicaSet { get; set; }

        public bool Initialize(string connectionKey)
        {
            bool b = false;
            ConnectionStringSettings settings = ConfigurationProvider.ConnectionStrings[connectionKey];
            if (settings != null)
            {
                b = Initialize(settings);
            }
            return b;
        }

        public bool Initialize(ConnectionStringSettings settings)
        {
            bool b =  true;
            Name = settings.Name;
            Provider = settings.ProviderName;
            ConnectionString = settings.ConnectionString;
            if (settings.ConnectionString.Contains(","))
            {
                IsInitialized = IsReplicaSet = true;                
            }
            else
            {
                string[] t = settings.ConnectionString.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (t.Length == 3)
                {
                    Database = t[2]; 
                }
                else
                {
                    b = false;
                }
                IsInitialized = b;
            }
            return IsInitialized;
        }

        public MongoDatabase GetDatabase()
        {
            var client = GetClient();
            var server = client.GetServer();
            var database = server.GetDatabase(Database);
            return database;
        }

        public MongoClient GetClient()
        {
            return IsInitialized ? new MongoClient(ConnectionString) : new MongoClient();
        }

        public MongoCollection GetCollection<T>() where T : class, new()
        {
            var db = GetDatabase();
            string key = GetCollectionKey<T>();
            return db.GetCollection<T>(key);
        }


        private static string GetCollectionKey<T>()
        {
            return GetModelType<T>().Name.ToLower();
        }
        private static Type GetModelType<T>()
        {
            return Activator.CreateInstance<T>().GetType();
        }
        
    }
}
