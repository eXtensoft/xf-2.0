

namespace XF.WebApi.Core.mvc
{
    using Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.WebApi.Core;

    public class ApiErrorsController : Controller
    {
        [HttpGet]
        public ActionResult Index(string zone = "all",string id = "", int pageSize = 50, int pageIndex = 0)
        {
            ZoneOption option;
            Page<ApiError> data = new Page<ApiError>();
            if (!String.IsNullOrEmpty(id))
            {
                data = ApiErrorSqlAccess.Get(id);

            }
            else if ( Enum.TryParse<ZoneOption>(id,true,out option))
            {
                data = ApiErrorSqlAccess.Get(option.ToString(), pageSize, pageIndex);
            }
            else
            {
                data = ApiErrorSqlAccess.Get(zone, pageSize, pageIndex);
            }

            return View(data);
        }

        //[HttpGet]
        //public ActionResult Index(string who, string path, string handler, string bearer, string basic, string code, bool hasLog = false, int pageSize = 10, int pageIndex = 0)
        //{

        //    Page<ApiError> data = null;// ApiRequestSqlAccess.Get(who, path, handler, bearer, basic, code, hasLog, pageSize, pageIndex);
        //    return View(data);

        //}
    }
}
