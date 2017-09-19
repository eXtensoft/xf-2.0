// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Contracts
{
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;
    using System.Configuration;

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
