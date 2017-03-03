using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ServerAppViewModelCollection : KeyedCollection<string, ServerAppViewModel>
    {
        public string Key { get; set; }

        protected override string GetKeyForItem(ServerAppViewModel item)
        {
            return item.Server;
        }

        public void AddRange(IEnumerable<ServerAppViewModel> list)
        {
            foreach (var item in list)
            {
                if (!Contains(item.Server))
                {
                    Add(item);
                }
            }
        }
    }
}
