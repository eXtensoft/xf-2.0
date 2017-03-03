using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public class AppViewModel : ViewModel<App>
    {
        #region properties

        [ScaffoldColumn(false)]
        public int AppId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "App Type")]
        public string AppType { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        public int AppTypeId { get; set; }

        public bool IsFavorite { get; set; }

        #endregion properties

        public AppViewModel() { }

        public AppViewModel(App model)
        {
            //Icon = ResolveIcon(model.AppTypeId);
            AppId = model.AppId;
            Name = model.Name;
            AppTypeId = model.AppTypeId;
            AppType = SelectionConverter.Convert(model.AppTypeId);
            var icons = SelectionListUtility.GetSelectionsDictionary();
            if (icons.ContainsKey(model.AppTypeId))
            {
                Icon = icons[model.AppTypeId];
            }
            Alias = model.Alias;
            Description = model.Description;
            Tags = model.Tags;

            string name = System.Security.Principal.WindowsPrincipal.Current.Identity.Name;
            //string user = System.Security.Principal.User.Identity.GetUserName();
            var favorites = SelectionListUtility.GetFavorites(name, "app");

            var found = favorites.Find(x => x.ModelId.Equals(model.AppId));
            if (found != null)
            {
                IsFavorite = true;
            }

        }


        public override bool IsValid()
        {
            return true;
        }


        public override App ToModel()
        {
            App model = new App();
            model.Alias = Alias;
            model.AppId = AppId;
            model.AppTypeId = AppTypeId;// SelectionConverter.ConvertToId(AppType);
            model.Description = Description;
            model.Name = Name;
            model.Tags = Tags;
            return model;
        }

    }
}
