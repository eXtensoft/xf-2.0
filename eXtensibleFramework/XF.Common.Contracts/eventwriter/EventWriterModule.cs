EventWriterModule.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common.Contracts
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    public class EventWriterModule
    {
        [ImportMany(typeof(IEventWriter))]
        public List<IEventWriter> Providers { get; set; }

    }
}
