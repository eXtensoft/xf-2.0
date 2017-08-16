// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Collections.Generic;
    using System.Data;

    public interface IModelDataGateway<T> : ITypeMap where T : class, new()
    {
        IDatastoreService DataService { get; set; }

        IContext Context { get; set; }

        T Post(T t, IContext context);

        T Get(ICriterion criterion, IContext context);

        T Put(T t, ICriterion criterion, IContext context);

        ICriterion Delete(ICriterion criterion, IContext context);

        IEnumerable<T> GetAll(ICriterion criterion, IContext context);

        IEnumerable<Projection> GetAllProjections(ICriterion criterion, IContext context);

        U ExecuteAction<U>(T t, ICriterion criterion, IContext context);

        DataSet ExecuteCommand(DataSet ds, ICriterion criterion, IContext context);

        U ExecuteMany<U>(IEnumerable<T> list, ICriterion criterion, IContext context);

    }
}
