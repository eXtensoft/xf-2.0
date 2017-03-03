// <copyright company="eXtensoft, LLC" file="DocViewController.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops.Controllers
{
    using Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Web;
    using XF.Common;
    public sealed class DocViewController : ServiceController
    {
        //public ActionResult Text(string location)
        //{
        //    return View("TextView",location);
        //}

        //public ActionResult Image(string location)
        //{
        //    //return View("ImageView","red");
        //    return View("imageview", "_viewer", location);
        //}
        public ActionResult Image(int id)
        {
            Criterion c = new Criterion("ArtifactId", id);
            var response = Service.Get<Artifact>(c);
            if (response.IsOkay)
            {
                ArtifactViewModel vm = new ArtifactViewModel(response.Model);
                return View("imageview", "_viewer", vm);
            }
            else
            {
                return View(ErrorViewName, response.Status);
            }
        }

        public ActionResult Text(int id)
        {
            Criterion c = new Criterion("ArtifactId", id);
            var response = Service.Get<Artifact>(c);
            if (response.IsOkay && System.IO.File.Exists(response.Model.Location))
            {
                var contents = System.IO.File.ReadAllText(response.Model.Location);
                ArtifactViewModel vm = new ArtifactViewModel(response.Model) { Text = contents };
                return View("textview", "_viewer", vm);
            }
            else
            {
                return View(ErrorViewName, response.Status);
            }
        }
    }

}
