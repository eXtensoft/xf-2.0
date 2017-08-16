// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{

    public interface IRpcHander<T> : ITypeMap where T : class, new()
    {
        IContext Context { get; set; }

        U Execute<U>(T t, ICriterion criterion, IContext context);
    }
}
