

namespace XF.Quality
{
    using System;

    public interface IProcessor<T> where T : class, new()
    {
        bool Initialize();
        void Execute(T t);
        void Teardown();
        void SetSuccessor(IProcessor<T> successor);
    }
}
