using Cyclops.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using XF.Common;

namespace Cyclops
{
    public static class SelectionListUtility
    {


        public static List<SelectListItem> GetOperatingSystems()
        {
            return GetList("operating-system");
        }

        public static List<SelectListItem> GetHosting()
        {
            return GetList("hosting");
        }

        public static List<SelectListItem> GetServerSecurity()
        {
            return GetList("security");
        }

        public static List<SelectListItem> GetAppTypes()
        {
            return GetList("app-type");
        }

        public static List<SelectListItem> GetDomains()
        {
            List<SelectListItem> list = GetList("domain");
            list.Insert(0, new SelectListItem() { Text = "none", Value = "0" });
            return list;
        }

        public static List<SelectListItem> GetScopes()
        {
            List<SelectListItem> list = GetList("scope");
            list.Insert(0, new SelectListItem() { Text = "none", Value = "0" });
            return list;
        }

        public static List<SelectListItem> GetArtifactScopes()
        {
            List<SelectListItem> list = GetList("artifact-scope");
            list.Insert(0, new SelectListItem() { Text = "none", Value = "0" });
            return list;
        }
        public static List<SelectListItem> GetArtifactTypes()
        {
            List<SelectListItem> list = GetList("artifact-type");
            list.Insert(0, new SelectListItem() { Text = "none", Value = "0" });
            return list;
        }


