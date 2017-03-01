using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Common;

namespace XF.Quality
{
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
