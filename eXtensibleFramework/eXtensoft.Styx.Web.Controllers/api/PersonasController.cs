// <copyright company="eXtensoft, LLC" file="PersonasController.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Styx.Web.Controllers
{
    using Styx.ProjectManagement;
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

    public sealed class PersonasController : EndpointServiceController
    {
        #region DO NOT ALTER
        private const string id = "154E0558-EA35-4856-8E43-416C965F4A3F";
        private const string name = "Styx PersonasController";
        private const string description = "This is the Persona controller for Styx";
        private const string whitelistPattern = "";
        private const string routeTablePattern = "v{version}/styx/personas/{id}";

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
                name: "styx.personas",
                routeTemplate: routeTablePattern,
                defaults: new { controller = "Personas", id = RouteParameter.Optional });
        }


        #endregion

        private const string idParamName = "PersonaId";

        [HttpGet]
        HttpResponseMessage Get(int version, string id)
        {
            HttpResponseMessage message = null;
            ICriterion c;
            if (Validator.IsId(idParamName, id, out c))
            {
                //Validator.AddParams(Request, c);

                var response = Service.Get<Persona>(c);
                if (response.IsOkay)
                {
                    message = Request.CreateResponse(HttpStatusCode.OK, response.Model);
                }
                else
                {
                    message = Request.CreateErrorResponse((HttpStatusCode)response.Status.Code, response.Status.Description);
                }
            }
            else
            {
                message = Request.CreateErrorResponse(HttpStatusCode.Conflict, "id not recognized");
            }
            return message;
        }



        [HttpGet]
        HttpResponseMessage Delete(int version, string id)
        {
            HttpResponseMessage message = null;
            ICriterion c;
            if (Validator.IsId(idParamName, id, out c))
            {
                var response = Service.Delete<Persona>(c);
                if (response.IsOkay)
                {
                    message = Request.CreateResponse(HttpStatusCode.OK, response.Model);
                }
                else
                {
                    message = Request.CreateResponse((HttpStatusCode)response.Status.Code, response.Status.Description);
                }
            }
            else
            {
                message = Request.CreateErrorResponse(HttpStatusCode.Conflict, "id not recognized");
            }
            return message;
        }
        [HttpGet]
        HttpResponseMessage Put(int version, [FromBody] Persona model)
        {
            HttpResponseMessage message = null;
            var response = Service.Put<Persona>(model, null);
            if (response.IsOkay)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, response.Model);
            }
            else
            {
                message = Request.CreateResponse((HttpStatusCode)response.Status.Code, response.Status.Description);
            }
            return message;
        }

        [HttpGet]
        HttpResponseMessage Post(int version, [FromBody] Persona model)
        {
            HttpResponseMessage message = null;
            var response = Service.Post<Persona>(model);
            if (response.IsOkay)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, response.Model);
            }
            else
            {
                message = Request.CreateResponse((HttpStatusCode)response.Status.Code, response.Status.Description);
            }
            return message;
        }

    }

}
