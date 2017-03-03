// <copyright company="eXtensoft, LLC" file="ScenariosController.cs">
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

    public sealed class ScenariosController : EndpointServiceController
    {
        #region DO NOT ALTER
        private const string id = "B38A1DEF-5FFF-41F0-A453-57ADD027E333";
        private const string name = "Styx ScenariosController";
        private const string description = "This is the scenario controller for Styx";
        private const string whitelistPattern = "";
        private const string routeTablePattern = "v{version}/styx/scenarios/{id}";

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
                name: "styx.scenarios",
                routeTemplate: routeTablePattern,
                defaults: new { controller = "Scenarios", id = RouteParameter.Optional });
        }


        #endregion

        private const string idParamName = "ScenarioId";

        [HttpGet]
        HttpResponseMessage Get(int version,  string id)
        {
            HttpResponseMessage message = null;
            ICriterion c;
            if (Validator.IsId(idParamName,id, out c))
            {
                var response = Service.Get<Scenario>(c);
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
                var response = Service.Delete<Scenario>(c);
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
        HttpResponseMessage Put(int version, [FromBody] Scenario model)
        {
            HttpResponseMessage message = null;
            var response = Service.Put<Scenario>(model, null);
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
        HttpResponseMessage Post(int version, [FromBody] Scenario model)
        {
            HttpResponseMessage message = null;
            var response = Service.Post<Scenario>(model);
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
