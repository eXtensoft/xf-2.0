

namespace XF.Common.Db
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class DbConfigCollection : KeyedCollection<string,DbConfig>
    {
        protected override string GetKeyForItem(DbConfig item)
        {
            return item.AppContextKey;
        }
    }
}
