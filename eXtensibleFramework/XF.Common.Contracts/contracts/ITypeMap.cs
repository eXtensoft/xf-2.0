// <copyright company="eXtensible Solutions, LLC" file="ITypeMap.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public interface ITypeMap
    {
        string Domain { get; }

        Type KeyType { get; }

        Type TypeResolution { get; }
    }
}
