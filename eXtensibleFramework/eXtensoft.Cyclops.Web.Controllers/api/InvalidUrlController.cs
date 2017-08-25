

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;
    using XF.WebApi;

    [PublicAccessAuthorizationFilter]
    public class InvalidUrlController : EndpointServiceController
    {
        #region do not alter

        private const string id = "CCCBFDC2-783C-49E6-B938-61F8ABDBB3C3";
        private const string name = "Invalid Url Controller";
        private const string description = "This endpoint captures invalid api url calls";
        private const int version = 1;
        private const string whitelistPattern = "{*tokens}";
        private const string routeTablePattern = "v{version}/{*tokens}";


        public override string Description
        {
            get
            {
                return description;
            }
        }


        public override Guid Id
        {
            get
            {
                return new Guid(id);
            }
        }

        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override string WhitelistPattern
        {
            get
            {
                return whitelistPattern;
            }
        }

        public override string RouteTablePattern
        {
            get
            {
                return routeTablePattern;
            }
        }

        #endregion

        #region register override
        public override void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "InvalidUrl",
                routeTemplate: routeTablePattern,
                defaults: new { controller = "InvalidUrl", action = "Get", tokens = RouteParameter.Optional }
                );
        }

        #endregion

        #region endpoint actions

        [HttpPost]
        public HttpResponseMessage Post(string tokens)
        {
            //ApiEvent data = CreateApiEvent("invalid.url");
            string message = !String.IsNullOrWhiteSpace(tokens) ? String.Format("Invalid 'POST' request, Url: {0}", tokens) : "Invalid Request";
            //EventWriter.WriteEvent(message, data.Build());
            return Request.GenerateResponse(HttpStatusCode.BadRequest, String.Format("Invalid 'POST' request, API URL: /v1/{0}", tokens));
        }

        [HttpGet]
        public HttpResponseMessage Get(string tokens)
        {
            //ApiEvent data = CreateApiEvent("invalid.url");
            string message = !String.IsNullOrWhiteSpace(tokens) ? String.Format("Invalid 'GET' request, Url: {0}", tokens) : "Invalid Request";
            //EventWriter.WriteEvent(message, data.Build());
            return Request.GenerateResponse(HttpStatusCode.BadRequest, String.Format("Invalid 'GET' request, API URL: /v1/{0}", tokens));
        }

        [HttpPut]
        public HttpResponseMessage Put(string tokens)
        {
            //ApiEvent data = CreateApiEvent("invalid.url");
            string message = !String.IsNullOrWhiteSpace(tokens) ? String.Format("Invalid 'PUT' request, Url: {0}", tokens) : "Invalid Request";
            //EventWriter.WriteEvent(message, data.Build());
            return Request.GenerateResponse(HttpStatusCode.BadRequest, String.Format("Invalid 'PUT' request, API URL: /v1/{0}", tokens));
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string tokens)
        {
            //ApiEvent data = CreateApiEvent("invalid.url");
            string message = !String.IsNullOrWhiteSpace(tokens) ? String.Format("Invalid 'DELETE' request, Url: {0}", tokens) : "Invalid Request";
            //EventWriter.WriteEvent(message, data.Build());
            return Request.GenerateResponse(HttpStatusCode.BadRequest, String.Format("Invalid 'DELETE' request, API URL: /v1/{0}", tokens));
        }


        #endregion



    }

}
