// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{

    public interface IModelDataGatewayInitializeable
    {
        void Initialize<T>(ModelActionOption option, IContext context, T t, ICriterion criterion, ResolveDbKey<T> dbkeyResolver) where T : class, new();
    }

    public delegate string ResolveDbKey<T>(IContext context);
}
