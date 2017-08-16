// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


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
