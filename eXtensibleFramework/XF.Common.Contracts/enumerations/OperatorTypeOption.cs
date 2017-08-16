// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
