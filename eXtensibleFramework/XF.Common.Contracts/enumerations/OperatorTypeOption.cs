// <copyright company="eXtensible Solutions, LLC" file="OperatorTypeOption.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    [Flags]
    public enum OperatorTypeOption
    {
        None = 0,
        EqualTo = 1,
        NotEqualTo = 2,
        LessThan = 4,
        GreaterThan = 8,
        X = 16,
        And = 32,
        Or = 64,
    }

}
