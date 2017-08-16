// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

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
