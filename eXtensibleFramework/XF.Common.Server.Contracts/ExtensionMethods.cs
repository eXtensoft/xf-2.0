// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Data;
    using System.Linq;

    public static class ExtensionMethods
    {
        public static bool FieldExists(this IDataReader reader, string fieldName)
        {
            reader.GetSchemaTable().DefaultView.RowFilter = String.Format("ColumnName= '{0}'", fieldName);
            return (reader.GetSchemaTable().DefaultView.Count > 0);
        }

        public static string[] GetFields(this IDataReader reader)
        {
            return (from datarow in reader.GetSchemaTable().AsEnumerable() select datarow["ColumnName"].ToString()).ToArray<string>();
        }
    }

}
