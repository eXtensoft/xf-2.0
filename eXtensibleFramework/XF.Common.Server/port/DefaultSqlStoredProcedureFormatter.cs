// <copyright company="eXtensible Solutions, LLC" file="DefaultSqlStoredProcedureFormatter.cs">
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

    [Serializable]

    public class DefaultSqlStoredProcedureFormatter : ISqlStoredProcedureFormatter
    {

        string ISqlStoredProcedureFormatter.ApplicationContext
        {
            get { return "demo"; }
        }

        IEnumerable<string> ISqlStoredProcedureFormatter.Schemas
        {
            get { return new string[]{"dbo","arc","all"}; }
        }

        string ISqlStoredProcedureFormatter.ComposeFormat<T>(ModelActionOption modelActionOption)
        {
            T t = new T();
            string model = t.GetType().Name;
            string format = "{0}:{1}:";
            return String.Format(format, model, modelActionOption.ToString());
        }

        string ISqlStoredProcedureFormatter.ComposeFormat(ModelActionOption modelActionOption, string typename)
        {
            string format = "{0}:{1}:";
            return String.Format(format, typename, modelActionOption.ToString());
        }
    }
}
