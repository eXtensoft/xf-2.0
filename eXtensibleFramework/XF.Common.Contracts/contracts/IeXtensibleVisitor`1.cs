// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IeXtensibleVisitor<T> : IeXtensibleVisitor where T : class, new()
    {
        void Visit(T t);
    }

}
