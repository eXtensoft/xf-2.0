// <copyright company="eXtensible Solutions, LLC" file="IStrategyResolver.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public interface IStrategyResolver
    {
        string Resolve(params string[] args);
    }
}
