// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{

    public interface IRpcDataRequestService
    {
        IResponse<T, U> ExecuteRpc<T, U>(IRequest<T> request) where T : class, new();
    }
}
