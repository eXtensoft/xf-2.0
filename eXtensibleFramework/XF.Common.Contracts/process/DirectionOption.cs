

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
