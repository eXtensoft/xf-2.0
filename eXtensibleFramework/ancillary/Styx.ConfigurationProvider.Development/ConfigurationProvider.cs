using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Common;
using XF.Common.Contracts;

namespace Styx.ConfigurationProvider
{
    public class ConfigurationProvider : SystemConfigurationProvider
    {
        private static object sync = new object();
        private static volatile ConnectionStringSettingsCollection _ConnectionStrings;

        protected override ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                if (_ConnectionStrings == null)
                {
                    lock(sync)
                    {
                        if (_ConnectionStrings == null)
                        {
                            try
                            {
                                _ConnectionStrings = GenerateConnectionStrings();
                            }
                            catch (Exception ex)
                            {
                                _ConnectionStrings = ConfigurationManager.ConnectionStrings;
                                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                var props = eXtensibleConfig.GetProperties();
                                props.Add("StyxConfigurationProvider", "31");
                                IEventWriter writer = new EventLogWriter();
                                writer.WriteError(message, SeverityType.Critical, "Configuration", props);
                            }
                        }
                    }
                }
                return _ConnectionStrings;
            }

        }

        private ConnectionStringSettingsCollection GenerateConnectionStrings()
        {
            var connectionStrings = new ConnectionStringSettingsCollection();
            connectionStrings.Add(new ConnectionStringSettings("styx", "localhost", "System.Data.SqlClient"));
            return connectionStrings;

        }
    }
}
