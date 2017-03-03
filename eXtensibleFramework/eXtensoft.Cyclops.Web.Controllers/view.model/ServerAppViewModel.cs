using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public class ServerAppViewModel : ViewModel<ServerApp>
    {
        #region properties

        public int ServerAppId { get; set; }

        public string Server { get; set; }

        public string App { get; set; }

        public string Folderpath { get; set; }

        public string BackupFolderpath { get; set; }

        public string Zone { get; set; }

        public string Scope { get; set; }

        public string Domain { get; set; }

        public int ZoneId { get; set; }

        public int ScopeId { get; set; }

        public int DomainId { get; set; }


        public string Url { get; set; }

        public int ServerId { get; set; }

        public int AppId { get; set; }

        public string GroupName { get; set; }

        public string GroupByPath { get; set; }

        public string ItemName { get; set; }

        public string Security { get; set; }

        public string ExternalIP { get; set; }

        public string InternalIP { get; set; }

        public string LastDeployedOn { get; set; }

        public int SolutionId { get; set; }

        public string BackUrl { get; set; }
        public bool IsFavorite { get; set; }

        public bool IsServerFavorite { get; set; }
        public bool IsAppFavorite { get; set; }
        public string Display
        {
            get
            {
                return (!String.IsNullOrEmpty(Domain) && !Domain.Equals("None")) ? String.Format("{0} {1}", Domain, App) : App;
            }
        }

        #endregion properties

        //public List<IProjection> Files { get; set; }

        public ServerAppViewModel() { }

        public ServerAppViewModel(ServerApp model)
        {
            string name = System.Security.Principal.WindowsPrincipal.Current.Identity.Name;
            //string user = System.Security.Principal.User.Identity.GetUserName();
            var favServerApps = SelectionListUtility.GetFavorites(name, "serverapp");
            var favServers = SelectionListUtility.GetFavorites(name, "server");
            var favApps = SelectionListUtility.GetFavorites(name, "app");
            var icons = SelectionListUtility.GetSelectionsDictionary();
            ServerAppId = model.ServerAppId;
            var found = favServerApps.Find(x => x.ModelId.Equals(model.ServerAppId));
            if (found != null)
            {
                IsFavorite = true;
            }
            var serverFound = favServerApps.Find(x => x.ModelId.Equals(model.ServerAppId));
            if (serverFound != null)
            {
                IsServerFavorite = true;
            }
            var appFound = favApps.Find(x => x.ModelId.Equals(model.AppId));
            if (appFound != null)
            {
                IsAppFavorite = true;
            }
            if (icons.ContainsKey(model.AppId))
            {
                Icon = icons[model.AppId];
            }
            var servers = SelectionListUtility.GetServerDictionary();
            var apps = SelectionListUtility.GetAppDictionary();
            var zones = SelectionListUtility.GetZoneDictionary();

            ServerId = model.ServerId;
            if (servers.ContainsKey(model.ServerId))
            {
                Server = servers[model.ServerId];
            }
            AppId = model.AppId;
            if (apps.ContainsKey(model.AppId))
            {
                App = apps[model.AppId];
            }

            Security = SelectionConverter.Convert(model.SecurityId);

            BackupFolderpath = model.BackupFolderpath;
            Folderpath = model.Folderpath;
            Scope = SelectionConverter.Convert(model.ScopeId);
            ScopeId = model.ScopeId;

            if (zones.ContainsKey(model.ZoneId))
            {
                Zone = zones[model.ZoneId];
            }
            ZoneId = model.ZoneId;

            Domain = SelectionConverter.Convert(model.DomainId);
            DomainId = model.DomainId;

            if (!String.IsNullOrWhiteSpace(model.ExternalIP))
            {
                ExternalIP = model.ExternalIP;
            }
            if (!String.IsNullOrWhiteSpace(model.InternalIP))
            {
                InternalIP = model.InternalIP;
            }

        }
        public override bool IsValid()
        {
            return true;
        }

        public override ServerApp ToModel()
        {
            ServerApp model = new ServerApp();
            model.ServerAppId = this.ServerAppId;
            model.ServerId = ServerId;
            if (!String.IsNullOrWhiteSpace(App))
            {
                model.AppId = SelectionListUtility.GetReverseAppDictionary()[App];
            }
            else
            {
                model.AppId = AppId;
            }
            model.DomainId = DomainId;
            model.ServerId = ServerId;
            model.ScopeId = ScopeId;
            model.ZoneId = ZoneId;
            model.Folderpath = Folderpath;
            model.BackupFolderpath = BackupFolderpath;

            return model;
        }
    }

}
