
namespace Cyclops.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.Common;
    using System.Dynamic;
    using Cyclops.Web;

    public class ServerAppController : FileUploadController
    {
        [HttpGet]
        public ActionResult Index (Nullable<int> id)
        {
            if (id.HasValue)
            {
                var c = Criterion.GenerateStrategy("server.app");
                c.AddItem("ServerAppId", id);
                var response = Service.Get<ServerApp>(c);
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    var vm = new ServerAppViewModel(response.Model);
                    return View( vm);
                }
            }
            else
            {
                var response = Service.GetAll<ServerApp>(null);
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                   // var vm = new ServerAppViewModel(response);
                    return View();
                }
            }


        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            var c = new Criterion("ServerAppId", id);
            var response = Service.Get<ServerApp>(c);
            if (!response.IsOkay)
            {
                return View(ErrorViewName,response.Status);
            }
            else
            {
                ServerAppViewModel vm = new ServerAppViewModel(response.Model);
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var criterion = new Criterion("ServerAppId", id);
            var response = Service.Delete<ServerApp>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
