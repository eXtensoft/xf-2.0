using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Quality
{
    public class HeadProcessor : Processor<ProcessItem>
    {
        protected override bool Initialize()
        {
            return true;
        }
        protected override void Execute(ProcessItem t)
        {
            
        }
    }
}
