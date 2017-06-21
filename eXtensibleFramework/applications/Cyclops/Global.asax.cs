using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using XF.WebApi.Core;

namespace Cyclops
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiRegistrar.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new eXtensibleMessageHandler());
            

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var xmlFormatter = formatters.XmlFormatter;
            formatters.Remove(xmlFormatter);
            var jsonFormatter = formatters.JsonFormatter;
            Newtonsoft.Json.Formatting formatting = Formatting.None;
            formatting = Formatting.Indented;

            var settings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = formatting,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            jsonFormatter.SerializerSettings = settings;

            SelectListItemCache.Instance.RegisterProvider("operating-systems", SelectionListUtility.GetOperatingSystems);
            SelectListItemCache.Instance.RegisterProvider("hosting", SelectionListUtility.GetHosting);
            SelectListItemCache.Instance.RegisterProvider("server-security", SelectionListUtility.GetServerSecurity);
            SelectListItemCache.Instance.RegisterProvider("app-types", SelectionListUtility.GetAppTypes);
            SelectListItemCache.Instance.RegisterProvider("scopes", SelectionListUtility.GetScopes);
            SelectListItemCache.Instance.RegisterProvider("zones", SelectionListUtility.GetZones);
            SelectListItemCache.Instance.RegisterProvider("domains", SelectionListUtility.GetDomains);
            SelectListItemCache.Instance.RegisterProvider("artifact-type", SelectionListUtility.GetArtifactTypes);
            //SelectListItemCache.Instance.RegisterProvider("domain", SelectionListUtility.GetDomains);
           // SelectListItemCache.Instance.RegisterProvider("urls", SelectionListUtility.GetUrls);

        }
    }
}
