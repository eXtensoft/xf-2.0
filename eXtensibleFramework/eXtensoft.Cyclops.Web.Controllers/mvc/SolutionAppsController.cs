
namespace Cyclops.Controllers
{

    using Cyclops.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.Common;

    public class SolutionAppsController : ServiceController
    {


        public ActionResult ViewMatrixItem(object data)
        {
            var list = SelectionListUtility.GetServers();
            return PartialView("_MatrixItem", list);
        }
        // GET: SolutionApps
        public ActionResult Index(int id)
        {
            var zonesList = SelectionListUtility.GetReverseZoneDictionary();
            List<SolutionAppViewModel> solutionApps = new List<SolutionAppViewModel>();
            SolutionViewModel solution = null;
            Criterion c = new Criterion("SolutionId", id);
            var response = Service.Get<Solution>(c);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                solution = new SolutionViewModel(response.Model);
                solution.Zones.OrderBy(x => x);
                ViewBag.Solution = solution;

                var criterion = Criterion.GenerateStrategy("solution");
                criterion.AddItem("SolutionId", id);

                var getAllResponse = Service.GetAll<ServerApp>(criterion);
                if (!getAllResponse.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    List<ServerAppViewModel> serverapps = (from x in getAllResponse select new ServerAppViewModel(x)).ToList();
                    var appResponse = Service.GetAll<SolutionApp>(c);
                    if (appResponse.IsOkay)
                    {
                        List<SolutionAppViewModel> vms = (from y in appResponse
                                                          select new SolutionAppViewModel(y)
                                                          {
                                                              ZoneServerApps = new List<ServerAppViewModelCollection>()
                                                          }).ToList();

                        foreach (SolutionAppViewModel vm in vms)
                        {
                            SolutionAppViewModel savm = vm;
                            foreach (var zone in solution.Zones)
                            {
                                var found = serverapps.FindAll(x => x.Zone.Equals(zone, StringComparison.OrdinalIgnoreCase) && x.App.Equals(savm.Application));
                                if (found == null)
                                {
                                    vm.ZoneServerApps.Add(null);
                                }
                                else
                                {
                                    if (zonesList.ContainsKey(zone))
                                    {
                                        int zoneId = zonesList[zone];
                                        ServerAppViewModelCollection sac = new ServerAppViewModelCollection() { Key = zoneId.ToString() };
                                        sac.AddRange(found);
                                        vm.ZoneServerApps.Add(sac);
                                    }
                                }
                            }
                            solutionApps.Add(vm);
                        }
                    }
                    return View(solutionApps);
                }
            }
        }

        // GET: SolutionApps/Details/5
        public ActionResult Details(int id)
        {
            Criterion c = new Criterion("SolutionAppId", id);
            var response = Service.Get<SolutionApp>(c);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new SolutionAppViewModel(response.Model));
            }
        }
        [HttpPost]
        public ActionResult AddServerApp(ServerAppViewModel viewModel)
        {
            return RedirectToAction("Index", "SolutionApps", 1);
        }

        //[Authorize(Roles = "admin")]
        // GET: SolutionApps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolutionApps/Create
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SolutionApps/Edit/5
        //[Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SolutionApps/Edit/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SolutionApps/Delete/5
        //[Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SolutionApps/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

