// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System.Web.Mvc;
    using XF.Common;
    using XF.WebApi.Core;

    public class ApiRequestsController : Controller
    {

        [HttpGet]
        public ActionResult Index(string who, string path, string handler, string bearer, string basic, string code, bool hasLog = false, int pageSize = 10, int pageIndex = 0)
        {

            Page<ApiRequest> data = ApiRequestSqlAccess.Get(who, path, handler, bearer, basic, code, hasLog, pageSize, pageIndex);
            //var props = eXtensibleConfig.GetProperties();
            //var message = "errir tist";
            //EventWriter.WriteError(message, SeverityType.Critical, "test", props);
            data.Items.ForEach(x => x.UserAgent = x.UserAgent.Truncate(15));
            return View(data);

        }



    }
}
