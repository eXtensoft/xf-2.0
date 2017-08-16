// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{

    public interface IResponse<T,U> : IResponse<T> where T : class, new()
    {
        U ActionResult { get; set; }
    }
}
