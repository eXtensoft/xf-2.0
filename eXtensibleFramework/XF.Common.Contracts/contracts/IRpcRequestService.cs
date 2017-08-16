﻿// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    public interface IRpcRequestService
    {

        IResponse<T, U> ExecuteRpc<T, U>(T model, ICriterion criterion) where T : class, new();

        void ExecuteRpcAsync<T, U>(T model, ICriterion criterion, Action<IResponse<T, U>> callback) where T : class, new();

    }

    
}
