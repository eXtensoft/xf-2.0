

namespace Cyclops.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Web;
    using XF.Common;

    public class FileDropController :  ServiceController
    {

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (HttpPostedFileBase file in files)
            {
                string filePath = Path.Combine("", file.FileName);
                System.IO.File.WriteAllBytes(filePath, this.ReadData(file.InputStream));
            }
            return this.Json("All files have been successfully stored.");
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
}
