// <copyright company="Recorded Books, Inc" file="ExtensionMethods.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Controllers;
    using System.Web.Http.ValueProviders;
    using System.Web.Http.ValueProviders.Providers;
    using System.Xml;
    using System.Xml.Serialization;
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










        //public static HttpResponseMessage GenerateErrorResponse(this HttpRequestMessage request, string errorIdentifier, params object[] messageParameters)
        //{
        //    HttpResponseMessage response = null;

        //    string message = String.Empty;
        //    System.Net.HttpStatusCode code = System.Net.HttpStatusCode.SeeOther;
        //    if (GetErrorResponse(errorIdentifier, out code, out message, messageParameters))
        //    { 
        //        response = request.CreateErrorResponse(code,message);
        //    }
        //    else
        //    {
        //        response = request.CreateErrorResponse(code, "an error occurred");
        //    }            
        //    return response;
        //}

        //private static void GetErrorResponseMessage(string errorIdentifier, out System.Net.HttpStatusCode code, out string message)
        //{
        //    code = System.Net.HttpStatusCode.SeeOther;
        //    message = String.Empty;
        //}

        //private static bool GetErrorResponse(string errorIdentifier, out System.Net.HttpStatusCode code, out string message, params object[] messageParameters)
        //{
        //    bool b = false;
        //    WebApiCaller.GetCallerId();
        //    code = System.Net.HttpStatusCode.BadRequest;
        //    List<string> list = new List<string>();
        //    list.Add(errorIdentifier);
        //    foreach (var item in messageParameters)
        //    {
        //        list.Add(item.ToString());
        //    }
        //    b = true;
        //    // obtain the pattern for the given errorIdentifier AND messageParameter.Count,
        //    // obtain the HttpStatusCode for the given errorIdentifier;
        //    string pattern = "pattern for: {0} | {1},{2},{3}";
        //    message = String.Format(pattern, list.ToArray());
        //    return b;
        //}

        //public static HttpResponseMessage GenerateErrorResponse(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, string messagePattern, params object[] messageParameters)
        //{
        //    HttpResponseMessage response = null;

        //    string message = String.Format(messagePattern, messageParameters);
        //    response = request.CreateErrorResponse(statusCode, message);

        //    return response;
        //}

        //public static HttpResponseMessage ApiErrorResponse(this HttpRequestMessage request, string key)
        //{
        //    HttpResponseMessage response = null;
        //    response = ResponseConfiguration.MessageProvider.ComposeErrorResponse(request, key);
        //    return response;
        //}

        //public static HttpResponseMessage ApiErrorResponse(this HttpRequestMessage request, string key, params object[] messageParameters)
        //{
        //    HttpResponseMessage response = null;
        //    response = ResponseConfiguration.MessageProvider.ComposeErrorResponse(request, key,messageParameters);
        //    return response;
        //}

        //public static HttpResponseMessage ApiResponse(this HttpRequestMessage request, string key)
        //{
        //    HttpResponseMessage response = null;
        //    response = ResponseConfiguration.MessageProvider.ComposeResponse(request, key);
        //    return response;
        //}

        //public static HttpResponseMessage ApiResponse<T>(this HttpRequestMessage request, string key, T model)
        //{
        //    HttpResponseMessage response = null;
        //    response = ResponseConfiguration.MessageProvider.ComposeResponse<T>(request, key,model);
        //    return response;
        //}

        //public static HttpResponseMessage ApiResponse<T>(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, T model)
        //{
        //    HttpResponseMessage response = null;

        //    bool b = ResponseConfiguration.MessageProvider.VetStatusCode(statusCode);
        //    response = request.CreateResponse(statusCode,model);

        //    return response;
        //}

        //public static HttpResponseMessage ApiResponse(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode, object model)
        //{
        //    HttpResponseMessage response = null;

        //    bool b = ResponseConfiguration.MessageProvider.VetStatusCode(statusCode);
        //    response = request.CreateResponse(statusCode,model);
            
        //    return response;
        //}

        //public static HttpResponseMessage ApiResponse(this HttpRequestMessage request, System.Net.HttpStatusCode statusCode)
        //{
        //    HttpResponseMessage response = null;

        //    if(!ResponseConfiguration.MessageProvider.VetStatusCode(statusCode))
        //    {

        //    }
        //    response = request.CreateResponse(statusCode);

        //    return response;
        //}

    }


}
