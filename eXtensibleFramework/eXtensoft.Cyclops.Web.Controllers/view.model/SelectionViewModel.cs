using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops.Web
{
    public class SelectionViewModel : ViewModel<Selection>
    {

        public int SelectionId { get; set; }
        public string Display { get; set; }
        public string Token { get; set; }

        public int Sort { get; set; }

        public int GroupId { get; set; }

        public int MasterId { get; set; }

        public string Master { get; set; }

        public string GroupName { get; set; }

        public string DisplayIcon { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override Selection ToModel()
        {
            Selection model = new Selection();
            model.SelectionId = SelectionId;
            model.Display = Display;
            model.Token = Token;
            model.Sort = Sort;
            model.GroupId = GroupId;
            model.MasterId = MasterId;
            model.Icon = Icon;
            return model;
        }

        public SelectionViewModel() { }

        public SelectionViewModel(Selection model)
        {
            SelectionId = model.SelectionId;
            Display = model.Display;
            Token = model.Token;
            Sort = model.Sort;
            GroupId = model.GroupId;
            MasterId = model.MasterId;
            DisplayIcon = model.DisplayIcon;
            Icon = model.Icon;
            if (model.MasterId > 0)
            {
                Master = SelectionConverter.Convert(model.MasterId);
            }
            if (model.GroupId > 0)
            {
                GroupName = SelectionConverter.Convert(model.GroupId);
            }

        }



    }
}
