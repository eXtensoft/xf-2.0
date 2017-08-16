// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
