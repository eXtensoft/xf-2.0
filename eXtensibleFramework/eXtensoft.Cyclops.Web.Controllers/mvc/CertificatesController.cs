// <copyright company="eXtensoft, LLC" file="CertificateController.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

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

    public sealed class CertificatesController : ServiceController
    {
        public ActionResult Index(Nullable<int> id)
        {
            if (id != null && id.HasValue)
            {
                return RedirectToAction("Details", new { id = id.Value });
            }
            else
            {
                string sortby;
                var c = GetParameters(Request, out sortby);

                var response = Service.GetAll<Certificate>(c);
                if (!response.IsOkay)
                {
                    return View(ErrorViewName, response.Status);
                }
                else
                {
                    var unsorted = from x in response
                                   select new CertificateViewModel(x);
                    var items = Sort(unsorted, sortby);
                    return View(items);
                }
            }
        }


        [Authorize(Roles = "member")]
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
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Server/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(CertificateViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Post<Certificate>(viewModel.ToModel());
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddModel(CertificateViewModel viewModel)
        {
            Certificate model = viewModel.ToModel();

            var response = Service.Post<Certificate>(model);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return RedirectToAction("Details", new { id = viewModel.CertificateId });
            }

        }

        [Authorize(Roles = "admin")]
        // GET: Server/Edit/5
        public ActionResult Edit(int id)
        {
            var criterion = new Criterion("CertificateId", id);
            var response = Service.Get<Certificate>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new CertificateViewModel(response.Model));
            }
        }

        // POST: Server/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(CertificateViewModel viewModel)
        {
            if (!viewModel.IsValid())
            {
                return View(viewModel);
            }
            else
            {
                var response = Service.Put<Certificate>(viewModel.ToModel(), null);
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
        [Authorize(Roles = "admin")]
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            var criterion = new Criterion("CertificateId", id);
            var response = Service.Get<Certificate>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return View(new CertificateViewModel(response.Model));
            }
        }

        // POST: Server/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var criterion = new Criterion("CertificateId", id);
            var response = Service.Delete<Certificate>(criterion);
            if (!response.IsOkay)
            {
                return View(ErrorViewName, response.Status);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private static IEnumerable<CertificateViewModel> Sort(IEnumerable<CertificateViewModel> list, string sortby)
        {
            IEnumerable<CertificateViewModel> sorted = null;
            switch (sortby)
            {
                case "domain":
                    sorted = list.OrderBy(x => x.Domain);
                    break;
                case "name":
                    sorted = list.OrderBy(x => x.Name);
                    break;
                case "begin":
                    sorted = list.OrderBy(x => x.Begin);
                    break;
                case "end":
                    sorted = list.OrderBy(x => x.End);
                    break;
                default:
                    sorted = list.OrderBy(x => x.Domain);
                    break;
            }
            return sorted;
        }
    }

}
