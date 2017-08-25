//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Cyclops.Api.Controllers
//{

//    using System;
//    using System.Collections.Generic;
//    using System.Collections.Specialized;
//    using System.Configuration;
//    using System.Data;
//    using System.Data.SqlClient;
//    using System.Dynamic;
//    using System.Net;
//    using System.Net.Http;
//    using System.Web.Http;
//    using XF.Common;
//    using XF.WebApi;

//    public class SystemConfigurationController : EndpointServiceController
//    {

//        #region interface implementations
//        public override string Description => "This controller provides read access to configuration values";

//        public override Guid Id => new Guid("{1806651C-9C5A-4789-9FE8-4EFBE6B0F796}");

//        public override string Name => "System Config Controller";

//        public override string WhitelistPattern => "system-config/{token}/{shape}";

//        public override string RouteTablePattern => "system-config/{token}/{shape}";
//        #endregion

//        #region register override
//        public override void Register(HttpConfiguration config)
//        {
//            config.Routes.MapHttpRoute(
//                name: "system-configuration",
//                routeTemplate: RouteTablePattern,
//                defaults: new { controller = "SystemConfiguration", action = "Get", shape = RouteParameter.Optional }
//                );
//        }

//        #endregion

//        #region endpoint actions

//        [HttpGet]
//        public HttpResponseMessage Get(string token, string shape = "none")
//        {
//            if (eXtensibleConfig.Zone.Equals("production", StringComparison.OrdinalIgnoreCase) && !Request.GetProtocol().Equals("http",StringComparison.OrdinalIgnoreCase))
//            {
//                return Request.GenerateErrorResponse(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString());
//            }

//            bool b = (Request.GetProtocol().Equals("http", StringComparison.OrdinalIgnoreCase) && !String.IsNullOrWhiteSpace(shape) && shape.Equals("complete")) ? true : false;

//            HttpResponseMessage message = null;

//            dynamic vm = new ExpandoObject();
//            vm.Machine = System.Environment.MachineName;
//            vm.Config = token;
//#if DEBUG
//            vm.Mode = "DEBUG";
//#endif
//            switch (token.ToLower())
//            {
//                case "connections":
//                    vm.Connections = SystemConfigurationHandler.GetConnections(b);
//                    break;
//                case "environment":
//                    vm.Environment = SystemConfigurationHandler.GetEnvironment();
//                    break;
//                case "appsettings":
//                    vm.AppSettings = SystemConfigurationHandler.GetAppSettings();
//                    break;
//                case "api":
//                    vm.Registration =  RouteHelper.Execute(GlobalConfiguration.Configuration);
//                    break;
//                case "all":
//                    object o = AssembleAll(b);
//                    return Request.CreateResponse(HttpStatusCode.OK, o);
//                default:
//                    break;
//            }
//            message = Request.CreateResponse(HttpStatusCode.OK, (object)vm);

//            return message;
//        }



//        #endregion


//            #region helper

//        private static object AssembleAll(bool b)
//        {
//            dynamic vm = new ExpandoObject();
//            vm.Machine = System.Environment.MachineName;
//            vm.Config = "all";
//            vm.Zone = eXtensibleConfig.Zone;

//            vm.LoadMachine = Environment.MachineName;
//            //vm.CompiledAt = System.Reflection.Assembly.GetExecutingAssembly().GetLinkerTime().ToString("MM/dd/yyyy hh:mm:ss.fff tt");

//#if DEBUG
//            vm.CompileMode = "DEBUG";
//#endif

//            vm.Environment = SystemConfigurationHandler.GetEnvironment();
//            vm.Environment.Columns.RemoveAt(0);

//            vm.Connections = SystemConfigurationHandler.GetConnections(b);

//            vm.AppSettings = SystemConfigurationHandler.GetAppSettings();
//            vm.AppSettings.Columns.RemoveAt(0);

//            //vm.Registration = RouteHelper.Execute(GlobalConfiguration.Configuration);

//            return vm;
//        }

//        #endregion

//    }
//}
