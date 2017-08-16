// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.Common
{
    using System.Collections.Specialized;
    using System.Configuration;
    using XF.Common.Contracts;

    public class ConfigurationProvider 
    {
        private static object sync = new object();
        private static volatile IConfigurationProvider provider;

        public string ProviderName
        {
            get
            {
                return provider.GetType().FullName;
            }
        }

        public static IConfigurationProvider Provider
        {
            get
            {
                if (provider == null)
                {
                    lock (sync)
                    {
                        if (provider == null)
                        {
                            try
                            {
                                provider = ConfigurationProviderLoader.Load();
                            }
                            catch
                            {

                            }
                        }
                    }
                }
                return provider;
            }
        }
        
        //
        // Summary:
        //     Gets the System.Configuration.AppSettingsSection data for the current application's
        //     default configuration.
        //
        // Returns:
        //     Returns a System.Collections.Specialized.NameValueCollection object that contains
        //     the contents of the System.Configuration.AppSettingsSection object for the current
        //     application's default configuration.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Could not retrieve a System.Collections.Specialized.NameValueCollection object
        //     with the application settings data.
        public static NameValueCollection AppSettings { get { return Provider.ProviderAppSettings; } }
        //
        // Summary:
        //     Gets the System.Configuration.ConnectionStringsSection data for the current application's
        //     default configuration.
        //
        // Returns:
        //     Returns a System.Configuration.ConnectionStringSettingsCollection object that
        //     contains the contents of the System.Configuration.ConnectionStringsSection object
        //     for the current application's default configuration.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Could not retrieve a System.Configuration.ConnectionStringSettingsCollection
        //     object.
        public static ConnectionStringSettingsCollection ConnectionStrings { get { return Provider.ProviderConnectionStrings; } }
        //
        // Summary:
        //     Retrieves a specified configuration section for the current application's default
        //     configuration.
        //
        // Parameters:
        //   sectionName:
        //     The configuration section path and name.
        //
        // Returns:
        //     The specified System.Configuration.ConfigurationSection object, or null if the
        //     section does not exist.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     A configuration file could not be loaded.
        public static void GetSection(string sectionName)
        {
            Provider.ProviderRefreshSection(sectionName);
        }
        //
        // Summary:
        //     Opens the configuration file for the current application as a System.Configuration.Configuration
        //     object.
        //
        // Parameters:
        //   userLevel:
        //     The System.Configuration.ConfigurationUserLevel for which you are opening the
        //     configuration.
        //
        // Returns:
        //     A System.Configuration.Configuration object.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     A configuration file could not be loaded.
        public static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
        {
            return Provider.ProviderOpenExeConfiguration(userLevel);
        }
        //
        // Summary:
        //     Opens the specified client configuration file as a System.Configuration.Configuration
        //     object.
        //
        // Parameters:
        //   exePath:
        //     The path of the executable (exe) file.
        //
        // Returns:
        //     A System.Configuration.Configuration object.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     A configuration file could not be loaded.
        public static Configuration OpenExeConfiguration(string exePath)
        {
            return ConfigurationManager.OpenExeConfiguration(exePath);
        }
        //
        // Summary:
        //     Opens the machine configuration file on the current computer as a System.Configuration.Configuration
        //     object.
        //
        // Returns:
        //     A System.Configuration.Configuration object.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     A configuration file could not be loaded.
        public static Configuration OpenMachineConfiguration()
        {
            return ConfigurationManager.OpenMachineConfiguration();
        }
        //
        // Summary:
        //     Opens the specified client configuration file as a System.Configuration.Configuration
        //     object that uses the specified file mapping and user level.
        //
        // Parameters:
        //   fileMap:
        //     An System.Configuration.ExeConfigurationFileMap object that references configuration
        //     file to use instead of the application default configuration file.
        //
        //   userLevel:
        //     The System.Configuration.ConfigurationUserLevel object for which you are opening
        //     the configuration.
        //
        // Returns:
        //     The configuration object.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     A configuration file could not be loaded.
        public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
        {
            return Provider.ProviderOpenMappedExeConfiguration(fileMap, userLevel);
        }
        //
        // Summary:
        //     Opens the specified client configuration file as a System.Configuration.Configuration
        //     object that uses the specified file mapping, user level, and preload option.
        //
        // Parameters:
        //   fileMap:
        //     An System.Configuration.ExeConfigurationFileMap object that references the configuration
        //     file to use instead of the default application configuration file.
        //
        //   userLevel:
        //     The System.Configuration.ConfigurationUserLevel object for which you are opening
        //     the configuration.
        //
        //   preLoad:
        //     true to preload all section groups and sections; otherwise, false.
        //
        // Returns:
        //     The configuration object.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     A configuration file could not be loaded.
        public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad)
        {
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel, preLoad);
        }
        //
        // Summary:
        //     Opens the machine configuration file as a System.Configuration.Configuration
        //     object that uses the specified file mapping.
        //
        // Parameters:
        //   fileMap:
        //     An System.Configuration.ExeConfigurationFileMap object that references configuration
        //     file to use instead of the application default configuration file.
        //
        // Returns:
        //     A System.Configuration.Configuration object.
        //
        // Exceptions:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     A configuration file could not be loaded.
        public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
        {
            return ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
        }
        //
        // Summary:
        //     Refreshes the named section so the next time that it is retrieved it will be
        //     re-read from disk.
        //
        // Parameters:
        //   sectionName:
        //     The configuration section name or the configuration path and section name of
        //     the section to refresh.
        public static void RefreshSection(string sectionName)
        {
            Provider.ProviderRefreshSection(sectionName);
        }


    }
}
