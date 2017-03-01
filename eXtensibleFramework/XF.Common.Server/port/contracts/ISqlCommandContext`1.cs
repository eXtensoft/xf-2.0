// <copyright company="eXtensible Solutions, LLC" file="ISqlCommandContext`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
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
