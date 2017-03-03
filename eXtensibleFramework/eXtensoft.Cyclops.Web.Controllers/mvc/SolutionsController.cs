

namespace Cyclops.Controllers
{
    using Cyclops.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.Common;

    public class SolutionsController : ServiceController
    {
        // GET: Solution
        public ActionResult Index()
        {
            var response = Service.GetAll<Solution>(null);
            if (!response.IsOkay)
            {
                return View("Error", response.Status);
            }
            else
            {
                var vms = from x in response select new SolutionViewModel(x);
                return View(vms);
            }
        }

        // GET: Solution/Details/5
        public ActionResult Details(int id)
        {
            Criterion c = new Criterion("SolutionId", id);
            var response = Service.Get<Solution>(c);
            if (!response.IsOkay)
            {
                return View("Error", response.Status);
            }
            else
            {
                var vm = new SolutionViewModel(response.Model);
                return View(vm);
            }
        }

        // GET: Solution/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Solution/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(SolutionViewModel vm)
        {
            if (!vm.IsValid())
            {
                return View("Create");
            }
            else
            {

                var response = Service.Post<Solution>(vm.ToModel());
                if (!response.IsOkay)
                {
                    return View("Error", response.Status);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddApp(int solutionId, int appId)
        {

            SolutionApp model = new SolutionApp()
            {
                SolutionId = solutionId,
                AppId = appId
            };
            var response = Service.Post<SolutionApp>(model);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return RedirectToAction("Details", new { id = solutionId });
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddZone(SolutionZoneViewModel viewModel)
        {

            SolutionZone model = viewModel.ToModel();

            var response = Service.Post<SolutionZone>(model);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return RedirectToAction("Details", new { id = model.SolutionId });
            }
        }

        // GET: Solution/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            Criterion c = new Criterion("SolutionId", id);
            var response = Service.Get<Solution>(c);
            if (!response.IsOkay)
            {
                return View("Error", response.Status);
            }
            else
            {
                var vm = new SolutionViewModel(response.Model);
                return View(vm);
            }


        }

        // POST: Solution/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(SolutionViewModel viewModel)
        {
            if (viewModel.IsValid())
            {
                var response = Service.Put<Solution>(viewModel.ToModel(), null);
                if (!response.IsOkay)
                {
                    return View("Error", response.Status);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        // GET: Solution/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Solution/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(SolutionViewModel viewModel)
        {
            var c = new Criterion("SolutionId", viewModel.SolutionId);
            var response = Service.Delete<Solution>(c);
            if (!response.IsOkay)
            {
                return View("Error", response.Status);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}

