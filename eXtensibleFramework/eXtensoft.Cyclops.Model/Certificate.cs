using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string Name { get; set; }

        public string Domain { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }


    }
}
