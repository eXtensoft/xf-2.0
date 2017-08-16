// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Quality
{
    using System.Collections.Generic;

    public class MetaProcessorManager
    {
        #region Factory (ProcessorFactory)

        private ProcessorFactory _Factory;

        /// <summary>
        /// Gets or sets the ProcessorFactory value for Factory
        /// </summary>
        /// <value> The ProcessorFactory value.</value>

        public ProcessorFactory Factory
        {
            get             
            {
                if (_Factory == null)
                {
                    _Factory = new ProcessorFactory();
                }
                return _Factory; 
            }
            set
            {
                if (_Factory != value)
                {
                    _Factory = value;
                }
            }
        }

        #endregion

        public void Execute(MetaChain instructions)
        {
            IProcessor<ProcessItem> chain = GenerateChain(instructions);

            if (chain.Initialize())
            {
                chain.Execute(instructions.ToProcessItem());
                chain.Teardown();
            }
        }

        private IProcessor<ProcessItem> GenerateChain(MetaChain instructions)
        {
            List<IProcessor<ProcessItem>> list = new List<IProcessor<ProcessItem>>();
            
            foreach (var item in list)
            {
                IProcessor<ProcessItem> p = Factory.Create(item) as IProcessor<ProcessItem>;
            }

            IProcessor<ProcessItem> head = GenerateChain(list);
            return head;
        }

        private IProcessor<ProcessItem> GenerateChain(List<IProcessor<ProcessItem>> list)
        {

            IProcessor<ProcessItem> p = new HeadProcessor();
            if (list != null && list.Count > 0)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    list[i - 1].SetSuccessor(list[i]);
                }
                p.SetSuccessor(list[0]);                
            }

            return p;
        }
    }
}
