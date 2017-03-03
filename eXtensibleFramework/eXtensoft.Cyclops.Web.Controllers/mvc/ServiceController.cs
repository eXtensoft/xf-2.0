

namespace Cyclops.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.Common;

    [Authorize(Roles = "guest,member")]
    public abstract class ServiceController : Controller
    {
        public virtual string ErrorViewName { get { return "Error"; } }

        private IModelRequestService _Service = null;
        protected IModelRequestService Service
        {
            get
            {
                if (_Service == null)
                {
                    _Service = new PassThroughModelRequestService(
                        new DataRequestService(new XF.DataServices.ModelDataGatewayDataService())
                        );
                }
                return _Service;
            }
            set
            {
                _Service = value;
            }
        }

        public string SearchInput { get; set; }

        public string SearchKey { get; set; }

        protected bool IsSearch()
        {
            bool b = false;
            string qs = Request.QueryString.ToString();
            if (!string.IsNullOrEmpty(qs))
            {
                string[] kvps = qs.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0;!b && i < kvps.Count(); i++)
                {
                    string[] t = kvps[i].Split(new char[] { '='});

                    b = t[0].Equals("q") && !String.IsNullOrWhiteSpace(t[1]);
                    SearchInput = t[1];
                    SearchKey = ResolveSearchKey(t[1]);
                }
            }
                return b;
        }

        protected virtual string ResolveSearchKey(string input)
        {
            return "q";
        }

        protected virtual ActionResult Search()
        {
            return View(ErrorViewName);
        }

        protected ICriterion GetParameters(HttpRequestBase request)
        {
            string sort;
            return GetParameters(request, out sort);
        }

        protected ICriterion GetParameters(HttpRequestBase request, out string sortBy)
        {
            sortBy = "domain";
            Criterion c = new Criterion();
            NameValueCollection nvc = request.QueryString;
            bool b = false;

            foreach (var item in nvc.AllKeys)
            {
                if (item != null)
                {
                    string s = item.ToLower();
                    if (s.Equals("sort") && sortfields.ContainsKey(nvc[s]))
                    {
                        sortBy = sortfields[nvc[s]];
                    }
                    else if (maps.ContainsKey(s))
                    {
                        if (nonInts.Contains(s.ToLower()))
                        {
                            string key = maps[s];
                            string val = nvc[s];
                            c.AddItem(key, val);
                        }
                        else
                        {
                            string key = maps[s];
                            string val = nvc[s];
                            int intValue = SelectionConverter.ConvertToId(val);
                            c.AddItem(key, intValue);
                        }
                        b = true;
                    }
                }

            }
            c.AddItem("sort-by", sortBy);

            Criterion criterion = b ? c : null;
            return criterion;
        }

        private static IDictionary<string, string> maps = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"zone","ZoneId"},
            {"z","ZoneId"},
            {"h","HostPlatformId"},
            {"host","HostPlatformId"},
            {"platform","HostPlatformId"},
            {"p","HostPlatformId"},
            {"os","OperatingSystemId"},
            {"tags","Tags"},
            {"tag","Tags"},
            {"t","Tags"},
            {"name","Name"},
            {"n","Name"},
            {"domain","TLD"},
            {"d","TLD"},
            {"s","ServerSecurityId"},
            {"u","Usage"},
            {"usage","Usage"},
        };

        private static string[] nonInts = new string[] { "t", "tag", "tags", "name", "n", "domain", "d", "usage", "u" };

        private static IDictionary<string, string> sortfields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"domain","domain"},
            {"d","domain"},
            {"os","os"},
            {"zone","zone"},
            {"z","zone"},
            {"platform","platform"},
            {"p","platform"},
            {"n","name"},
            {"name","name"},
            {"app-type","app-type"},
            {"t","app-type"},
            {"u","usage"},
            {"usage","usage"},
        };
    }
}
