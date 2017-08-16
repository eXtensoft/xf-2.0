// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IModelService
    {
        #region synchronous

        T Post<T>(T model) where T : class, new();

        T Put<T>(T model, ICriterion criterion) where T : class, new();

        ICriterion Delete<T>(ICriterion criterion) where T : class, new();

        T Get<T>(ICriterion criterion) where T : class, new();

        IEnumerable<T> GetAll<T>(ICriterion criterion) where T : class, new();

        IEnumerable<IProjection> GetAllProjections<T>(ICriterion criterion) where T : class, new();

        U ExecuteAction<T, U>(T model, ICriterion criterion) where T : class, new();

        DataSet ExecuteCommand<T>(ICriterion criterion) where T : class, new();

        U ExecuteMany<T, U>(IEnumerable<T> list, ICriterion criterion) where T : class, new();

        #endregion

        #region asynchronous

        void PostAsync<T>(T model, Action<T> callback) where T : class, new();

        void PutAsync<T>(T model, ICriterion criterion, Action<T> callback) where T : class, new();

        void DeleteAsync<T>(ICriterion criterion, Action<T> callback) where T : class, new();

        void GetAsync<T>(ICriterion criterion, Action<T> callback) where T : class, new();

        void GetAllAsync<T>(ICriterion criterion, Action<IEnumerable<T>> callback) where T : class, new();

        void GetAllProjectionsAsync<T>(ICriterion criterion, Action<IEnumerable<IProjection>> callback) where T : class, new();

        void ExecuteActionAsync<T, U>(T model, ICriterion criterion, Action<U> callback) where T : class, new();

        void ExecuteCommand<T>(ICriterion criterion, Action<DataSet> callback) where T : class, new();

        void ExecuteManyAsync<T, U>(IEnumerable<T> list, ICriterion criterion, Action<U> callback) where T : class, new();

        #endregion

    }

}
