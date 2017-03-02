// <copyright company="eXtensoft, LLC" file="AdministratorAccessAuthorizationFilter.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;

    public sealed class AdministratorAccessAuthorizationFilter : WebApiAuthorizationFilter
    {
        protected override bool IsAuthorized(HttpActionContext context)
        {
            return true;
        }
    }

}
