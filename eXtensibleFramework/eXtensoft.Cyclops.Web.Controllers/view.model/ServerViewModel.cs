using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public class ServerViewModel : ViewModel<Server>
    {
        #region properties

        [ScaffoldColumn(false)]
        public int ServerId { get; set; }

        [Required]
        [Display(Name = "OS")]
        public string OperatingSystem { get; set; }

        [Required]
        [Display(Name = "Zone")]
        public string Zone { get; set; }

        [Required]
        [Display(Name = "Platform")]
        public string HostPlatform { get; set; }

        [Required]
        [Display(Name = "Security")]
        public string ServerSecurity { get; set; }

        [Required(ErrorMessage = "Must enter the machine name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        [Display(Name = "ExternalIP")]
        public string ExternalIP { get; set; }

        [Display(Name = "InternalIP")]
        public string InternalIP { get; set; }

        [Display(Name = "Usage")]
        public string Usage { get; set; }

        [Display(Name = "Domain")]
        public string TLD { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        [Display(Name = "App to Add")]
        public int AppId { get; set; }

        public int OperatingSystemId { get; set; }
        public int ServerSecurityId { get; set; }
        public int HostPlatformId { get; set; }

        public string BackUrl { get; set; }
        public int SolutionId { get; set; }

        public bool IsFavorite { get; set; }
        #endregion properties

        public ServerViewModel() { }

        public ServerViewModel(Server model)
        {

            string name =  System.Security.Principal.WindowsPrincipal.Current.Identity.Name;
            //string user = System.Security.Principal.User.Identity.GetUserName();
            var favorites = SelectionListUtility.GetFavorites(name, "server");
            ServerId = model.ServerId;
            var found = favorites.Find(x => x.ModelId.Equals(model.ServerId));
            if (found != null)
            {
                IsFavorite = true;
            }
            ResolveIcon(model.OperatingSystemId);


            OperatingSystem = SelectionConverter.Convert(model.OperatingSystemId);
            HostPlatform = SelectionConverter.Convert(model.HostPlatformId);
            ServerSecurity = SelectionConverter.Convert(model.SecurityId);
            OperatingSystemId = model.OperatingSystemId;
            HostPlatformId = model.HostPlatformId;
            ServerSecurityId = model.SecurityId;
             Name = model.Name;
            Alias = model.Alias;
            Description = model.Description;
            ExternalIP = model.ExternalIP;
            InternalIP = model.InternalIP;
            Tags = model.Tags;

        }





        public override bool IsValid()
        {
            return true;
        }

        public override Server ToModel()
        {
            Server model = new Server();
            model.ServerId = ServerId;
            model.OperatingSystemId = OperatingSystemId;// SelectionConverter.ConvertToId(OperatingSystem);
            model.HostPlatformId = HostPlatformId; //SelectionConverter.ConvertToId(HostPlatform);
            model.SecurityId = ServerSecurityId;// SelectionConverter.ConvertToId(ServerSecurity);
            model.Name = Name;
            model.Alias = Alias;
            model.Description = Description;
            model.ExternalIP = ExternalIP;
            model.InternalIP = InternalIP;
            model.Tags = Tags;
            return model;
        }
    }
}
