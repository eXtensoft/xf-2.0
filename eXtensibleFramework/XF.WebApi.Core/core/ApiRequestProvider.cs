using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Common;

namespace XF.WebApi.Core
{
    public static class ApiRequestProviderLoader
    {
        public static IApiRequestProvider Load()
        {
            IApiRequestProvider provider = null;

            switch (eXtensibleWebApiConfig.LogTo)
            {

                case XF.Common.LoggingStrategyOption.Datastore:
                    provider = new SqlServerApiRequestProvider();
                    break;
                case XF.Common.LoggingStrategyOption.Plugin:
                    provider = PluginLoader.LoadReferencedAssembly<IApiRequestProvider>().FirstOrDefault() as IApiRequestProvider;
                    break;
                case XF.Common.LoggingStrategyOption.WindowsEventLog:
                case XF.Common.LoggingStrategyOption.None:
                case XF.Common.LoggingStrategyOption.Output:
                case XF.Common.LoggingStrategyOption.Silent:
                case XF.Common.LoggingStrategyOption.XFTool:
                case XF.Common.LoggingStrategyOption.CommonServices:
                default:
                    break;
            }

            return provider;
        }
    }
}
