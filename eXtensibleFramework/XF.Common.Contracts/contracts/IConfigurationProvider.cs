// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Contracts
{
    using System.Collections.Specialized;
    using System.Configuration;

    public interface IConfigurationProvider
    {
        NameValueCollection ProviderAppSettings { get; }

        ConnectionStringSettingsCollection ProviderConnectionStrings { get; }

        Configuration ProviderOpenExeConfiguration(ConfigurationUserLevel userLevel);

        Configuration ProviderOpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);

        void ProviderRefreshSection(string sectionName);
    }
}
