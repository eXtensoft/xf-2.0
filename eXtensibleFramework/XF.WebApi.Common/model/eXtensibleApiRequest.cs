using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.WebApi.Core
{
    [Serializable]
    public class eXtensibleApiRequest
    {
        public string HandlerKey { get; private set; }
        public string Data { get; set; }

        public string Zone { get; set; }
        public string AppKey { get; set; }
        public string InstanceId { get; set; }
        public string Start { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public decimal Elapsed { get; set; }

        public string AuthSchema { get; set; }
        public string AuthValue { get; set; }

        public Guid MessageId { get; set; }
        public string Uri { get; set; }
        public string LocalPath { get; set; }
        public string HttpMethod { get; set; }
        public string Protocol { get; set; }
        public string Host { get; set; }
        public string ClientIP { get; set; }
        public string UserAgent { get; set; }

        public string[] Headers { get; set; }

        public string ControllerName { get; set; }
        public string ControllerMethod { get; set; }
        public string MethodReturnType { get; set; }

        public string[] RouteData { get; set; }

        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }


        public eXtensibleApiRequest()
        {

        }

        public eXtensibleApiRequest(Dictionary<string,object> items, Func<Dictionary<string,object>,string> serializer)
        {
            bool b = false;
            if (serializer != null)
            {
                try
                {
                    Data = serializer.Invoke(items);
                    b = true;
                }
                catch
                {

                }
                
            }
            if (!b)
            {
                Data = SerializeData(items);
            }
            SetProperties(items);


        }

        private void SetProperties(Dictionary<string, object> items)
        {
            if (items.ContainsKey("api.extensible.list"))
            {
                HandlerKey = items.Get<string>("api.extensible.list");
               
            }
            if (items.ContainsKey("xf-request.start"))
            {
                string start = items.Get<string>("xf-request.start");
                Start = start;
                DateTime dte;
                if (DateTime.TryParse(start, out dte))
                {
                    Begin = dte;
                }
            }
            if (items.ContainsKey("xf-request-end"))
            {
                string end = items.Get<string>("xf-request-end");
                DateTime dte;
                if (DateTime.TryParse(end, out dte))
                {
                    End = dte;
                }
            }
            if (items.ContainsKey("xf-request-milliseconds-elapsed"))
            {
                string elapsed = items.Get<string>("xf-request-milliseconds-elapsed");
                Decimal d;
                if (Decimal.TryParse(elapsed, out d))
                {
                    Elapsed = d;
                }
            }
            if (items.ContainsKey("xf-request.message.auth-schema"))
            {
                AuthSchema = items.Get<string>("xf-request.message.auth-schema");
            }
            if (items.ContainsKey("xf-request.message.auth-value"))
            {
                AuthValue = items.Get<string>("xf-request.message.auth-value");
            }
            if (items.ContainsKey("xf-request.id"))
            {
                Guid id = items.Get<Guid>("xf-request.id");
                if (id != null && id != Guid.Empty)
                {
                    MessageId = id;
                }
            }
            if (items.ContainsKey("xf-request.message.uri"))
            {
                Uri = items.Get<string>("xf-request.message.uri");
            }
            if (items.ContainsKey("xf-request.message.local-path"))
            {
                LocalPath = items.Get<string>("xf-request.message.local-path");
            }
            if (items.ContainsKey("xf-request.message.http-method"))
            {
                HttpMethod = items.Get<string>("xf-request.message.http-method").ToLower();
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
                StatusCode = items.Get<string>("xf-request.response.status-code");
            }

            if (items.ContainsKey("xf-request.response.status-description"))
            {
                StatusDescription = items.Get<string>("xf-request.response.status-description");
            }

            var headers = items.Where(x => x.Key.Contains("request.header"));
            List<string> hlist = new List<string>();
            foreach (var header in headers)
            {
                hlist.Add(String.Format("{0} {1}", header.Key.Trim(), header.Value.ToString().Trim()));
            }
            if (hlist.Count > 0)
            {
                Headers = hlist.ToArray();
            }

            var routedata = items.Where(x => x.Key.Contains("route-data"));
            List<string> rdlist = new List<string>();
            foreach (var data in routedata)
            {
                rdlist.Add(String.Format("{0} {1}", data.Key.Trim(), data.Value.ToString().Trim()));
            }
            if (rdlist.Count > 0)
            {
                RouteData = rdlist.ToArray();
            }


        }

        private static string format = "\"{0}\":\"{1}\"";

        private static string SerializeData(Dictionary<string,object> items)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            int i = 0;
            foreach (var item in items)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                if (item.Value != null)
                {
                    sb.Append(String.Format(format,item.Key,item.Value.ToString()));
                    i++;
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
