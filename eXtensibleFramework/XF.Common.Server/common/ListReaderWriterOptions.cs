

namespace XF.Common.BulkCopy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Flags]
    public enum ListReaderWriterOptions
    {
        Default,
        TruncateOnOverflow,
        CreateTable,
    }
}
