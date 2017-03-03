using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.ProjectManagement
{
    public class Condition
    {
        public string ConditionId { get; set; }

        public ConditionTypeOption ConditionType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

    }
}
