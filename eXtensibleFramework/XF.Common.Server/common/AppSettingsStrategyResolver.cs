// <copyright company="eXtensible Solutions, LLC" file="AppSettingsStrategyResolver.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Configuration;
    using XF.Common;

    public sealed class AppSettingsStrategyResolver : IStrategyResolver
    {
        string IStrategyResolver.Resolve(params string[] args)
        {
            string resolution = String.Empty;
            if (args != null)
            {
                if (args.Length == 1)
                {
                    string appkey = args[0];
                    resolution = ConfigurationManager.AppSettings[appkey];
                }
                else if (args.Length == 2)
                {
                    string appkey = string.Format("{0}.{1}", args[0], args[1]).Trim(new char[]{'.'});
                    resolution = ConfigurationManager.AppSettings[appkey];
                }
                else if (args.Length == 3)
                {
                    string appkey = string.Format("{0}.{1}.{2}", args[0], args[1].Trim(new char[] { '.' }), args[2]);
                    resolution = ConfigurationManager.AppSettings[appkey];
                }
            }

            return resolution;
        }
    }
}
