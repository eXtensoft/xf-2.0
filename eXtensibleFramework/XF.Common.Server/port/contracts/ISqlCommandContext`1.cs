// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Data;
    using System.Data.SqlClient;

    public interface ISqlCommandContext<T> where T : class, new()
    {
        SqlCommand PostSqlCommand(SqlConnection cn, T t, IContext context);

        SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context);

        SqlCommand PutSqlCommand(SqlConnection cn, T t, ICriterion criterion, IContext context);

        SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context);

        SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context);

        SqlCommand GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context);

        SqlCommand ExecuteCommandSqlCommand(SqlConnection cn, DataSet dataSet, ICriterion criterion, IContext context);

        string Schema { get; }
    }
}
