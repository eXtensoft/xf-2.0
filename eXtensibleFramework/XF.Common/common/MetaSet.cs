// <copyright company="eXtensible Solutions LLC" file="MetaSet.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using XF.Common;

    [Serializable]
    public sealed class MetaSet
    {
        public VersionInfo Version { get; set; }

        public string OwnedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime Tds { get; set; }

        public Infoset ExtendedProperties { get; set; }
    }
}
