

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

    public class SelectionsController : ServiceController
    {
        public ActionResult Index()
        {
            var response = Service.GetAll<Selection>(null);
            if (!response.IsOkay)
            {
                return View(ErrorViewName,response.Status);
            }
            else
            {
                var vm = from x in response select new SelectionViewModel(x);
                return View(vm);
            }
        }

        // GET: Apps/Edit/5
        //[Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var criterion = new Criterion("SelectionId", id);
            var response = Service.Get<Selection>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new SelectionViewModel(response.Model));
            }
        }

        // POST: Apps/Edit/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(SelectionViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Put<Selection>(viewModel.ToModel(), null);
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

        // GET: Apps/Details/5
        public ActionResult Details(int id)
        {
            var criterion = new Criterion("SelectionId", id);
            var response = Service.Get<Selection>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new SelectionViewModel(response.Model));
            }
        }






    }
}
