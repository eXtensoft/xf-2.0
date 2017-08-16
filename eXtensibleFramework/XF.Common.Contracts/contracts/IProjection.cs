// <copyright company="eXtensible Solutions, LLC" file="IProjection.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;


    public interface IProjection
    {
        string Id { get; }
        string Display { get; }
        string DisplayAlt { get; }
        string Uri { get; }
        string Typename { get; }
        string Group { get; set; }
        string Status { get; set; }
        int IntVal { get; set; }
        bool IsSelected { get; set; }
        string MasterId { get; set; }
        IEnumerable<TypedItem> Items { get; }

    }
}
