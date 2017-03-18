using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common.Contracts
{
    public interface IConfigurationProvider
    {
        NameValueCollection ProviderAppSettings { get; }

        ConnectionStringSettingsCollection ProviderConnectionStrings { get; }

        Configuration ProviderOpenExeConfiguration(ConfigurationUserLevel userLevel);

        Configuration ProviderOpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);

        void ProviderRefreshSection(string sectionName);
    }
}
