// <copyright company="eXtensible Solutions, LLC" file="IDataRequestService.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
