using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XF.Common;
using Cyclops.Web;

namespace Cyclops.Controllers
{
    public class DocumentsController : ServiceController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
