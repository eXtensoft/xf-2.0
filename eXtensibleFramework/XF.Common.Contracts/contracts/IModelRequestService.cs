// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IModelRequestService
    {
        #region synchronous

        IResponse<T> Post<T>(T model) where T : class, new();

        IResponse<T> Put<T>(T model, ICriterion criterion) where T : class, new();

        IResponse<T> Delete<T>(ICriterion criterion) where T : class, new();

        IResponse<T> Get<T>(ICriterion criterion) where T : class, new();

        IResponse<T> GetAll<T>(ICriterion criterion) where T : class, new();

        IResponse<T> GetAllProjections<T>(ICriterion criterion) where T : class, new();

        IResponse<T,U> ExecuteAction<T, U>(T model, ICriterion criterion) where T : class, new();

        IResponse<T> ExecuteCommand<T>(DataSet ds, ICriterion criterion) where T : class, new();

        IResponse<T, U> ExecuteMany<T, U>(IEnumerable<T> list, ICriterion criterion) where T : class, new();

        #endregion

        #region asynchronous

        void PostAsync<T>(T model, Action<IResponse<T>> callback) where T : class, new();

        void PutAsync<T>(T model, ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void DeleteAsync<T>(ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void GetAsync<T>(ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void GetAllAsync<T>(ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void GetAllProjectionsAsync<T>(ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void ExecuteActionAsync<T, U>(T model, ICriterion criterion, Action<IResponse<T,U>> callback) where T : class, new();

        void ExecuteCommand<T>(DataSet ds, ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void ExecuteManyAsync<T, U>(IEnumerable<T> list, ICriterion criterion, Action<IResponse<T, U>> callback) where T : class, new();
        #endregion

    }
}
