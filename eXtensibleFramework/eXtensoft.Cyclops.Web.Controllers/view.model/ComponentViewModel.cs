using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public class ComponentViewModel : ViewModel<Component>
    {
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public override Component ToModel()
        {
            throw new NotImplementedException();
        }
    }
}
