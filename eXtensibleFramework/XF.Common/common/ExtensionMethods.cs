// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Extensions
{
    using System;
    using XF.Common;


    public static class ExtensionMethods
    {

        public static eXMetric ToMetric<T>(this IResponse<T> response) where T : class, new()
        {
            Message<T> message = (Message<T>)response;

            eXMetric item = new eXMetric(message.Items);

            return item;
        }

        public static string Truncate (this string input,int maxLength)
        {
            if (!String.IsNullOrWhiteSpace(input) && input.Length > maxLength)
            {
                return input.Substring(0, maxLength);
            }
            return input;
        }

    }
}
