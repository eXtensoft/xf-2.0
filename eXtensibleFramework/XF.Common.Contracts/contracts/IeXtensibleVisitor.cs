// <copyright company="eXtensoft, LLC" file="IeXtensibleVisitor.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IeXtensibleVisitor
    {
        void Visit(IEnumerable<TypedItem> items);
    }

}
