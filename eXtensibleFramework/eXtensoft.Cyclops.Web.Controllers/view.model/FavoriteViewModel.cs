using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public class FavoriteViewModel : ViewModel<Favorite>
    {
        public int Id { get; set; }
        public int FavoriteId { get; set; }
        public string Username { get; set; }
        public string Model { get; set; }
        public int ModelId { get; set; }
        public DateTime Tds { get; set; }

        public FavoriteViewModel() { }

        public FavoriteViewModel(Favorite model)
        {
            FavoriteId = model.FavoriteId;
            Username = model.Username;
            Model = model.Model;
            ModelId = model.ModelId;
            Tds = model.Tds;
        }

        public override bool IsValid()
        {
            bool b = true;
            b = b ? !String.IsNullOrWhiteSpace(Model) : false;
            b = b ? !String.IsNullOrWhiteSpace(Username) : false;
            b = b ? ModelId > 0 : false;
            return b;
        }

        public override Favorite ToModel()
        {
            Favorite model = new Favorite();
            model.FavoriteId = Id > 0 ? Id : FavoriteId;
            model.Model = Model;
            model.ModelId = ModelId;
            model.Username = Username;
            return model;
        }
    }
}