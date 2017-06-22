
namespace Cyclops.Controllers
{

    using Cyclops.Web;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.Common;

    public class ZonesController : ServiceController
    {
        public ActionResult Index()
        {
            var response = Service.GetAll<Zone>(null);
            if (response != null)
            {
                var vms = from x in response select new ZoneViewModel(x);
                return View(vms);
            }
            return View();
        }

        //[Authorize(Roles = "member")]
        public ActionResult Details(int id)
        {
            var criterion = new Criterion("ZoneId", id);
            var response = Service.Get<Zone>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new ZoneViewModel(response.Model));
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(ZoneViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Post<Zone>(viewModel.ToModel());
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
        // GET: Server/Edit/5
        public ActionResult Edit(int id)
        {
            var criterion = new Criterion("ZoneId", id);
            var response = Service.Get<Zone>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new ZoneViewModel(response.Model));
            }
        }

        // POST: Server/Edit/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(ZoneViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Put<Zone>(viewModel.ToModel(), null);
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
            var criterion = new Criterion("ZoneId", id);
            var response = Service.Get<Zone>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new ZoneViewModel(response.Model));
            }
        }

        // POST: Server/Delete/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var criterion = new Criterion("ZoneId", id);
            var response = Service.Delete<Zone>(criterion);
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
}
