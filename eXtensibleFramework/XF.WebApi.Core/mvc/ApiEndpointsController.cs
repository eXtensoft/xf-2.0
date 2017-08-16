// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.




namespace XF.WebApi.Core
{
    using System;
    using System.Web.Mvc;

    public class ApiEndpointsController : Controller
    {
        public ActionResult Index()
        {
            //WebApiRegistrar.EndpointManager.Endpoints
            ViewBag.IsDirty = WebApiRegistrar.EndpointManager.IsDirty;
            return View(WebApiRegistrar.EndpointManager.Endpoints);
        }

        public ActionResult Swap(Guid xId, Guid yId)
        {
            WebApiRegistrar.EndpointManager.Swap(xId, yId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save()
        {
            if (WebApiRegistrar.EndpointManager.IsDirty)
            {
                WebApiRegistrar.EndpointManager.SaveChanges();
            }
            ViewBag.Message = "Changes have been saved.";
            return RedirectToAction("Index");
        }
    }
}
