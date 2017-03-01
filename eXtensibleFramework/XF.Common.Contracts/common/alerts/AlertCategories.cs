using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common
{
    [FlagsAttribute]
    public enum AlertCategories
    {
        None = 0,
        Processing = 1,
        Connectivity = 2,
        Resources = 4,
        Database = 8,
        Web = 16,
        API = 32,
        WebService = 64,
        Ftp = 128,
        Authentication = 256,
        Authorization = 512,
    }
}
