

namespace Cyclops.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.Common;
    using Cyclops.Web;

    public class ServersController : ServiceController
    {
        // GET: Server
       
        public ActionResult Index(Nullable<int> id)
        {
            AlertWriter.Alert("alertTitle", "alertMessage", ScaleOption.High, ScaleOption.High,eXtensibleConfig.GetProperties());
            if (IsSearch())
            {
                return Search();
            }
            if (id != null && id.HasValue)
            {
                return RedirectToAction("Details", new { id = id.Value });
            }
            else
            {
                string sortby;
                var c = GetParameters(Request, out sortby);

                var response = Service.GetAll<Server>(c);
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    var unsorted = from x in response
                                   select new ServerViewModel(x);
                    var items = Sort(unsorted, sortby);
                    return View(items);
                }
            }

        }

        protected override ActionResult Search()
        {
            string key = ResolveSearchKey(SearchInput);
            var c = Criterion.GenerateStrategy("search");
            c.AddItem("q", SearchInput);
            c.AddItem("key", key);
            var response = Service.GetAll<Server>(c);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                var unsorted = from x in response
                               select new ServerViewModel(x);
                return View(unsorted);
            }

        }



        protected override string ResolveSearchKey(string input)
        {
            bool isIP = true;
            string output = "q";
            if (!String.IsNullOrWhiteSpace(input))
            {
                var t = input.ToCharArray();
                for (int i = 0;isIP && i < t.Count(); i++)
                {
                    Char c = t[i];
                    isIP = c.Equals('.') || Char.IsDigit(c);
                }
                if (isIP)
                {
                    output = "IP";
                }
                else
                {
                    output = "Alpha";
                }
            }


            return output;
        }

        // GET: Server/Details/5
        ////[Authorize(Roles = "member")]
        public ActionResult Details(int id, Nullable<int> solutionapps)
        {
            var criterion = new Criterion("ServerId", id);
            var response = Service.Get<Server>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {

                var vm = new ServerViewModel(response.Model) { };
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

        // GET: Server/Create
       // //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Server/Create
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(ServerViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Post<Server>(viewModel.ToModel());
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddApp(ServerViewModel viewModel)
        {

            ServerApp model = new ServerApp()
            {
                ServerId = viewModel.ServerId,
                AppId = viewModel.AppId,
                ZoneId = !String.IsNullOrWhiteSpace(viewModel.Zone) ? SelectionConverter.ConvertToId(viewModel.Zone) : 0,
                Folderpath = "d:\\none",
                BackupFolderpath = "c:\\none"
            };

            var response = Service.Post<ServerApp>(model);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return RedirectToAction("Details", new { id = viewModel.ServerId });
            }

        }

        //[Authorize(Roles = "admin")]
        // GET: Server/Edit/5
        public ActionResult Edit(int id)
        {
            var criterion = new Criterion("ServerId", id);
            var response = Service.Get<Server>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new ServerViewModel(response.Model));
            }
        }

        // POST: Server/Edit/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(ServerViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Put<Server>(viewModel.ToModel(), null);
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        // GET: Server/Delete/5
        //[Authorize(Roles = "admin")]
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            var criterion = new Criterion("ServerId", id);
            var response = Service.Get<Server>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new ServerViewModel(response.Model));
            }
        }

        // POST: Server/Delete/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var criterion = new Criterion("ServerId", id);
            var response = Service.Delete<Server>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }




        private static IEnumerable<ServerViewModel> Sort(IEnumerable<ServerViewModel> list, string sortby)
        {
            IEnumerable<ServerViewModel> sorted = null;
            switch (sortby)
            {
                case "domain":
                    sorted = list.OrderBy(x => x.TLD);
                    break;
                case "os":
                    sorted = list.OrderBy(x => x.OperatingSystem);
                    break;
                case "zone":
                    sorted = list.OrderBy(x => x.Zone);
                    break;
                case "platform":
                    sorted = list.OrderBy(x => x.HostPlatform);
                    break;
                case "name":
                    sorted = list.OrderBy(x => x.Name);
                    break;
                case "usage":
                    sorted = list.OrderBy(x => x.Usage);
                    break;
                default:
                    sorted = list.OrderBy(x => x.TLD);
                    break;
            }
            return sorted;
        }



    }
}
