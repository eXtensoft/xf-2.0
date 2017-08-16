// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public class SqlConnectionProvider 
    {

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
    }
}
