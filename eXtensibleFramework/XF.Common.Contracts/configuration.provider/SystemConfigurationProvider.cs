using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common.Contracts
{

    [InheritedExport(typeof(IConfigurationProvider))]
    public class SystemConfigurationProvider : IConfigurationProvider
    {
        NameValueCollection IConfigurationProvider.ProviderAppSettings
        {
            get
            {
                return AppSettings;
            }
        }

        ConnectionStringSettingsCollection IConfigurationProvider.ProviderConnectionStrings
        {
            get
            {
                return ConnectionStrings;
            }
        }

        Configuration IConfigurationProvider.ProviderOpenExeConfiguration(ConfigurationUserLevel userLevel)
        {
            return OpenExeConfiguration(userLevel);
        }

        Configuration IConfigurationProvider.ProviderOpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
        {
            return OpenMappedExeConfiguration(fileMap, userLevel);
        }

        void IConfigurationProvider.ProviderRefreshSection(string sectionName)
        {
            RefreshSection(sectionName);
        }

        protected virtual NameValueCollection AppSettings { get { return ConfigurationManager.AppSettings; } }

        protected virtual ConnectionStringSettingsCollection ConnectionStrings { get { return ConfigurationManager.ConnectionStrings; } }

        protected virtual Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
        {
            return ConfigurationManager.OpenExeConfiguration(userLevel);
        }

        protected virtual Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
        {
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel);
        }

        protected virtual void RefreshSection(string sectionName)
        {
             ConfigurationManager.RefreshSection(sectionName);
        }

    }
}
