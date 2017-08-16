// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    public class WebApiAuthorizationFilter : AuthorizeAttribute
    {
        protected virtual string AuthErrorMessage
        {
            get
            {
                return "Access is not authorized.";
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext context)
        {
            HandleUnauthorized(context);
        }

        protected virtual void HandleUnauthorized(HttpActionContext context)
        {
            context.Response = context.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.NotImplemented, AuthErrorMessage);
        }
    }

}
