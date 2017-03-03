

namespace Cyclops.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Web;
    using XF.Common;

    //[Authorize(Roles="guest,member,admin")]
    public abstract class FileUploadController : Controller
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

        [HttpGet]
        public FileResult Download(int id)
        {
            FileResult result = null;
            if (id > 0)
            {
                var c = Criterion.GenerateStrategy("by.documentid");
                c.AddItem("DocumentId", id);
                //var criterion = new Criterion("DocumentId", id);
                var response = Service.Get<Artifact>(c);
                if (!response.IsOkay)
                {
                    result = File("", System.Net.Mime.MediaTypeNames.Application.Octet);
                }
                else
                {
                    if (System.IO.File.Exists(response.Model.Location))
                    {
                        result = File(response.Model.Location, response.Model.Mime, response.Model.OriginalFilename);
                    }
                    else
                    {
                        result = null;
                    }
                }
               
            }

            return result;
        }



        [HttpPost]
        public ActionResult Upload(ArtifactViewModel viewModel)
        {

            ActionResult result = null;

            var validMimes = new string[] { "image/png", "image/jpeg", "image/gif" };


            if (viewModel.IsValid())
            {
                var m = viewModel.ToModel();
                m.Mime = viewModel.FileUpload.ContentType;
                m.ContentLength = viewModel.FileUpload.ContentLength;
                m.OriginalFilename = viewModel.FileUpload.FileName;

                m.Id = Guid.NewGuid();

                var fileExtension = m.OriginalFilename.Substring(m.OriginalFilename.LastIndexOf('.'));

                if (viewModel.FileUpload != null && viewModel.FileUpload.ContentLength > 0)
                {
                    var filename = m.Id.ToString().Trim(new char[] { '{', '}' }) + fileExtension ;
                        var uploadDirectory = "~/app_files/file-uploads/";
                    string folderpath = Path.Combine(Server.MapPath(uploadDirectory), m.Mime.Replace('/', '-').Replace('+','-'));
                    if (!Directory.Exists(folderpath))
                    {
                        Directory.CreateDirectory(folderpath);
                    }
                    //m.Location = Path.Combine(Server.MapPath(uploadDirectory), filename);
                    m.Location = Path.Combine(Server.MapPath(uploadDirectory), m.Mime.Replace('/', '-').Replace('+','-'), filename);
                    var response = Service.Post<Artifact>(m);
                    if (response.IsOkay)
                    {
                        viewModel.FileUpload.SaveAs(m.Location);
                    }


                }
                result = RedirectToAction("index", new { id = viewModel.ArtifactScopeId });
            }
            else
            {
                result = RedirectToAction("index", viewModel.ArtifactScopeId);
            }


            return result;
        }



    }
}
