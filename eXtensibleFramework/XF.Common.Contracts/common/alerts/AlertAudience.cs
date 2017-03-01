using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common
{
    [FlagsAttribute]
    public enum AlertAudiences
    {
        None = 0,
        Developer = 1,
        DBA = 2,
        Network = 4,
        Operations = 8,
        Management = 16,
        CTO = 32,
        Business = 64,
    }

}
