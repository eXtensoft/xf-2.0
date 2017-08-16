// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    public interface ICacheable<T> where T : class, new()
    {
        ICache Cache { set; }
    }
}
