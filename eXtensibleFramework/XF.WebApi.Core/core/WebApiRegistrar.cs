// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using XF.Common;

    public static class WebApiRegistrar
    {
        public static ApiEndpointManager EndpointManager;
        private static bool _IsInitialized;

        static WebApiRegistrar()
        {
            Initialize();
        }

        private static void Initialize()
        {
            List<string> list = new List<string>()
            {
                AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"),
                eXtensibleConfig.RemoteProcedureCallPlugins
            };

            // load ApiEndpointManager
            ModuleLoader<ApiEndpointManager> loader = new ModuleLoader<ApiEndpointManager>() { Folderpaths = list };
            if (loader.Load(out EndpointManager))
            {
                EndpointManager.Initialize();
                _IsInitialized = true;
            }
        }


        public static void Register(HttpConfiguration config)
        {
            if (_IsInitialized)
            {
                foreach (IEndpointController pluginController in EndpointManager)
                {
                    pluginController.RegisterApiRoute(config);
                }
                var cors = new EnableCorsAttribute("*", "*", "GET,POST,PUT,DELETE,OPTIONS"); // origins, headers, methods
                config.EnableCors(cors);
            }


        }
    }

}
