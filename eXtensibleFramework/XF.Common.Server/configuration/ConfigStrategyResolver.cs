// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Configuration;

    public static class ConfigStrategyResolverLoader
    {
        public static IConfigStrategyResolver Load(string sectionGroupName)
        {
            IConfigStrategyResolver resolver = new DatabaseKeyResolver();

            string filepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = filepath };

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            eXtensibleStrategySectionGroup group = config.SectionGroups[sectionGroupName] as eXtensibleStrategySectionGroup;
            resolver.Initialize(group);
            if (eXtensibleConfig.Inform)
            {
                EventWriter.Inform("DatabaseKeyResolver loaded");
            }
            
            return resolver;
        }
    }
}
