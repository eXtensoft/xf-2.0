// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
