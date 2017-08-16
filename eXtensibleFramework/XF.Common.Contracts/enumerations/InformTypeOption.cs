// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    public enum InformTypeOption 
    {
        None = 0,
        Initialize = 1,
        Cache = 2,
        Associate = 4,
        Resolve = 8,
        Execute = 16,
        Wait = 32,
        WebService = 64,
        ModelDataGateway = 128,
        Connection = 256,
        DataStore = 512,
        Finalize = 1024
        
    }
}
