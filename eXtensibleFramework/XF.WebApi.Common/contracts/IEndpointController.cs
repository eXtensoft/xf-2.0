using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace XF.WebApi
{
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
