

namespace Cyclops.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.Common;
    using System.Dynamic;
    using Cyclops.Web;

    public class ServerAppsController : FileUploadController
    {
        // GET: ServerApps
        [HttpGet]
        public ActionResult Index(Nullable<int> id)
        {
            if (id != null && id.HasValue)
            {
                return RedirectToAction("Details", new { id = id.Value });
            }
            else
            {

                var response = Service.GetAll<ServerApp>(null);
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    var vms = from x in response select new ServerAppViewModel(x);
                    return View(vms);
                }

            }
        }

        public ActionResult Details(int id, Nullable<int> serverapps)
        {
            var criterion = new Criterion("ServerAppId", id);
            var response = Service.Get<ServerApp>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                var vm = new ServerAppViewModel(response.Model) {};
                var qs = Request.QueryString;
                if (qs != null && qs.AllKeys.Count() > 0)
                {
                    int sid;
                    string idCandidate = qs[qs.AllKeys[0]];
                    if (!String.IsNullOrWhiteSpace(idCandidate) && Int32.TryParse(idCandidate, out sid))
                    {
                        vm.BackUrl = String.Format("{0}?id={1}", qs.Keys[0], idCandidate);
                        vm.SolutionId = sid;
                    }
                    
                }

                return View(vm);
            }
        }

        // GET: ServerApps/Create
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create(string data)
        {
            // reg-app-zone
            dynamic o = new ExpandoObject();
            ServerAppViewModel vm = new ServerAppViewModel();
            if (!String.IsNullOrWhiteSpace(data))
            {
                int scopeId, appId, zoneId;
                string[] t = data.Split(new char[] { '-' });
                if (t.Length.Equals(4) && Int32.TryParse(t[0], out scopeId) && Int32.TryParse(t[1], out appId) && Int32.TryParse(t[2], out zoneId))
                {
                    vm.Scope = SelectionConverter.Convert(scopeId);
                    vm.ScopeId = scopeId;
                    vm.App = SelectionListUtility.GetAppDictionary()[appId];
                    vm.AppId = appId;
                    vm.Zone = SelectionListUtility.GetZoneDictionary()[zoneId];
                    vm.ZoneId = zoneId;
                    ViewBag.SolutionId = t[3];
                    return View(vm);
                }
            }
            return View(data);
        }

        // POST: ServerApps/Create
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(ServerAppViewModel viewModel)
        {
            int solutionId = viewModel.SolutionId;
            ServerApp model = viewModel.ToModel();
            var response = Service.Post<ServerApp>(model);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                //return RedirectToAction("Index", "SolutionApps", new { id = solutionId });
                return RedirectToAction("", "SolutionApps", new { id = solutionId });
            }

        }

        // GET: ServerApps/Edit/5
        //[Authorize(Roles = "admin")]
        public ActionResult Edit(int id, Nullable<int> solutionapps)
        {
            var criterion = new Criterion("ServerAppId", id);
            var response = Service.Get<ServerApp>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                int i = solutionapps.HasValue ? solutionapps.Value : -1;
                return View(new ServerAppViewModel(response.Model) { SolutionId = i});
            }
        }

        // POST: ServerApps/Edit/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(ServerAppViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Put<ServerApp>(viewModel.ToModel(), null);
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    return RedirectToAction("Details", new { id = viewModel.ServerAppId, solutionapps = viewModel.SolutionId });
                }
            }
        }

        [HttpPost]
        public ActionResult Upload(int id)
        {
            foreach (string file in Request.Files)
            {
                int fileId;
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

                FileInfo info = new FileInfo(savedFileName);
                //string cn = "Data Source=107.23.84.86;Initial Catalog=DevOpsData;User ID=ApiAdmin;Password=1clIck8PI";
                string cn = "Data Source=(local);Initial Catalog=OpsData;Integrated Security=True;";
                MetaFile model = new MetaFile();
               // model.AddedBy = User.Identity.GetUserName();
                model.AddedAt = DateTimeOffset.Now;
                model.Name = info.Name;
                model.Filepath = info.FullName;
                model.Extension = info.Extension;
                model.Size = info.Length;
                model.Mime = "mime";
                model.MimeId = 10;
                model.TagText = "<root/>";
                model.FileGuid = Guid.NewGuid();
                model.FileType = "config";
                //if (SqlFileStreamer.TryStore(model, cn, out fileId))
                //{
                //    InSituFile sfile = new InSituFile()
                //    {
                //        FileId = fileId,
                //        ModelId = id,
                //        ModelType = "server-app",
                //        Filename = info.Name,
                //        Groupname = "Configuration",
                //        UploadedAt = DateTime.UtcNow,
                //        UploadedBy = model.AddedBy
                //    };
                //    var response = Service.Post<InSituFile>(sfile);
                //    if (!response.IsOkay)
                //    {
                //        return View(ErrorViewName, response.Status);
                //    }

                //}
            }
            return RedirectToAction("Details", new { id = id });
        }
    }
}
