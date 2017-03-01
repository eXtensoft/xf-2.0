// <copyright company="eXtensible Solutions, LLC" file="IRequestContext.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{


    public interface IRequestContext : IContext
    {
        string InstanceIdentifier { get; }
        void SetMetric(string scope, string key, object value);
        bool HasError();
    }

}
