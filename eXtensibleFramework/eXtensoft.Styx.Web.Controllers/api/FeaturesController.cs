// <copyright company="eXtensoft, LLC" file="FeaturesController.cs">
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

    public sealed class FeaturesController : EndpointServiceController
    {
        #region DO NOT ALTER
        private const string id = "A36791B1-E4D3-4E90-BD14-B92810BB81DD";
        private const string name = "Styx FeaturesController";
        private const string description = "This is the Feature controller for Styx";
        private const string whitelistPattern = "";
        private const string routeTablePattern = "v{version}/styx/features/{id}";

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
                name: "styx.features",
                routeTemplate: routeTablePattern,
                defaults: new { controller = "Features", id = RouteParameter.Optional });
        }


        #endregion

        private const string idParamName = "FeatureId";

        [HttpGet]
        HttpResponseMessage Get(int version, string id)
        {
            HttpResponseMessage message = null;
            ICriterion c;
            if (Validator.IsId(idParamName, id, out c))
            {
                var response = Service.Get<Feature>(c);
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
                var response = Service.Delete<Feature>(c);
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
        HttpResponseMessage Put(int version, [FromBody] Feature model)
        {
            HttpResponseMessage message = null;
            var response = Service.Put<Feature>(model, null);
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
        HttpResponseMessage Post(int version, [FromBody] Feature model)
        {
            HttpResponseMessage message = null;
            var response = Service.Post<Feature>(model);
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
