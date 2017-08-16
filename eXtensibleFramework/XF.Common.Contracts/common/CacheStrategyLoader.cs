// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
