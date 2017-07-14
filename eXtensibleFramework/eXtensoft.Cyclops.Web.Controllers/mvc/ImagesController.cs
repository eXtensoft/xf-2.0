// <copyright company="eXtensoft, LLC" file="ImagesController.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    public sealed class ImagesController : FileUploadController
    {
        #region local members

        #endregion

        [HttpGet]
        public ActionResult Index(Nullable<int> id)
        {
            string s = "~/content/icons";
            string folderPath = Path.Combine(Server.MapPath(s));

            if (id.HasValue)
            {
                return View();
            }
            else
            {
                var images = ImagesManager.GetAll(folderPath,s);
                return View(images);
            }


        }
        #region constructors

        #endregion


    }

}
