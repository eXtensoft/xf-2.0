
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
    }
}
