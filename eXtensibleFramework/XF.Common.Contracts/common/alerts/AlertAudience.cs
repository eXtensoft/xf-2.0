// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace XF.Common
{
    using System;

    [FlagsAttribute]
    public enum AlertAudiences
    {
        None = 0,
        Developer = 1,
        DBA = 2,
        Network = 4,
        Operations = 8,
        Management = 16,
        CTO = 32,
        Business = 64,
    }

}