        public static List<SelectListItem> GetList(string token)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var service = GetService();
            var response = service.GetAll<Selection>(null);
            if (response.IsOkay)
            {
                var found = response.ToList().Find(x => x.Token.Equals(token, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                {
                    list = (from x in response.Where(x => x.MasterId.Equals(found.SelectionId)) 
                            select new SelectListItem() { Text = x.Display, Value = x.SelectionId.ToString() }).ToList();
                }
            }

            return list;
        }

        public static Dictionary<int,string> GetDictionary(string token)
        {
            Dictionary<int, string> d = new Dictionary<int, string>();

            var service = GetService();
            var response = service.GetAll<Selection>(null);
            if (response.IsOkay)
            {
                var found = response.ToList().Find(x => x.Token.Equals(token, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                {
                    foreach (var item in response.Where(x=>x.MasterId.Equals(found.SelectionId)))
                    {
                        d.Add(item.SelectionId,item.Token);
                    }
                }
            }

            return d;
        }

        public static Dictionary<string, int> GetReverseDictionary(string token)
        {
            Dictionary<string,int> d = new Dictionary<string, int>();

            var service = GetService();
            var response = service.GetAll<Selection>(null);
            if (response.IsOkay)
            {
                var found = response.ToList().Find(x => x.Token.Equals(token, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                {
                    foreach (var item in response.Where(x => x.MasterId.Equals(found.SelectionId)))
                    {
                        d.Add(item.Token,item.SelectionId);
                    }
                }
            }

            return d;
        }


        public static SelectList GetAppsList()
        {
            var service = GetService();
            var response = service.GetAll<App>(null);
            return new SelectList((from x in response
                                   select new
                                   {
                                       Id = x.AppId.ToString(),
                                       Name = x.Name,
                                       Group = SelectionConverter.Convert(x.AppTypeId)
                                   }).OrderBy(y => y.Group), "Id", "Name", "Group", 1);

        }

        public static List<SelectListItem> GetZones()
        {
            var service = GetService();
            var response = service.GetAll<Zone>(null);
            return (from x in response
                                   select new SelectListItem()
                                   {
                                       Value = x.ZoneId.ToString(),
                                       Text = x.Name,
                                   }).ToList();
        }

        public static SelectList GetServers()
        {
            var service = GetService();
            var response = service.GetAll<Server>(null);
            return new SelectList((from x in response
                                   select new
                                   {
                                       Id = x.ServerId.ToString(),
                                       Name = x.Name,
                                       Group = SelectionConverter.Convert(x.SecurityId)
                                   }).OrderBy(y => y.Group), "Id", "Name", "Group", 1);
        }

        public static SelectList GetSolutions()
        {
            var service = GetService();
            var response = service.GetAll<Solution>(null);
            return new SelectList((from x in response
                                   select new
                                   {
                                       Id = x.SolutionId.ToString(),
                                       Name = x.Name,
                                       Group = SelectionConverter.Convert(x.ScopeId)
                                   }).OrderBy(y => y.Group), "Id", "Name", "Group", 1);
        }

        public static List<ArtifactViewModel> GetArtifacts(int scopeTypeId, int scopeId)
        {
            var criterion = Criterion.GenerateStrategy("get.for");
            criterion.AddItem("ScopeTypeId", scopeTypeId);
            criterion.AddItem("ScopeId", scopeId);
            var service = GetService();
            var response = service.GetAll<Artifact>(criterion);
            return (from x in response
                    select new ArtifactViewModel(x)
                    {



                    }).ToList();
        }

        public static List<ArtifactViewModel> GetArtifacts()
        {
            var service = GetService();
            var response = service.GetAll<Artifact>(null);
            return (from x in response select new ArtifactViewModel(x)).ToList();
        }


        public static List<SelectListItem> GetApps()
        {
            var service = GetService();
            var response = service.GetAll<App>(null);
            return (from x in response
                    select new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.AppId.ToString(),
                        Group = new SelectListGroup()
                        {
                            Name = SelectionConverter.Convert(x.AppTypeId)
                        }
                    }).ToList();
        }


        public static IEnumerable<ServerApp> GetAppsOnServer(int serverId)
        {
            var service = GetService();
            Criterion c = new Criterion();
            c.AddItem("ServerId", serverId);
            var response = service.GetAll<ServerApp>(c);
            if (response.IsOkay)
            {
                return response.ToList();
            }
            else
            {
                return new List<ServerApp>();
            }

        }

        public static IEnumerable<SolutionApp> GetAppsInSolution(int solutionId)
        {
            var service = GetService();
            Criterion c = new Criterion();
            c.AddItem("SolutionId", solutionId);
            var response = service.GetAll<SolutionApp>(c);
            if (response.IsOkay)
            {
                return response.ToList();
            }
            else
            {
                return new List<SolutionApp>();
            }

        }

        public static List<Favorite> GetFavorites(string user)
        {
            return GetFavorites(user, String.Empty);
        }

        public static List<Favorite> GetFavorites(string user, string model)
        {
            var c = Criterion.GenerateStrategy("for.user");
            c.AddItem("Username", user);
            if (!String.IsNullOrWhiteSpace(model))
            {
                c.AddItem("Model", model);
            }
            var service = GetService();
            var response = service.GetAll<Favorite>(c);
            if (response.IsOkay)
            {
                return response.ToList();
            }
            else
            {
                return new List<Favorite>();
            }

        }

        public static Dictionary<int, string> GetServerDictionary()
        {
            var service = GetService();
            var response = service.GetAll<Server>(null);
            Dictionary<int, string> d = new Dictionary<int, string>();
            foreach (var item in response)
            {
                d.Add(item.ServerId, item.Name);
            }
            return d;
        }

        public static Dictionary<string, int> GetReverseServerDictionary()
        {
            var service = GetService();
            var response = service.GetAll<Server>(null);
            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach (var item in response)
            {
                d.Add(item.Name, item.ServerId);
            }
            return d;
        }

        public static Dictionary<int, string> GetSolutionDictionary()
        {
            var service = GetService();
            var response = service.GetAll<Solution>(null);
            Dictionary<int, string> d = new Dictionary<int, string>();
            foreach (var item in response)
            {
                d.Add(item.SolutionId, item.Name);
            }
            return d;
        }

        public static Dictionary<string, int> GetReverseSolutionDictionary()
        {
            var service = GetService();
            var response = service.GetAll<Solution>(null);
            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach (var item in response)
            {
                d.Add(item.Name, item.SolutionId);
            }
            return d;
        }

        public static Dictionary<int, string> GetAppDictionary()
        {
            var service = GetService();
            var response = service.GetAll<App>(null);
            Dictionary<int, string> d = new Dictionary<int, string>();
            foreach (var item in response)
            {
                d.Add(item.AppId, item.Name);
            }
            return d;
        }

        public static Dictionary<int,string> GetAppIconDictionary()
        {
            var c = Criterion.GenerateStrategy("app.icon");
            var service = GetService();
            var response = service.GetAllProjections<App>(c);
            Dictionary<int, string> d = new Dictionary<int, string>();
            foreach (var item in response.Display)
            {
                if (!String.IsNullOrWhiteSpace(item.DisplayAlt))
                {
                    d.Add(item.IntVal, item.DisplayAlt);
                }                
            }
            return d;
        }

        public static Dictionary<int,string> GetSelectionsDictionary()
        {
            var service = GetService();
            var response = service.GetAllProjections<Selection>(null);
            Dictionary<int, string> d = new Dictionary<int, string>();
            foreach (var item in response.Display)
            {
                if (!String.IsNullOrWhiteSpace(item.DisplayAlt))
                {
                    d.Add(item.IntVal, item.DisplayAlt);
                }
            }
            return d;
        }

        public static Dictionary<string, int> GetReverseAppDictionary()
        {
            var service = GetService();
            var response = service.GetAll<App>(null);
            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach (var item in response)
            {
                d.Add(item.Name, item.AppId);
            }
            return d;
        }

        public static Dictionary<int,string> GetZoneDictionary()
        {
            var service = GetService();
            var response = service.GetAll<Zone>(null);
            Dictionary<int, string> d = new Dictionary<int, string>();
            foreach (var item in response)
            {
                d.Add(item.ZoneId, item.Name);
            }
            return d;
        }

        public static Dictionary<string, int> GetReverseZoneDictionary()
        {
            var service = GetService();
            var response = service.GetAll<Zone>(null);
            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach (var item in response)
            {
                d.Add(item.Name, item.ZoneId);
            }
            return d;
        }

        public static IEnumerable<SolutionZone> GetZonesForSolution(int solutionId)
        {
            var service = GetService();
            Criterion c = new Criterion();
            c.AddItem("SolutionId", solutionId);
            var response = service.GetAll<SolutionZone>(c);
            if (response.IsOkay)
            {
                return response.ToList();
            }
            else
            {
                return new List<SolutionZone>();
            }
        }


        private static IModelRequestService GetService()
        {
            return new PassThroughModelRequestService(
                        new DataRequestService(new XF.DataServices.ModelDataGatewayDataService())
                        );
        }


    }
}
