using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Quality;

namespace XF.WebApi.Quality
{
    public abstract class WebApiProcessor : Processor<ProcessItem>
    {
        protected ProcessItem Item { get; set; }

        public bool IsPass { get; set; }

        protected override bool Initialize()
        {
            return InitializeProcessor();
        }

        protected override void Execute(ProcessItem t)
        {
            Item = t;
            ExecuteProcess(t);
            if (t.IsExecuteAssert)
            {
                IsPass = Assert(t);
            }
        }

        protected override void Teardown()
        {
            Cleanup();
        }

        public virtual void Cleanup()
        {

        }

        public virtual bool InitializeProcessor()
        {
            return true;
        }

        public virtual bool Assert(ProcessItem t)
        {
            return true;
        }

        public abstract void ExecuteProcess(ProcessItem t);

      


    }
}
