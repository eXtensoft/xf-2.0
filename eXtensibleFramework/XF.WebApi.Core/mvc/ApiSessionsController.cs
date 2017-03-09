
namespace XF.WebApi.Core.mvc
{
    using XF.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.WebApi.Core;

    public class ApiSessionsController : Controller
    {
        [HttpGet]
        public ActionResult Index(string who, string path, string handler, string bearer, string basic, string code, bool hasLog = false, int pageSize = 10, int pageIndex = 0)
        {

            Page<ApiSession> data = null;// ApiRequestSqlAccess.Get(who, path, handler, bearer, basic, code, hasLog, pageSize, pageIndex);
            return View(data);

        }
    }
}
