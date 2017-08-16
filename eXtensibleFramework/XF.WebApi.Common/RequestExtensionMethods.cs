// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System.Collections.Generic;
    using System.Net.Http;
    using XF.Common;

    public static class RequestExtensionMethods
    {
        public static List<TypedItem> ExtractQueryString(this HttpRequestMessage request, IEnumerable<MultiMap> maps)
        {

            return null;
        }
        public static List<TypedItem> ExtractQueryString(this HttpRequestMessage request)
        {
            return null;
        }
    }

}
