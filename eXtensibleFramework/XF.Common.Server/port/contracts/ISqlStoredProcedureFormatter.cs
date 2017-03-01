// <copyright company="eXtensible Solutions, LLC" file="ISqlStoredProcedureFormatter.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISqlStoredProcedureFormatter 
    {
        string ApplicationContext { get; }
        IEnumerable<string> Schemas { get; }
        string ComposeFormat<T>(ModelActionOption modelActionOption) where T : class, new();
        string ComposeFormat(ModelActionOption modelActionOption, string typename);
    }
}
