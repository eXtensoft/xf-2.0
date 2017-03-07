// <copyright company="eXtensoft, LLC" file="PublicAccessAuthorizationFilter.cs">
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


    public class SpecialAccessAuthorizationFilter : WebApiAuthorizationFilter
    {
        protected override bool IsAuthorized(HttpActionContext context)
        {
            bool b = false;
            var authHeader = context.Request.Headers.Authorization;
            if (authHeader != null)
            {
                string authSchema = authHeader.Scheme;
                string authValue = authHeader.Parameter;
                if (authSchema.Equals("special") && !String.IsNullOrWhiteSpace(authValue))
                {
                    b = authValue.IsSpecial();
                }
            }
            return b;
        }

        private static bool IsSpecial(string token)
        {
            bool b = false;


            return b;
        }
    }

}
