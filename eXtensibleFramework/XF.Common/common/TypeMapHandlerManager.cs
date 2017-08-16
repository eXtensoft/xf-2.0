// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    public class TypeMapHandlerManager 
    {
        private static ITypeMapCache _TypeCache;

        static TypeMapHandlerManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _TypeCache = new TypeMapCache();
            _TypeCache.Initialize();
        }


        public bool TryResolveHandler<T,U>(out U u) where T : class, new() where U : class, new()
        {
            u = null;
            bool b = false;
            Type type = _TypeCache.ResolveType<T>();
            if (type != null)
            {
                u = Activator.CreateInstance(type) as U;
                if (u != null)
                {
                    b = true;
                }
                else
                {
                    string message = String.Format("Unable to resolve the TypeMapHandler for {0}", typeof(T).GetType().FullName);
                    EventWriter.WriteError(message, SeverityType.Error,"General",eXtensibleConfig.GetProperties());
                }
            }
            return b;
        }
    }
}
