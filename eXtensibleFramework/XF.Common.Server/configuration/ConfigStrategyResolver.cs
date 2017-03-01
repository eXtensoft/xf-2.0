// <copyright company="eXtensible Solutions, LLC" file="ConfigStrategyResolver.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
