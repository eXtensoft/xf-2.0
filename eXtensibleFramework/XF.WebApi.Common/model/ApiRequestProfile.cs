// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi.Core
{
    using System;
    using System.Runtime.Serialization;

    [DataContract( Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    [Serializable]
    public sealed class ApiRequestProfile
    {
        public string ReferringUrl { get; set; }
        public string UserAgent { get; set; }
        public string Token { get; set; }
        public string Authorization { get; set; }
        public string Url { get; set; }
        public string[] Headers { get; set; }
        public string RequestBody { get; set; }
        public string Controller { get; set; }
        public string ControllerMethod { get; set; }
        public string UseCase { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }
    }

}
