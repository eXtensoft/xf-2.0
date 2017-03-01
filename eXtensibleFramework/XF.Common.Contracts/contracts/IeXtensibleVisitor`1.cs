// <copyright company="eXtensoft, LLC" file="IeXtensibleVisitor_1.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IeXtensibleVisitor<T> : IeXtensibleVisitor where T : class, new()
    {
        void Visit(T t);
    }

}
