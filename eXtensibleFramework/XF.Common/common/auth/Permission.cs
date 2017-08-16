// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    [Serializable]
    public sealed class Permission
    {
        public string ActionName { get; set; }

        public string AlternateId { get; set; }

        public GrantOption PermissionGrant { get; set; }

        public Guid TargetActionId { get; set; }

        public string TargetName { get; set; }
    }
}
