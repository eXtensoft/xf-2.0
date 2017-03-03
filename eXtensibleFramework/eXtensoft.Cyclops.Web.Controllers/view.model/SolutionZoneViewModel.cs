

namespace Cyclops.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;

    public class SolutionZoneViewModel : ViewModel<SolutionZone>
    {
        public string Display { get; set; }

        public string Solution { get; set; }

        public string Zone { get; set; }

        public string Domain { get; set; }

        public int SolutionZoneId { get; set; }

        public int SolutionId { get; set; }

        public int ZoneId { get; set; }

        public int DomainId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }



        public SolutionZoneViewModel() { }

        public SolutionZoneViewModel(SolutionZone model)
        {
            var solutions = SelectionListUtility.GetSolutionDictionary();
            var zones = SelectionListUtility.GetZoneDictionary();

            SolutionZoneId = model.SolutionZoneId;
            SolutionId = model.SolutionId;
            ZoneId = model.ZoneId;
            DomainId = model.DomainId;

            Name = model.Name;
            Description = model.Description;

            if (solutions.ContainsKey(model.SolutionId))
            {
                Solution = solutions[model.SolutionId];
            }
            if (zones.ContainsKey(model.ZoneId))
            {
                Zone = zones[model.ZoneId];
            }
            Domain = SelectionConverter.Convert(model.DomainId);

        }

        public override bool IsValid()
        {
            return true;
        }

        public override SolutionZone ToModel()
        {
            SolutionZone model = new SolutionZone();
            model.SolutionZoneId = SolutionZoneId;
            model.SolutionId = SolutionId;
            model.ZoneId = ZoneId;
            model.DomainId = DomainId;
            model.Name = Name;
            model.Description = Description;

            return model;
        }
    }
}
