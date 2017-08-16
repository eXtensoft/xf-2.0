// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using XF.Common;

    [Serializable]
    public sealed class ApiRequest
    {
        #region lists
        private static IList<string> exhaustedKeys = new List<string>()
        {
            "app.context.key",
            "app.context.instance",
            "app.context.zone",
            "xf-request.start",
            "xf-request-milliseconds-elapsed",
            "xf-request.message.auth-schema",
            "xf-request.message.auth-value",
            "xf-id",
            "xf-request.message.local-path",
            "xf-request.message.http-method",
            "xf-request.message.protocol",
            "xf-request.message.host",
            "xf-request.message.client-ip",
            "xf-request.message.user-agent",
            "xf-request.route-data.controller",
            "xf-request.controller-method.name",
            "xf-request.controller-method.return-type",
            "xf-request.response.status-code",
            "xf-request.response.status-description",
            "xf-request.basic-token",
            "xf-request.message.body",
        };

        private static IList<string> exhaustedKeyParts = new List<string>()
        {
            "request-header","route-data"
        };

        private static List<string> routeDataExclusions = new List<string>(){
                "xf-request.route-data.controller",
                "xf-request.route-data.controller-method",
                "xf-request.route-data.returntype"
                };
        #endregion

        #region properties
        [XmlAttribute("id")]
        public long ApiRequestId { get; set; }
        [XmlAttribute("appKey")]
        public string AppKey { get; set; }
        [XmlAttribute("appZone")]
        public string AppZone { get; set; }
        [XmlAttribute("instance")]
        public string AppInstance { get; set; }

        public decimal Elapsed { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Protocol { get; set; }

        public string Host { get; set; }

        public string Path { get; set; }

        public string ClientIP { get; set; }

        public string UserAgent { get; set; }

        public string HttpMethod { get; set; }

        public string ControllerName { get; set; }

        public string ControllerMethod { get; set; }

        public string MethodReturnType { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseText { get; set; }

        public Guid MessageId { get; set; }

        public string BasicToken { get; set; }

        public string BearerToken { get; set; }

        public string AuthSchema { get; set; }

        public string AuthValue { get; set; }

        public string[] Headers { get; set; }

        public string[] RouteData { get; set; }

        public string[] QueryString { get; set; }

        public string MessageBody { get; set; }

        public bool HasErrorLog { get; set; }

        [XmlIgnore]
        public string XmlData { get; set; }
        
        [XmlIgnore]
        public string RData
        { 
            get 
            {
                StringBuilder sb = new StringBuilder();
                if (RouteData != null)
                {
                    foreach (string data in RouteData)
                    {
                        if (!routeDataExclusions.Contains(data))
                        {
                            int i = data.IndexOf("::");
                            if (i > 0)
                            {
                                string left = data.Substring(0, i);
                                string[] t = left.Split(new char[] { '.' });
                                string right = data.Substring(i + 2);
                                string output = String.Format("{0} : {1}", t[t.Length - 1], right);
                                sb.AppendLine(output);                               
                            }
                            else
                            {
                                sb.AppendLine(data);
                            }

                        }
                    }
                }
                return sb.ToString();
            } 
        }
        
        public List<TypedItem> LogItems { get; set; }
        #endregion properties



        #region constructors
        public ApiRequest() { }

        public ApiRequest(Dictionary<string,object> items)
        {
            
            bool hasStart = false;
            bool hasEnd = false;
            if (items.ContainsKey("xf-custom-xf-id-log"))
            {
                HasErrorLog = true;
            }
            if (items.ContainsKey("app.context.key"))
            {
                AppKey = items.Get<string>("app.context.key");
                
            }
            if (items.ContainsKey("app.context.zone"))
            {
                AppZone = items.Get<string>("app.context.zone");
                
            }
            if (items.ContainsKey("app.context.instance"))
            {
                AppInstance = items.Get<string>("app.context.instance");
                
            }
            if (items.ContainsKey("xf-request.start"))
            {
                string start = items.Get<string>("xf-request.start");
                DateTime dte;
                if (DateTime.TryParse(start, out dte))
                {
                    Start = dte;
                    hasStart = true;
                }
            }
            if (items.ContainsKey("xf-request.end"))
            {
                string end = items.Get<string>("xf-request.end");
                DateTime dte;
                if (DateTime.TryParse(end, out dte))
                {
                    End = dte;
                    hasEnd = true;
                }
            }
            if (hasStart && hasEnd)
            {
                TimeSpan ts = End - Start;
                Decimal elapsed = Convert.ToDecimal(ts.TotalMilliseconds);
                Elapsed = elapsed;
            }

            //if (items.ContainsKey("xf-request-milliseconds-elapsed"))
            //{
            //    string elapsed = items.Get<string>("xf-request-milliseconds-elapsed");
            //    Decimal d;
            //    if (Decimal.TryParse(elapsed, out d))
            //    {
            //        Elapsed = d;
                    
            //    }
            //}

            if (items.ContainsKey("xf-request.message.auth-schema"))
            {
                AuthSchema = items.Get<string>("xf-request.message.auth-schema");
                
            }
            else
            {
                AuthSchema = "none";
            }
            if (items.ContainsKey("xf-request.message.auth-value"))
            {
                AuthValue = items.Get<string>("xf-request.message.auth-value");
                
            }
            else
            {
                AuthValue = "none";
            }
            if (items.ContainsKey("xf-id"))
            {
                Guid id = items.Get<Guid>("xf-id");

                if (id != null && id != Guid.Empty)
                {
                    MessageId = id;
                }
                
            }

            if (items.ContainsKey("xf-request.message.local-path"))
            {
                Path = items.Get<string>("xf-request.message.local-path");
                
            }
            if (items.ContainsKey("xf-request.message.http-method"))
            {
                HttpMethod = items.Get<string>("xf-request.message.http-method");
                
            }
            if (items.ContainsKey("xf-request.message.protocol"))
            {
                Protocol = items.Get<string>("xf-request.message.protocol");
                
            }
            if (items.ContainsKey("xf-request.message.host"))
            {
                Host = items.Get<string>("xf-request.message.host");
                
            }
            if (items.ContainsKey("xf-request.message.client-ip"))
            {
                ClientIP = items.Get<string>("xf-request.message.client-ip");
                
            }
            if (items.ContainsKey("xf-request.message.user-agent"))
            {
                UserAgent = items.Get<string>("xf-request.message.user-agent");
                
            }
            if (items.ContainsKey("xf-request.route-data.controller"))
            {
                ControllerName = items.Get<string>("xf-request.route-data.controller");
                
            }
            if (items.ContainsKey("xf-request.controller-method.name"))
            {
                ControllerMethod = items.Get<string>("xf-request.controller-method.name");
                
            }
            if (items.ContainsKey("xf-request.controller-method.return-type"))
            {
                MethodReturnType = items.Get<string>("xf-request.controller-method.return-type");
                
            }
            if (items.ContainsKey("xf-request.response.status-code"))
            {
                ResponseCode = items.Get<string>("xf-request.response.status-code");
                
            }

            if (items.ContainsKey("xf-request.response.status-description"))
            {
                ResponseText = items.Get<string>("xf-request.response.status-description");
                
            }
            if (items.ContainsKey("xf-request.message.basic-token"))
            {
                BasicToken = items.Get<string>("xf-request.message.basic-token");
                
            }
            if (items.ContainsKey("xf-request.message.bearer-token"))
            {
                BearerToken = items.Get<string>("xf-request.message.bearer-token");
            }

            var others = items.Where(x => !exhaustedKeys.Contains(x.Key));



            List<string> hlist = new List<string>();
            List<string> rdlist = new List<string>();
            List<string> qslist = new List<string>();
            foreach (var data in others)
            {                
                if (data.Key.Contains("request.header"))
                {                    
                    if (data.Key.ToString().Equals("referer", StringComparison.OrdinalIgnoreCase))
                    {

                        string s = System.Uri.EscapeDataString(data.Value.ToString().Trim()); 
                        string t = s.Replace('/','|');
                        hlist.Add(String.Format("{0}.s:: {1}", data.Key.Trim(), s));
                        hlist.Add(String.Format("{0}.t:: {1}", data.Key.Trim(), t));
                    }
                    hlist.Add(String.Format("{0}:: {1}", data.Key.Trim(), data.Value.ToString().Trim()));                   
                }
                else if (data.Key.Contains("route-data"))
                {
                    rdlist.Add(String.Format("{0}:: {1}", data.Key.Trim(), data.Value.ToString().Trim()));
                }
                else if (data.Key.Contains("query-string"))
                {
                    qslist.Add(String.Format("{0}:: {1}", data.Key.Trim(), data.Value.ToString().Trim()));
                }

                if (hlist.Count > 0)
                {
                    Headers = hlist.ToArray();
                }
                if (rdlist.Count > 0)
                {
                    RouteData = rdlist.ToArray();
                }
                if (qslist.Count > 0)
                {
                    QueryString = qslist.ToArray();
                }
            }


            if (items.ContainsKey("xf-request.message.body"))
            {
                MessageBody = items.Get<string>("xf-request.message.body");
                
            }


        }

        private string Encode(string s)
        {
            return System.Uri.EscapeDataString(s);
        }
        #endregion constructors

    }
}
