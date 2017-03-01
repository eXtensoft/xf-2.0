// <copyright company="eXtensible Solutions, LLC" file="IContext.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;

    public interface IContext
    {
        string ApplicationContextKey { get; }
        string Zone { get; }
        string UserIdentity { get; }
        IEnumerable<string> Claims { get; }
        string UICulture { get; }
        IEnumerable<TypedItem> TypedItems { get; }

        T GetValue<T>(string key);
        void SetError(int errorCode, string errorMessage);
        void SetStacktrace(string stackTrace);
        void Set<T>(T t);

    
    }
}
