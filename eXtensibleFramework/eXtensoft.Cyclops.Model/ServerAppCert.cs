using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops
{
    [Serializable]
    public class ServerAppCert
    {
        public int ServerId { get; set; }

        public int AppId { get; set; }

        public int CertificateId { get; set; }
    }
}
