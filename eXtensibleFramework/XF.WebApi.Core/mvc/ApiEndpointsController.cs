using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace XF.WebApi.Core
{
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
