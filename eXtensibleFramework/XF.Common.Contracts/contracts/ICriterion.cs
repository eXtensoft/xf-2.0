// <copyright company="eXtensible Solutions, LLC" file="ICriterion.cs">
// Copyright © 2015 All Right Reserved
// </copyright>


namespace XF.Common
{
    using System.Collections.Generic;

    public interface ICriterion
    {
        T GetValue<T>(string key);

        IEnumerable<TypedItem> Items { get; }
    }
}
