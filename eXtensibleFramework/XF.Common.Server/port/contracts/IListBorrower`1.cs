// <copyright company="eXtensible Solutions, LLC" file="IListBorrower`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;

    public interface IListBorrower<T> : IBorrower<List<T>>
    {
    }
}
