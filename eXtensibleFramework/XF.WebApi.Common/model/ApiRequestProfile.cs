// <copyright company="eXtensoft, LLC" file="ApiRequestProfile.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract(Namespace ="")]
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
