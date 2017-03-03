

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MetaFile
    {
        public int FileId { get; set; }

        public Guid FileGuid { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        //

        public string TagText { get; set; }

        public string Name { get; set; }

        public string Filepath { get; set; }

        public string FileType { get; set; }

        public long Size { get; set; }

        public int MimeId { get; set; }

        public string Mime { get; set; }

        public string Extension { get; set; }

        public string AddedBy { get; set; }

        public DateTimeOffset AddedAt { get; set; }

        public bool IsValid()
        {
            bool b = true;
            b = (b == true) ? !String.IsNullOrWhiteSpace(Name) : false;
            b = (b == true) ? !String.IsNullOrWhiteSpace(Filepath) : false;
            b = (b == true) ? !String.IsNullOrWhiteSpace(FileType) : false;
            b = (b == true) ? !String.IsNullOrWhiteSpace(Mime) : false;
            b = (b == true) ? !String.IsNullOrWhiteSpace(Extension) : false;
            b = (b == true) ? !String.IsNullOrWhiteSpace(AddedBy) : false;

            return b;
        }
    }
}

