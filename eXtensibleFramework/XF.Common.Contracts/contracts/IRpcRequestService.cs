// <copyright company="eXtensible Solutions LLC" file="IRpcRequestService.cs">
// Copyright © 2015 All Right Reserved
// </copyright>



namespace XF.Common
{
    using System;

    public interface IRpcRequestService
    {

        IResponse<T, U> ExecuteRpc<T, U>(T model, ICriterion criterion) where T : class, new();

        void ExecuteRpcAsync<T, U>(T model, ICriterion criterion, Action<IResponse<T, U>> callback) where T : class, new();

    }

    
}
