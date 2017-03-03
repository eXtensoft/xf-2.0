// <copyright company="eXtensoft, LLC" file="ItemsController.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Styx.Web.Controllers
{
    using ProjectManagement;
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

    public sealed class ItemsController : EndpointServiceController
    {
        #region DO NOT ALTER
        private const string id = "F14EF5E6-9F77-44B2-BC7D-6A83B25933AF";
        private const string name = "Styx ItemsController";
        private const string description = "This is the Item controller for Styx";
        private const string whitelistPattern = "";
        private const string routeTablePattern = "v{version}/styx/items/{id}";

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
                name: "styx.items",
                routeTemplate: routeTablePattern,
                defaults: new { controller = "Items", id = RouteParameter.Optional });
        }


        #endregion

        private const string idParamName = "ItemId";

        [HttpGet]
        HttpResponseMessage Get(int version, string id)
        {
            HttpResponseMessage message = null;
            ICriterion c;
            if (Validator.IsId(idParamName, id, out c))
            {
                var response = Service.Get<Item>(c);
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
                var response = Service.Delete<Item>(c);
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
        HttpResponseMessage Put(int version, [FromBody] Item model)
        {
            HttpResponseMessage message = null;
            var response = Service.Put<Item>(model, null);
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
        HttpResponseMessage Post(int version, [FromBody] Item model)
        {
            HttpResponseMessage message = null;
            var response = Service.Post<Item>(model);
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
