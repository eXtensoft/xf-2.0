// <copyright company="eXtensible Solutions, LLC" file="ICacheable`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public interface ICacheable<T> where T : class, new()
    {
        ICache Cache { set; }
    }
}
