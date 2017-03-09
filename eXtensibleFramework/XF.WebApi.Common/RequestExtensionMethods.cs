// <copyright company="eXtensoft, LLC" file="RequestExtensionMethods.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi
{
    using XF.Common;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

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
