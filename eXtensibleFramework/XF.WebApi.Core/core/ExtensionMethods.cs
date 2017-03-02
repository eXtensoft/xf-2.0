// <copyright company="Recorded Books, Inc" file="ExtensionMethods.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Controllers;
    using System.Web.Http.ValueProviders;
    using System.Web.Http.ValueProviders.Providers;
    using System.Xml;
    using System.Xml.Serialization;
    using XF.Common;

    public static class ExtensionMethods
    {
        private static IList<string> routeData = new List<string>()
        {
            "MS_HttpRouteData",
            "ms_httproutedata",
            "{C0E50671-A1D4-429E-93C9-2AA63779924F}",
            "{c0e50671-a1d4-429e-93c9-2aa63779924f}",

        };

        private static IDictionary<string,object> ToDictionaryOut(this System.Net.Http.HttpRequestMessage message)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();

            foreach (var item in message.Properties)
            {
                if(routeData.Contains(item.Key.ToLower()))
                {
                    RouteDataValueProvider provider = item.Value as RouteDataValueProvider;
                    if (provider != null)
                    {
                        foreach (var kvp in provider.GetKeysFromPrefix(""))
                        {
                            string key = kvp.Key;
                            ValueProviderResult vpr = (ValueProviderResult)provider.GetValue(key);
                            object o = vpr.RawValue;
                            if (key.Equals("controller", StringComparison.OrdinalIgnoreCase))
                            {
                                d.Add("xf-request.route-data.controller", String.Format("{0}.Controller",o.ToString()));
                            }
                            else
                            {
                                if (o.GetType().IsClass)
                                {
                                    d.Add(String.Format("xf-request.route-data.{0}", key), vpr.AttemptedValue);
                                }
                                else
                                {
                                    d.Add(String.Format("xf-request.route-data.{0}", key), o);
                                }
                            }

                        }
                    }
                }
                else if (item.Key.Equals("MS_HttpActionDescriptor"))
                {
                    ReflectedHttpActionDescriptor descriptor = item.Value as ReflectedHttpActionDescriptor;
                    if (descriptor != null && descriptor.ReturnType != null && !String.IsNullOrWhiteSpace(descriptor.ActionName))
                    {
                        d.Add("xf-request.controller-method.name", descriptor.ActionName);
                        d.Add("xf-request.controller-method.return-type", descriptor.ReturnType.Name);
                    }
                }
            }
            if (actions.Contains(message.Method.ToString().ToLower()))
            {
                var ctx = message.Properties["MS_HttpContext"] as HttpContextWrapper;
                if (ctx != null && ctx.Request.ContentLength > 0)
                {
                    using (var stream = new System.IO.MemoryStream())
                    {
                        ctx.Request.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                        ctx.Request.InputStream.CopyTo(stream);
                        string body = Encoding.UTF8.GetString(stream.ToArray());
                        d.Add("xf-request.message.body", body);
                    }
                }
            }
            var responseCtx = message.Properties["MS_HttpContext"] as HttpContextWrapper;
            int status = responseCtx.Response.StatusCode;
            string statusDescription = responseCtx.Response.StatusDescription;
            d.Add("xf-request.response.status-code", responseCtx.Response.StatusCode.ToString());
            d.Add("xf-request.response.status-description", responseCtx.Response.StatusDescription);
            return d;
        }


        private static IList<string> actions = new List<string>(){"post","put","delete"};

        public  static IDictionary<string,object> ToDictionary(this System.Net.Http.HttpRequestMessage message, bool isBegin)
        {
                if(isBegin)
                {
                    return ToDictionaryIn(message);
                }
                else
                {
                    return ToDictionaryOut(message);
                }
        }

        public static IDictionary<string,object> ToDictionaryIn(this System.Net.Http.HttpRequestMessage message)
        {

            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("xf-request.message.uri", message.RequestUri.ToString());
            d.Add("xf-request.message.local-path", message.RequestUri.LocalPath.Trim(new char[] { '/' }).ToLower());
            d.Add("xf-request.message.http-method", message.Method.Method.ToUpper());


            if (message.Headers.Authorization != null)
            {
                d.Add("xf-request.message.auth-schema", message.Headers.Authorization.Scheme);
                d.Add("xf-request.message.auth-value", message.Headers.Authorization.Parameter);
                if (message.Headers.Authorization.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
                {
                    d.Add("xf-request.message.basic-token", message.Headers.Authorization.Parameter);
                }
                if (message.Headers.Authorization.Scheme.Equals("bearer",StringComparison.OrdinalIgnoreCase))
                {
                    d.Add("xf-request.message.bearer-token", message.Headers.Authorization.Parameter);
                }
            }
            else
            {
                d.Add("xf-request.message.auth-schema", "none");
                d.Add("xf-request.message.auth-value", "none");
            }
            d.Add("xf-request.message.protocol", message.RequestUri.Scheme.ToLower());
            
            string host = message.RequestUri.Host.ToLower();
            d.Add("xf-request.message.host", host.ToLower());
            var parts = host.Split(new char[] { '.' });
            if (parts.Length > 1)
            {
                d.Add("xf-request.message.sub-domain", parts[0].ToLower());
            }
            if (message.Properties.ContainsKey("MS_HttpContext"))
            {
                var context = ((System.Web.HttpContextWrapper)message.Properties["MS_HttpContext"]);
                if (!String.IsNullOrEmpty(context.Request.UserHostAddress))
                {
                    d.Add("xf-request.message.client-ip", context.Request.UserHostAddress);
                }
                if (!String.IsNullOrWhiteSpace(context.Request.UserAgent))
                {
                    d.Add("xf-request.message.user-agent", context.Request.UserAgent);
                }
                
            }

            foreach (var item in message.Headers)
            {
                d.Add(String.Format("xf-request.header.{0}", item.Key.ToLower()), item.Value);
            }

            foreach (var item in message.Properties)
            {

                if (item.Key.Equals("{8572540D-3BD9-46DA-B112-A1E6C9086003}", StringComparison.OrdinalIgnoreCase))
                {
                    QueryStringValueProvider provider = item.Value as QueryStringValueProvider;
                    if (provider != null)
                    {
                        foreach (var kvp in provider.GetKeysFromPrefix(""))
                        {
                            string key = kvp.Key;
                            ValueProviderResult vpr = (ValueProviderResult)provider.GetValue(key);
                            d.Add(String.Format("xf-request.query-string.{0}", key), vpr.AttemptedValue);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string,string> kvp in message.GetQueryNameValuePairs())
            {
                string key = String.Format("xf-request.query-string.{0}", kvp.Key);
                d.Add(key, kvp.Value);
            }


            return d;
        }
    
        public static void Start(this eXtensibleClaimsPrincipal principal, HttpRequestMessage request)
        {
            principal.Initialize(request, true);
        }

        public static void Stop(this eXtensibleClaimsPrincipal principal, HttpRequestMessage request, bool isSuccessful = true)
        {
            principal.Items.Add(new TypedItem("xf-request.end", DateTime.Now.ToString(XFWebApiConstants.TimeFormat)));
            principal.StopWatch.Stop();
            TimeSpan elapsed = principal.StopWatch.Elapsed;
            principal.Items.Add(new TypedItem("xf-request.milliseconds-elapsed", elapsed.TotalMilliseconds));

            principal.Initialize(request, false);
        }

        public static void Initialize(this eXtensibleClaimsPrincipal principal, HttpRequestMessage request, bool isStart)
        {
            var items = isStart ? request.ToDictionary(true) : request.ToDictionary(false);
            foreach (var item in items)
            {
                string[] arr = item.Value as string[];
                if (arr != null)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i > 0)
                        {
                            sb.Append("|");
                        }
                        sb.Append(arr[i]);
                    }
                    principal.AddItem(item.Key, sb.ToString());
                }
                else
                {
                    principal.AddItem(item.Key, item.Value);

                }                
            }
        }


        //public static string ToXmlString(this ApiRequest model)
        //{
        //    Type t = model.GetType();
        //    XmlDocument xml = new XmlDocument();

        //    xml.PreserveWhitespace = false;
        //    XmlSerializer serializer = new XmlSerializer(t);
        //    using (MemoryStream stream = new MemoryStream())
        //    using (StreamWriter writer = new StreamWriter(stream))
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        serializer.Serialize(writer, model);
        //        stream.Position = 0;
        //        xml.Load(stream);
        //    }
        //    XmlDeclaration declaration;
        //    if (xml.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
        //    {
        //        declaration = (XmlDeclaration)xml.FirstChild;
        //        declaration.Encoding = "UTF-16";
        //    }
        //    return xml.InnerXml;
        //}

    }

}
