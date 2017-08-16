// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using Common;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Xml.Linq;
    using XF.Common;

    public class HttpMessageProvider
    {

        private static List<ApiHttpStatusCode> statusCodes;

        private static ConcurrentDictionary<int, ApiHttpStatusCode> maps = new ConcurrentDictionary<int, ApiHttpStatusCode>();

        public bool IsInitialized { get; set; }

        public virtual HttpStatusCode DefaultErrorCode { get { return HttpStatusCode.NotImplemented; } }

        #region ErrorCodeWhitelist (List<ApiHttpStatusCode>)

        private static List<ApiHttpStatusCode> _ErrorCodeWhitelist;

        /// <summary>
        /// Gets or sets the List<ApiHttpStatusCode> value for ErrorCodeWhitelist
        /// </summary>
        /// <value> The List<ApiHttpStatusCode> value.</value>

        public List<ApiHttpStatusCode> ErrorCodeWhitelist
        {
            get 
            {
                return _ErrorCodeWhitelist;
            }

        }

        #endregion

        static HttpMessageProvider()
        {
            InitializeCodes();           
        }

        private static void InitializeCodes()
        {
            
            try
            {
                XDocument xdoc = XDocument.Parse(HttpResources.httpstatuscodes);
                statusCodes = (from x in xdoc.Descendants("HttpCode")
                                                let y = x
                                                select new ApiHttpStatusCode()
                                                {
                                                
                                                    Code = Int32.Parse(x.Attribute("code").Value),
                                                    Name = x.Attribute("name").Value,
                                                    Summary = (x.Attribute("sys.net").Value).Trim(),
                                                    IsSuccess = Boolean.Parse(x.Attribute("success").Value)
                                                   
                                                }).ToList();
            }
            catch
            {
                statusCodes = new List<ApiHttpStatusCode>();
            }

            string candidates = ConfigurationProvider.AppSettings["api.httpcodes.whitelist"];
            List<int> codes = new List<int>();
            if (!String.IsNullOrWhiteSpace(candidates))
            {
                string[] t = candidates.Split(new char[] { ',', ';', '.' });
                foreach (var item in t)
                {
                    int j;
                    if (Int32.TryParse(item,out j))
                    {
                        codes.Add(j);
                    }
                }
            }
            else
            {
                codes.Add(200);
                codes.Add(201);
                codes.Add(202);
                codes.Add(400);
                codes.Add(401);
                codes.Add(403);
                codes.Add(404);
                codes.Add(409);
                codes.Add(500);
            }
            _ErrorCodeWhitelist = new List<ApiHttpStatusCode>();
            foreach (var code in statusCodes)
            {
                if (codes.Contains(code.Code))
                {
                    code.IsAvailable = true;
                }
                _ErrorCodeWhitelist.Add(code);
                maps.TryAdd(code.Code, code);
            }

        }



        public virtual void Initialize()
        {
            InitializeCodes();
            IsInitialized = true;
        }

        public bool IsSuccessful(HttpStatusCode statusCode)
        {
            int code = (int)statusCode;
            return (maps.ContainsKey(code) && maps[code].IsSuccess);
        }

        #region get 

        public virtual void Get(string identifier, out HttpStatusCode code)
        {
            code = HttpStatusCode.NotImplemented;
        }

        public virtual void Get(string identifier, out HttpStatusCode code, out string message)
        {
            code = HttpStatusCode.NotImplemented;
            message = HttpStatusCode.NotImplemented.ToString();
        }

        public virtual void Get(string identifier, out HttpStatusCode code, out string message, object[] messageParameters)
        {
            code = HttpStatusCode.NotImplemented;
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("errorIdentifier:{0}", identifier));
            for (int i = 0; i < messageParameters.Length; i++)
            {
                string s = messageParameters[i].ToString();
                sb.Append(String.Format(";p{0}={1}", i + 1, s));
            }
            message = sb.ToString();
        }


        #endregion

        #region get error

        public virtual void GetError<T>(HttpStatusCode statusCode, T t, out string message)
        {
            // tag if statusCode is not allowed
            message = statusCode.ToString();
        }

        public virtual void GetError(string identifier, out HttpStatusCode errorCode, out string message)
        {
            errorCode = HttpStatusCode.NotImplemented;
            message = HttpStatusCode.NotImplemented.ToString();
        }

        public virtual void GetError(string errorIdentifier, object[] messageParameters, out HttpStatusCode errorCode, out string message)
        {
            errorCode = HttpStatusCode.NotImplemented;
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("errorIdentifier:{0}", errorIdentifier));
            for (int i = 0; i < messageParameters.Length; i++)
            {
                string s = messageParameters[i].ToString();
                sb.Append(String.Format(";p{0}={1}", i + 1, s));
            }
            message = sb.ToString();
        }

        #endregion

 


        public virtual bool VetStatusCode(HttpStatusCode candidateStatusCode)
        {
            bool b = false;
            int j = (int)candidateStatusCode;
            if (maps.ContainsKey(j))
            {
                b = maps[j].IsAvailable;
            }
            if (!b)
            {
                var principal = Thread.CurrentPrincipal as eXtensibleClaimsPrincipal;
                if (principal != null)
                {

                    principal.Items.Add(new TypedItem("xf-request.response.status-code-invalid", candidateStatusCode.ToString()));
                }
            }

            return b;
        }

        

        public string ResolvePattern(string key)
        {
            return key;
        }

        public string ComposeMessage(string formatPattern, params object[] messageParameters)
        {
            string s = String.Empty;
            try
            {
                s = String.Format(formatPattern, messageParameters);
            }
            catch
            {
                s = ComposeMessage(formatPattern,messageParameters);
            }
            return s;
        }

        private string ComposeMessage(params object[] messageParameters)
        {
            StringBuilder sb = new StringBuilder();
            if (messageParameters != null && messageParameters.Count() > 0)
            {
                for (int i = 0; i < messageParameters.Length; i++)
                {
                    string s = messageParameters[i].ToString();
                    if (i==0)
                    {
                        sb.AppendFormat("pattern:{0}", s);
                    }
                    else
                    {
                        sb.Append(",");
                    }
                    sb.Append(s);
                }
            }
            return sb.ToString();
        }



        private HttpStatusCode GetStatusCode(string key)
        {
            return HttpStatusCode.OK;
        }

        public bool VetStatusCode(HttpStatusCode statusCode, object model)
        {
            //return VetStatusCode(statusCode);
            return false;
        }

    }

}
