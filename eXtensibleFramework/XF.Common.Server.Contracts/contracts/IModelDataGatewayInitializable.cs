// <copyright company="eXtensible Solutions, LLC" file="IModelDataGatewayInitializer.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{

    public interface IModelDataGatewayInitializeable
    {
        void Initialize<T>(ModelActionOption option, IContext context, T t, ICriterion criterion, ResolveDbKey<T> dbkeyResolver) where T : class, new();
    }

    public delegate string ResolveDbKey<T>(IContext context);
}
