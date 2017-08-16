// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;

    public sealed class HttpsAuthorizationFilter : WebApiAuthorizationFilter
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthorized = false;

            if (actionContext.Request.RequestUri.Scheme == Uri.UriSchemeHttps)
            {
                isAuthorized = true;
            }
            return isAuthorized;
        }

        protected override void HandleUnauthorized(HttpActionContext context)
        {
            context.Response = context.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, AuthErrorMessage);
        }
    }
}
