// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using MySql.Data.MySqlClient;
    using System.Data;

    public interface IMySqlCommandContext<T> where T : class, new()
    {
        MySqlCommand PostSqlCommand(MySqlConnection cn, T t, IContext context);

        MySqlCommand GetSqlCommand(MySqlConnection cn, ICriterion criterion, IContext context);

        MySqlCommand PutSqlCommand(MySqlConnection cn, T t, ICriterion criterion, IContext context);

        MySqlCommand DeleteSqlCommand(MySqlConnection cn, ICriterion criterion, IContext context);

        MySqlCommand GetAllSqlCommand(MySqlConnection cn, ICriterion criterion, IContext context);

        MySqlCommand GetAllProjectionsSqlCommand(MySqlConnection cn, ICriterion criterion, IContext context);

        MySqlCommand ExecuteCommandSqlCommand(MySqlConnection cn, DataSet dataSet, ICriterion criterion, IContext context);

        string Schema { get; }
    }
}