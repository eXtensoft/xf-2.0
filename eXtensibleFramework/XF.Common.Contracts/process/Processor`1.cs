

namespace XF.Quality
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Processor<T> : IProcessor<T> where T : class, new()
    {
        private IProcessor<T> _Successor = null;

        #region Selectors (List<ProcessParameter>)

        private List<ProcessParameter> _Selectors = new List<ProcessParameter>();

        /// <summary>
        /// Gets or sets the List<ProcessParameter> value for Selectors
        /// </summary>
        /// <value> The List<ProcessParameter> value.</value>

        public List<ProcessParameter> Selectors
        {
            get { return _Selectors; }
            set
            {
                if (_Selectors != value)
                {
                    _Selectors = value;
                }
            }
        }

        #endregion

        bool IProcessor<T>.Initialize()
        {
            return Initialize();
        }

        void IProcessor<T>.Execute(T t)
        {
            Execute(t);
            if (_Successor != null)
            {
                _Successor.Execute(t);
            }
        }

        void IProcessor<T>.Teardown()
        {
            Teardown();
        }

        void IProcessor<T>.SetSuccessor(IProcessor<T> successor)
        {
            _Successor = successor;
        }



        protected virtual bool Initialize()
        {
            return true;
        }

        protected abstract void Execute(T t);

        protected virtual void Teardown()
        {

        }







    }
}
