// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using XF.Common;

    public class eXtensibleClaimsPrincipal<T> : eXtensibleClaimsPrincipal where T : class, new()
    {


        public T Model { get; set; }


        public eXtensibleClaimsPrincipal(ClaimsIdentity identity)
            : base(identity)
        {
        }


        public static void Accept(IeXtensibleVisitor<T> visitor)
        {
            var cp = Thread.CurrentPrincipal as eXtensibleClaimsPrincipal<T>;
            if (cp != null)
            {
                cp.Items.Accept(cp.Model, visitor);
            }
        }


    }

}
