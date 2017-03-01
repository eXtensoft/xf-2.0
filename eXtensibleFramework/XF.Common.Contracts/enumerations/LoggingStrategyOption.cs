// <copyright company="eXtensible Solutions, LLC" file="LoggingStrategyOption.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public enum LoggingStrategyOption
    {
        None,
        Output,
        Silent,
        XFTool,
        CommonServices,
        WindowsEventLog,
        Datastore,
        Plugin,
    }
}
