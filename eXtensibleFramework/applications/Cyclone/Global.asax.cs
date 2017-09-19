using Cyclops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Cyclone
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);








            SelectListItemCache.Instance.RegisterProvider("operating-systems", SelectionListUtility.GetOperatingSystems);
            SelectListItemCache.Instance.RegisterProvider("hosting", SelectionListUtility.GetHosting);
            SelectListItemCache.Instance.RegisterProvider("server-security", SelectionListUtility.GetServerSecurity);
            SelectListItemCache.Instance.RegisterProvider("app-types", SelectionListUtility.GetAppTypes);
            SelectListItemCache.Instance.RegisterProvider("scopes", SelectionListUtility.GetScopes);
            SelectListItemCache.Instance.RegisterProvider("zones", SelectionListUtility.GetZones);
            SelectListItemCache.Instance.RegisterProvider("domains", SelectionListUtility.GetDomains);
            SelectListItemCache.Instance.RegisterProvider("artifact-type", SelectionListUtility.GetArtifactTypes);
            SelectListItemCache.Instance.RegisterProvider("users", SelectionListUtility.GetUsers);
        }
    }
}
