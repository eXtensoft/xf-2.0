// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Quality
{
    using System;

    [Flags]
    public enum DirectionOption
    {
        None = 0,
        Input = 1,
        Output = 2,
        Acquire = 4,
        Find = 8,
        Selector = 16
    }
}
