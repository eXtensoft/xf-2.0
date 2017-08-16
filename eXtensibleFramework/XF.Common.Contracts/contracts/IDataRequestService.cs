// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    public interface IDataRequestService
    {
        IResponse<T> Post<T>(IRequest<T> request) where T : class, new();

        IResponse<T> Put<T>(IRequest<T> request) where T : class, new();

        IResponse<T> Delete<T>(IRequest<T> request) where T : class, new();

        IResponse<T> Get<T>(IRequest<T> request) where T : class, new();

        IResponse<T> GetAll<T>(IRequest<T> request) where T : class, new();

        IResponse<T> GetAllProjections<T>(IRequest<T> request) where T : class, new();

        IResponse<T,U> ExecuteAction<T,U>(IRequest<T> request) where T : class, new();

        IResponse<T> ExecuteCommand<T>(IRequest<T> request) where T : class, new();

        IResponse<T, U> ExecuteMany<T, U>(IRequest<T> request) where T : class, new();

    }
}
