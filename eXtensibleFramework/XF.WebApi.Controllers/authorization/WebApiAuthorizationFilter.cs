// <copyright company="eXtensoft, LLC" file="WebApiAuthorizationFilter.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

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
