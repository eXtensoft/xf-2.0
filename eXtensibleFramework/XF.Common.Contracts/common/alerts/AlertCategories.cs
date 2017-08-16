// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    [FlagsAttribute]
    public enum AlertCategories
    {
        None = 0,
        Processing = 1,
        Connectivity = 2,
        Resources = 4,
        Database = 8,
        Web = 16,
        API = 32,
        WebService = 64,
        Ftp = 128,
        Authentication = 256,
        Authorization = 512,
    }
}
