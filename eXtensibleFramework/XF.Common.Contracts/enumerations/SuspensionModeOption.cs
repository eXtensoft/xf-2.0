// <copyright company="eXtensible Solutions, LLC" file="SuspensionModeOption.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
