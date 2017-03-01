// <copyright company="eXtensoft, LLC" file="eXtensibleCliamsPrincipal`1.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

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
