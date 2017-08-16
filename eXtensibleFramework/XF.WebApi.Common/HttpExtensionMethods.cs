// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using XF.Common;

    public static class HttpExtensionMethods
    {
        #region code T
        public static HttpResponseMessage GenerateResponse<T>(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, T t)
        {
            HttpResponseMessage response = null;
            if (!IsSuccessful(statusCode))
            {
                response = request.GenerateErrorResponse<T>(statusCode, t);
            }
            else
            {
                response = request.GenerateResponseInternal<T>(statusCode, t);
            }
            return response;
        }
        private static HttpResponseMessage GenerateResponseInternal<T>(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, T t)
        {
            HttpResponseMessage response = null;
            ResponseConfiguration.MessageProvider.VetStatusCode(statusCode,t);
            response = request.CreateResponse<T>(statusCode, t);
            return response;
        }
        public static HttpResponseMessage GenerateErrorResponse<T>(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, T t)
        {
            EnsureMessageId(request);
            HttpResponseMessage response = null;
            ResponseConfiguration.MessageProvider.VetStatusCode(statusCode, t);
            string message;
            ResponseConfiguration.MessageProvider.GetError(statusCode,t, out message);
            response = request.CreateErrorResponse(statusCode, message);
            return response;
        }
        public static HttpResponseMessage GenerateErrorResponse(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, string message)
        {
            EnsureMessageId(request);
            HttpResponseMessage response = null;
            ResponseConfiguration.MessageProvider.VetStatusCode(statusCode);
            response = request.CreateErrorResponse(statusCode, message);
            return response;
        }

        private static void EnsureMessageId(HttpRequestMessage request)
        {
            var principal = Thread.CurrentPrincipal as eXtensibleClaimsPrincipal;
            if (principal != null)
            {
                if (!principal.Items.ContainsKey("xf-id"))
                {
                    principal.Items.Add(new TypedItem("xf-id", Guid.NewGuid()));
                }
            }

        }

        #endregion

        #region code object
        public static HttpResponseMessage GenerateResponse(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, object o)
        {
            HttpResponseMessage response = null;
            if (!IsSuccessful(statusCode))
            {
                response = request.GenerateErrorResponse(statusCode, o);
            }
            else
            {
                response = request.GenerateResponseInternal(statusCode, o);
            }
            return response;
        }

        private static HttpResponseMessage GenerateResponseInternal(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, object o)
        {
            HttpResponseMessage response = null;
            ResponseConfiguration.MessageProvider.VetStatusCode(statusCode,o);
            response = request.CreateResponse(statusCode, o);

            return response;
        }

        public static HttpResponseMessage GenerateErrorResponse(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, object o)
        {
            HttpResponseMessage response = null;
            ResponseConfiguration.MessageProvider.VetStatusCode(statusCode,o);
            response = request.CreateErrorResponse(statusCode, o.ToString());
            return response;
        }

        #endregion

        #region code
        public static HttpResponseMessage GenerateResponse(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode)
        {
            HttpResponseMessage response = null;

            if (!IsSuccessful(statusCode))
            {
                response = request.GenerateErrorResponse(statusCode, statusCode.ToString());
            }
            else
            {
                response = request.GenerateResponseInternal(statusCode);
            }

            return response;
        }

        private static HttpResponseMessage GenerateResponseInternal(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode)
        {
            HttpResponseMessage response = null;
            ResponseConfiguration.MessageProvider.VetStatusCode(statusCode);
            response = request.CreateResponse(statusCode);

            return response;
        }

        public static HttpResponseMessage GenerateResponse(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, string message)
        {
            HttpResponseMessage response = null;
            // tag if statusCode not allowed
            response = request.CreateResponse(statusCode, message);

            return response;
        }
        #endregion

        #region identifier

        public static HttpResponseMessage GenerateResponse(this HttpRequestMessage request, string identifier)
        {
            HttpResponseMessage response = null;
            
            string message;
            System.Net.HttpStatusCode code;
            ResponseConfiguration.MessageProvider.Get(identifier, out code, out message);

            response = request.CreateResponse(code, message);
            return response;
        }

        public static HttpResponseMessage GenerateResponse(this HttpRequestMessage request, string identifier, object o)
        {
            HttpResponseMessage response = null;

            System.Net.HttpStatusCode code;
            ResponseConfiguration.MessageProvider.Get(identifier, out code);

            response = request.CreateResponse(code, o);
            return response;
        }

        public static HttpResponseMessage GenerateResponse<T>(this HttpRequestMessage request, string identifier, T t)
        {
            HttpResponseMessage response = null;

            System.Net.HttpStatusCode code;
            ResponseConfiguration.MessageProvider.Get(identifier, out code);

            response = request.CreateResponse(code, t);
            return response;
        }

        public static HttpResponseMessage GenerateErrorResponse(this HttpRequestMessage request, string errorIdentifier)
        {
            HttpResponseMessage response = null;
            WebApiCaller.GetCallerId();
            string message;
            System.Net.HttpStatusCode code;
            ResponseConfiguration.MessageProvider.GetError(errorIdentifier, out code, out message);

            response = request.CreateErrorResponse(code, message);
            return response;
        }

        public static HttpResponseMessage GenerateErrorResponse(this HttpRequestMessage request, string errorIdentifier, params object[] messageParameters)
        {
            HttpResponseMessage response = null;
            WebApiCaller.GetCallerId();
            string message = String.Empty;
            System.Net.HttpStatusCode code;
            ResponseConfiguration.MessageProvider.GetError(errorIdentifier, messageParameters, out code, out message);
            response = request.CreateResponse(code, message);
            return response;
        }
        #endregion

        private static bool IsSuccessful(System.Net.HttpStatusCode statusCode)
        {
            return ResponseConfiguration.MessageProvider.IsSuccessful(statusCode);
        }

    }


}
