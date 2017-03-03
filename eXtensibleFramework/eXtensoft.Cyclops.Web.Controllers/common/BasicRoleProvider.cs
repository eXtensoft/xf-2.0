using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Cyclops
{
    public class BasicRoleProvider : RoleProvider
    {

        private static IDictionary<string, List<string>> _personaRoles = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        private static IList<string> _roles = new List<string>();

        static BasicRoleProvider()
        {
            Initialize();
        }

        private static void Initialize()
        {
            HashSet<string> hs = new HashSet<string>();

            SortedDictionary<int, string> d = new SortedDictionary<int, string>();
            Dictionary<string, List<string>> userRoles = new Dictionary<string, List<string>>();


            string key = String.Format("basicRoleProvider.{0}", "roles");
            var found = ConfigurationManager.AppSettings[key];
            if (!String.IsNullOrWhiteSpace(found))
            {
                string[] roles = found.Split(new char[] { ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (roles != null)
                {
                    foreach (var s in roles)
                    {
                        int id;
                        string[] role = s.Split(new char[] { '.', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (role != null && role.Length == 2)
                        {
                            string t = role[0];
                            _roles.Add(t);
                            if (Int32.TryParse(role[1], out id))
                            {
                                d.Add(id, t);

                                string roleKey = String.Format("basicRoleProvider.roles.{0}", t);

                                string roleFound = ConfigurationManager.AppSettings[roleKey];
                                string[] users = roleFound.Split(new char[] { ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var usr in users)
                                {
                                    hs.Add(usr);
                                }
                                userRoles.Add(t, new List<string>(users));
                            }
                        }
                    }
                }
                foreach (var item in d.Keys.Reverse())
                {
                    int max = item;
                    string rolename = d[item];
                    string roleKey = String.Format("basicRoleProvider.roles.{0}", rolename);
                    string usersInRole = ConfigurationManager.AppSettings[roleKey];
                    if (!String.IsNullOrEmpty(usersInRole))
                    {
                        string[] arr = usersInRole.Split(new char[] { ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var p in arr)
                        {
                            if (!_personaRoles.ContainsKey(p.ToLower()))
                            {
                                _personaRoles.Add(p.ToLower(), new List<string>());
                            }
                            for (int i = max; i >= 0; i--)
                            {
                                if (d.ContainsKey(i))
                                {
                                    string additionalRole = d[i];
                                    _personaRoles[p.ToLower()].Add(additionalRole);
                                }
                            }
                        }
                    }

                }
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                string key = String.Format("basicRoleProvider.{0}", "applicationName");
                var found = ConfigurationManager.AppSettings[key];
                return !String.IsNullOrEmpty(found) ? found : "demo";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            string key = String.Format("basicRoleProvider.{0}", roleName.ToLower());
            var found = ConfigurationManager.AppSettings[key];
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return _roles.ToArray();

            //string key = String.Format("basicRoleProvider.{0}", "roles");
            //var found = ConfigurationManager.AppSettings[key];
            //if (!String.IsNullOrWhiteSpace(found))
            //{
            //    return Split(found);
            //}
            //return new string[2]{"guest","admin"};
        }

        public override string[] GetRolesForUser(string username)
        {
            //List<string> list = new List<string>();
            //foreach (string role in GetAllRoles())
            //{
            //    string key = String.Format("basicRoleProvider.roles.{0}", role);
            //    string found = ConfigurationManager.AppSettings[key];
            //    if (!String.IsNullOrWhiteSpace(found))
            //    {
            //        foreach (string user in Split(found))
            //        {
            //            if (username.Equals(user, StringComparison.OrdinalIgnoreCase))
            //            {
            //                if (key.Contains('.') && key.LastIndexOf('.') < key.Length)
            //                {
            //                    string userrole = key.Substring(key.LastIndexOf('.')+1);
            //                    list.Add(userrole);                                
            //                }

            //            }
            //        }
            //    }
            //}
            //return list.ToArray();

            if (_personaRoles.ContainsKey(username))
            {
                return _personaRoles[username].ToArray();
            }
            else
            {
                return new string[0];
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            string key = String.Format("basicRoleProvider.roles.{0}", roleName);
            return Split(key);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }


        private static string[] Split(string delimitedString)
        {
            return !String.IsNullOrWhiteSpace(delimitedString) ? delimitedString.Split(new char[] { ',', ';', ':', '|' }, StringSplitOptions.RemoveEmptyEntries) : new string[0];
        }
    }
}
