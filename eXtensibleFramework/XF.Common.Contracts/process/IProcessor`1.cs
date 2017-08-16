﻿// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace XF.Quality
{
    public interface IProcessor<T> where T : class, new()
    {
        bool Initialize();
        void Execute(T t);
        void Teardown();
        void SetSuccessor(IProcessor<T> successor);
    }
}
