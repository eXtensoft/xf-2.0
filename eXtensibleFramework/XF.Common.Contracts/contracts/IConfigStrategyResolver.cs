// <copyright company="eXtensible Solutions LLC" file="IConfigStrategyResolver.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{

    public interface IConfigStrategyResolver 
    {
        void Initialize(eXtensibleStrategySectionGroup sectionGroup);

        string Resolve<T>(IContext context) where T : class, new();

        bool IsInitialized { get; }

    }
}
