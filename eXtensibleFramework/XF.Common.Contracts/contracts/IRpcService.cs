// <copyright company="eXtensible Solutions LLC" file="IRpcService.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public interface IRpcService
    {

        U ExecuteRpc<T, U>(T model, ICriterion criterion) where T : class, new();

        void ExecuteRpcAsync<T, U>(T model, ICriterion criterion, Action<U> callback) where T : class, new();

    }
}
