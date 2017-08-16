// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{

    using System;
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
