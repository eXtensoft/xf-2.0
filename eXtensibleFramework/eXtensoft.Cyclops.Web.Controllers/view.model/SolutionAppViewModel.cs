using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public class SolutionAppViewModel : ViewModel<SolutionApp>
    {

        public int SolutionAppId { get; set; }

        public int SolutionId { get; set; }

        public int AppId { get; set; }

        public string Solution { get; set; }

        public string Application { get; set; }

        public List<ServerAppViewModelCollection> ZoneServerApps { get; set; }

        public SolutionAppViewModel() { }

        public SolutionAppViewModel(SolutionApp model)
        {
            var apps = SelectionListUtility.GetAppDictionary();
            var solutions = SelectionListUtility.GetSolutionDictionary();
            var icons = SelectionListUtility.GetSelectionsDictionary();

            SolutionAppId = model.SolutionAppId;
            SolutionId = model.SolutionId;
            AppId = model.AppId;
            Solution = SelectionConverter.Convert(model.SolutionId);
            if (apps.ContainsKey(model.AppId))
            {
                Application = apps[model.AppId];
            }
            if (solutions.ContainsKey(model.SolutionId))
            {
                Solution = solutions[model.SolutionId];
            }
            if (icons.ContainsKey(model.AppId))
            {
                Icon = icons[model.AppId];
            }
        }



        public override SolutionApp ToModel()
        {
            SolutionApp model = new SolutionApp();

            model.SolutionAppId = SolutionAppId;
            model.SolutionId = SelectionConverter.ConvertToId(Solution);
            model.AppId = SelectionConverter.ConvertToId(Application);

            return model;
        }

        public override bool IsValid()
        {
            return true;
        }







    }
}

