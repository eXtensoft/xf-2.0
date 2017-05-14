// <copyright company="eXtensoft, LLC" file="NoteApiController.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using XF.Common;
    using XF.WebApi;

    public sealed class NoteApiController : EndpointServiceController
    {
        #region DO NOT ALTER
        private const string id = "ACEED304-C4DB-4824-8739-18EE7ADF01C4";
        private const string name = "Cyclops NoteController";
        private const string description = "This is the Note controller for Cyclops";
        private const string whitelistPattern = "";
        private const string routeTablePattern = "v{version}/cyclops/notes/{id}";

        public override string Description
        {
            get { return description; }
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
            get { return name; }
        }

        public override string WhitelistPattern
        {
            get { return whitelistPattern; }
        }

        public override string RouteTablePattern
        {
            get { return routeTablePattern; }
        }

        public override void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "cyclops.note",
                routeTemplate: routeTablePattern,
                defaults: new { controller = "NoteApi", id = RouteParameter.Optional });
        }


        #endregion

        #region local members

        #endregion

        #region properties

        #endregion

        #region constructors

        #endregion

        [HttpGet]
        public HttpResponseMessage Get(int version, string id)
        {
            HttpResponseMessage message = null;
            if (!String.IsNullOrWhiteSpace(id))
            {
                var c = new Criterion("Id", id);
                var response = Service.Get<Note>(c);
                if (response.IsOkay)
                {
                    message = Request.GenerateResponse(HttpStatusCode.OK, response.Model);
                }
                else
                {
                    message = Request.GenerateErrorResponse(HttpStatusCode.Conflict, response.Status.Description);
                }
            }
            else
            {
                var response = Service.Get<Note>(null);
                if (response.IsOkay)
                {
                    message = Request.GenerateResponse(HttpStatusCode.OK, response.Model);
                }
                else
                {
                    message = Request.GenerateErrorResponse(HttpStatusCode.Conflict, response.Status.Description);
                }
            }
            
            return message;
        }

        [HttpPost]
        public HttpResponseMessage Post(int version, Note model)
        {
            HttpResponseMessage message = null;
            var response = Service.Post(model);
            if (response.IsOkay)
            {
                message = Request.GenerateResponse(HttpStatusCode.OK, response.Model);
            }
            else
            {
                message = Request.GenerateErrorResponse(HttpStatusCode.Conflict, response.Status.Description);
            }
            return message;
        }


        [HttpPut]
        public HttpResponseMessage Put(int version, Note model)
        {
            HttpResponseMessage message = null;
            var response = Service.Put(model,null);
            if (response.IsOkay)
            {
                message = Request.GenerateResponse(HttpStatusCode.OK, response.Model);
            }
            else
            {
                message = Request.GenerateErrorResponse(HttpStatusCode.Conflict, response.Status.Description);
            }
            return message;
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int version, string id)
        {
            HttpResponseMessage message = null;

            if (!String.IsNullOrWhiteSpace(id))
            {
                var c = new Criterion("Id", id);
                var response = Service.Delete<Note>(c);
                if (response.IsOkay)
                {
                    message = Request.GenerateResponse(HttpStatusCode.OK, response.Model);
                }
                else
                {
                    message = Request.GenerateErrorResponse(HttpStatusCode.Conflict, response.Status.Description);
                }
            }

            return message;
        }









    }

}
