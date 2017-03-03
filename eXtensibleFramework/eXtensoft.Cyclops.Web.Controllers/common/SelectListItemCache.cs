

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using XF.Common;

    public class SelectListItemCache
    {
        #region singleton members

        public DateTime Start { get; set; }

        public static SelectListItemCache Instance { get; set; }

        #region constructors

        static SelectListItemCache()
        {
            Instance = new SelectListItemCache();
        }

        private SelectListItemCache()
        {
            Start = DateTime.UtcNow;
        }

        #endregion

        #endregion


        #region local fields

        private List<string> _CoerceRefreshKeys = new List<string>();

        private Dictionary<string, List<SelectListItem>> _Cache = new Dictionary<string, List<SelectListItem>>();

        #endregion

        #region properties

        private Dictionary<string, Func<List<SelectListItem>>> _Providers = new Dictionary<string, Func<List<SelectListItem>>>();
        public Dictionary<string, Func<List<SelectListItem>>> Providers
        {
            get { return _Providers; }
            set
            {
                if (_Providers != value)
                {
                    _Providers = value;
                }
            }
        }

        #endregion


        #region instance methods

        public void CoerceRefresh(string parameter)
        {
            if (parameter != null)
            {
                string key = parameter.ToString().ToLower();
                if (!_CoerceRefreshKeys.Contains(key))
                {
                    Remove(parameter);
                    PullList(parameter);
                }
            }
        }

        public List<SelectListItem> ReadList(object parameter)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (parameter != null)
            {
                string key = parameter.ToString().ToLower();
                if (_CoerceRefreshKeys.Contains(key))
                {
                    list = InvokeProvider(key);
                }
                else if (!_Cache.ContainsKey(key))
                {
                    PullList(key);
                    if (_Cache.ContainsKey(key))
                    {
                        list = _Cache[key];
                    }
                }
                else
                {
                    list = _Cache[key];
                }


            }
            return list;
        }

        public void RegisterProvider(string key, Func<List<SelectListItem>> provider)
        {
            RegisterProvider(key, provider, false);
        }

        public void RegisterProvider(string key, Func<List<SelectListItem>> provider, bool coerceRefresh)
        {
            if (!String.IsNullOrEmpty(key) && provider != null)
            {
                string providerkey = key.Trim().ToLower();
                if (!_Cache.ContainsKey(providerkey))
                {
                    if (coerceRefresh)
                    {
                        _CoerceRefreshKeys.Add(providerkey);
                    }
                    Providers.Add(providerkey, provider);
                }
            }
        }

        #endregion

        #region helper methods

        private void Remove(object parameter)
        {
            if (parameter != null)
            {
                string key = parameter.ToString().ToLower();
                if (_Cache.ContainsKey(key))
                {
                    _Cache.Remove(key);
                }
            }
        }

        private void PullList(object parameter)
        {
            if (parameter != null)
            {
                string key = parameter.ToString().ToLower();
                List<SelectListItem> list = InvokeProvider(parameter);
                if (list != null && list.Count > 0)
                {
                    _Cache.Add(key, list);
                }
            }
        }

        private List<SelectListItem> InvokeProvider(object parameter)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (parameter != null)
            {
                string key = parameter.ToString().ToLower();
                if (Providers.ContainsKey(key))
                {
                    try
                    {
                        var provider = Providers[key];
                        list = provider.Invoke();
                    }
                    catch (Exception ex)
                    {
                        EventWriter.WriteError(ex.Message, SeverityType.Error);
                    }
                }
            }
            return list;
        }

        #endregion

    }
}
