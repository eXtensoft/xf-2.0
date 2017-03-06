

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

    public class AppsController : FileUploadController
    {
        public ActionResult Index(Nullable<int> id)
        {
            if (id != null && id.HasValue)
            {
                return RedirectToAction("Details", new { id = id.Value });
            }
            else
            {
                var c = GetParameters(Request);

                var response = Service.GetAll<App>(c);
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    var items = from x in response
                                select new AppViewModel(x);
                    return View(items);
                }
            }
        }

        // GET: Apps/Details/5
        public ActionResult Details(int id)
        {
            var criterion = new Criterion("AppId", id);
            var response = Service.Get<App>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new AppViewModel(response.Model));
            }
        }

        // GET: Apps/Create
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Apps/Create
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(AppViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Post<App>(viewModel.ToModel());
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

        // GET: Apps/Edit/5
        //[Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var criterion = new Criterion("AppId", id);
            var response = Service.Get<App>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new AppViewModel(response.Model));
            }
        }

        // POST: Apps/Edit/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(AppViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Put<App>(viewModel.ToModel(), null);
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

        // GET: Apps/Delete/5
        //[Authorize(Roles = "admin")]
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            var criterion = new Criterion("AppId", id);
            var response = Service.Get<App>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new AppViewModel(response.Model));
            }
        }

        // POST: Apps/Delete/5
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var criterion = new Criterion("AppId", id);
            var response = Service.Delete<App>(criterion);
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
