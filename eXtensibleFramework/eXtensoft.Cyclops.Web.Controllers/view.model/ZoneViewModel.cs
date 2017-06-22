using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public class ZoneViewModel : ViewModel<Zone>
    {


        public int ZoneId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Token { get; set; }


        public ZoneViewModel() { }

        public ZoneViewModel(Zone model)
        {
            ZoneId = model.ZoneId;
            Name = model.Name;
            Alias = model.Alias;
            Token = model.Token;
        }

        public override bool IsValid()
        {
            return true;
        }

        public override Zone ToModel()
        {
            Zone model = new Zone();
            model.ZoneId = ZoneId;
            model.Name = Name;
            model.Alias = Alias;
            model.Token = Token;
            return model;
        }
    }
}
