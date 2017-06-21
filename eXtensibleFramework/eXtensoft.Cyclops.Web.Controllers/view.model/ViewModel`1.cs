using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public abstract class ViewModel<T> where T : class, new()
    {
        public abstract bool IsValid();

        public abstract T ToModel();

        private string _Icon = "server.red.png";
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        protected  void ResolveIcon(int id)
        {
            var icons = SelectionListUtility.GetSelectionsDictionary();
            if (icons.ContainsKey(id))
            {
                _Icon = icons[id];
            }
        }

        //private static Dictionary<int, string> _IconMaps = new Dictionary<int, string>()
        //{
        //    {20,"app.website.png" },
        //    {21,"app.website.png" },
        //    {22,"app.website.png" },
        //    {24,"app.webservice.png" },
        //    {25,"app.webservice.png" },
        //    {26,"app.processing.png" },
        //    {27,"app.processing.png" },
        //    {28,"app.windows.png" },
        //    {29,"app.processing.png" },
        //    {30,"app.datastore.png" },
        //    {31,"app.mongo.png" },
        //};
    }
}


