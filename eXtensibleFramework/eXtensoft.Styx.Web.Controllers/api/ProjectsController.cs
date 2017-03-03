// <copyright company="eXtensoft, LLC" file="ProjectsController.cs">
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

    public sealed class ProjectsController : EndpointServiceController
    {
        #region DO NOT ALTER
        private const string id = "21C2FB95-4356-49E7-82C5-501608CF878F";
        private const string name = "Styx ProjectsController";
        private const string description = "This is the Project controller for Styx";
        private const string whitelistPattern = "";
        private const string routeTablePattern = "v{version}/styx/projects/{id}";

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
                name: "styx.projects",
                routeTemplate: routeTablePattern,
                defaults: new { controller = "Projects", id = RouteParameter.Optional });
        }


        #endregion

        private const string idParamName = "ProjectId";

        [HttpGet]
        HttpResponseMessage Get(int version, string id)
        {
            HttpResponseMessage message = null;
            ICriterion c;
            if (Validator.IsId(idParamName, id, out c))
            {
                var response = Service.Get<Project>(c);
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
                var response = Service.Delete<Project>(c);
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
        HttpResponseMessage Put(int version, [FromBody] Project model)
        {
            HttpResponseMessage message = null;
            var response = Service.Put<Project>(model, null);
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
        HttpResponseMessage Post(int version, [FromBody] Project model)
        {
            HttpResponseMessage message = null;
            var response = Service.Post<Project>(model);
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
