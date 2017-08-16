// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.Quality
{
    using System.Collections.Generic;
    using XF.Common;

    public static class MetaExtensionMethods
    {
        public static ProcessItem ToProcessItem(this MetaChain chain)
        {

            ProcessItem item = new ProcessItem()
            {
                Parameters = new List<ProcessParameter>(),
                Settings = chain.Settings.Clone(),
            };

            foreach (var param in chain.Parameters)
            {
                var p = param.Clone();
                item.Parameters.Add(p);
            }

            return item;
        }
    }
}
