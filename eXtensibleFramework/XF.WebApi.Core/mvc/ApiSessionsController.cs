// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi.Core.mvc
{
    using System.Web.Mvc;
    using XF.Common;

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
