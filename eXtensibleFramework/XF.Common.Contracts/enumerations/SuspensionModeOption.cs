// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    [Flags]
    public enum SuspensionModeOption
    {
        None = 0,
        FifteenMinutes = 1,
        HalfHour = 2,
        OneHour = 4,
        TwoHours = 8,
        FourHours = 16,
        EightHours = 32,
        TwelveHourse = 64,
        OneDay = 128
    }
}
