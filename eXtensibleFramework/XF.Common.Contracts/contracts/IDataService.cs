// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Collections.Generic;
    using System.Data;

    public interface IDatastoreService
    {
        T Post<T>(T model, IRequestContext requestContext) where T : class, new();

        T Put<T>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new();

        ICriterion Delete<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new();

        T Get<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new();

        IEnumerable<T> GetAll<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new();

        IEnumerable<IProjection> GetAllProjections<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new();

        U ExecuteAction<T, U>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new();

        DataSet ExecuteCommand<T>(DataSet ds, ICriterion criterion, IRequestContext requestContext) where T : class, new();

        U ExecuteMany<T, U>(IEnumerable<T> list, ICriterion criterion, IRequestContext requestContext) where T : class, new();

    }
}
