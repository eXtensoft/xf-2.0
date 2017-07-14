using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cyclops.Web
{
    public class FileInfoViewModel 
    {
        public string Folderpath { get; set; }

        public string RelativeFolderpath { get; set; }

        public string ImagePath
        {
            get
            {
                return RelativeFolderpath.TrimStart('~') + "/" + _Model.Name;
            }
        }

        public string Fullname { get { return _Model.FullName; } }

        public string Extension { get { return _Model.Extension; } }

        private string _Name;
        public string Name { get { return  _Model != null  ?_Model.Name : _Name; } }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase FileUpload { get; set; }
        private FileInfo _Model;
        public FileInfoViewModel(FileInfo model)
        {
            _Model = model;
          
        }

        public FileInfoViewModel()
        {

        }
    }
}
