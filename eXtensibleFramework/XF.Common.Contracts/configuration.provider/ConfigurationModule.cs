using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Common.Contracts;

namespace XF.Common.Contracts
{
    public class ConfigurationModule
    {
        [Import]
        public IEnumerable<IConfigurationProvider> Providers { get; set; }

    }
}
