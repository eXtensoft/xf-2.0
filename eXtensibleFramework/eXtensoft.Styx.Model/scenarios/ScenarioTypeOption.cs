using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.ProjectManagement
{
    [Flags]
    public enum ScenarioTypeOption
    {
        None = 0,
        Domain = 1, // user Story: as a [persona] I want [action] so that [goal]
        System = 2, // system interacting without people (sequence)
        Mixed = 3, // User interacting with a system

    }
}
// http://www.boost.co.nz/blog/2012/01/use-cases-or-user-stories/
// 