// <copyright file="BindingTypeOptions.cs" company="eXtensible Solutions LLC">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "BasicHttp is the default binding"), FlagsAttribute]
    public enum BindingTypeOptions
    {
        BasicHttp = 0,
        WsHttp = 1,
        Tcp = 2,
        NoSecurity = 4,
        MessageSecurity = 8,
        TransportSecurity = 16,
        TransportWithMessageCredential = 32
    }
}
