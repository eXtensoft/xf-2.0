

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
    public class FavoritesController : ServiceController
    {

        [HttpGet]
        public ActionResult Index()
        {
            var response = Service.GetAll<Favorite>(null);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                var vms = from x in response select new FavoriteViewModel(x);
                return View(vms);
            }
        }

        // POST: Server/Create
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(FavoriteViewModel viewModel)
        {
            if (viewModel.IsValid())
            {
                var model = viewModel.ToModel();
                var response = Service.Post<Favorite>(model);
                if (!response.IsOkay)
                {

                }
                else
                {

                }
            }
            if (viewModel.Model.Equals("server"))
            {
                //return RedirectToAction("details", "servers", new { ServerId = viewModel.ModelId });
                return RedirectToAction("index", "servers", new { id = viewModel.ModelId });
            }
            else if (viewModel.Model.Equals("serverapp"))
            {
                return RedirectToAction("index", "serverapps", new { id = viewModel.ModelId });
            }
            else if(viewModel.Model.Equals("app"))
            {
                return RedirectToAction("index", "apps", new { id = viewModel.ModelId });
            }
            return RedirectToAction("Index");

        }



    }
}
