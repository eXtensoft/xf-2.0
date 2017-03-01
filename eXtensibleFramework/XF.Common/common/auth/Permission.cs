// <copyright company="eXtensible Solutions LLC" file="Permission.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
