// <copyright company="eXtensible Solutions, LLC" file="CacheStrategyLoader.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public static class CacheStrategyLoader
    {
        public static ICache Load()
        {
            // based upon configuration load desired caching strategy: {Memcached, NetMemoryCache, CustomCache, etc)
            return new NetMemoryCache();
        }
    }
}
