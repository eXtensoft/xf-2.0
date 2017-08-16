// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.




namespace XF.WebApi.Quality
{
    using XF.Quality;

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
