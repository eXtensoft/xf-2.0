// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace XF.WebApi
{
    using System;
    using System.Web.Http;

    public interface IEndpointController
    {

        Guid EndpointId { get; }
        string EndpointName { get; }
        string EndpointDescription { get; }
        int EndpointVersion { get; }
        string EndpointWhitelistPattern { get; }
        string EndpointRouteTablePattern { get; }


        void RegisterApiRoute(HttpConfiguration config);

    }
}
