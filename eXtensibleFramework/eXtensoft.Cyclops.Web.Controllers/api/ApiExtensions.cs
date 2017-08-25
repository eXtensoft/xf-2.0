
namespace Cyclops.Api.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using XF.Common;

    public static class ApiExtensions
    {
        public static HttpStatusCode ToHttpStatusCode(this RequestStatus status)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            if (statusmaps.ContainsKey(status.Code))
            {
                code = statusmaps[status.Code];
            }
            return code;
        }

        public static string ToHttpMessage(this RequestStatus status)
        {
            return status.Description;
        }

        public static bool IsOkay(this HttpStatusCode code)
        {
            return (code == HttpStatusCode.OK);
        }

        #region HttpStatusCode mapping

        private static IDictionary<int, HttpStatusCode> statusmaps = new Dictionary<int, HttpStatusCode>
        {
            {200,HttpStatusCode.OK},
            {201,HttpStatusCode.Created},
            {202,HttpStatusCode.Accepted},
            {301,HttpStatusCode.Moved},
            {302,HttpStatusCode.Redirect},
            {303,HttpStatusCode.RedirectMethod},
            {400,HttpStatusCode.BadRequest},
            {401,HttpStatusCode.Unauthorized},
            {403,HttpStatusCode.Forbidden},
            {404,HttpStatusCode.NotFound},
            {405,HttpStatusCode.MethodNotAllowed},
            {406,HttpStatusCode.NotAcceptable},
            {500,HttpStatusCode.InternalServerError},
            {501,HttpStatusCode.NotImplemented},
            {502,HttpStatusCode.BadGateway},
            {503,HttpStatusCode.ServiceUnavailable},
        };

        #endregion


    }
}
