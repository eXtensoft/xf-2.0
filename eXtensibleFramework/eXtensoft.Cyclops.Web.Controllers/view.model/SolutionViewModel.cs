

namespace Cyclops.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SolutionViewModel : ViewModel<Solution>
    { 
        [ScaffoldColumn(false)]
        public int SolutionId { get; set; }
        [Required]
        [Display(Name = "Name")]
         public string Name { get; set; }

        [Required]
        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Scope")]
        public string Scope { get; set; }

        public int ScopeId { get; set; }

        public int ZoneCount { get; set; }

        public string[] Zones { get; set; }

        public string ZoneDisplay { get; set; }

        public SolutionViewModel() { }

        public SolutionViewModel(Solution model)
        {
            
            Name = model.Name;
            Alias = model.Alias;
            Description = model.Description;
            SolutionId = model.SolutionId;
            ScopeId = model.ScopeId;
            Scope = SelectionConverter.Convert(model.ScopeId);

            var zonesList = SelectionListUtility.GetZoneDictionary();
            if (model.Zones != null)
            {
                ZoneCount = model.Zones.Length;
                List<string> zones = new List<string>();
                foreach (var zoneid in model.Zones)
                {
                    if (zonesList.ContainsKey(zoneid))
                    {
                        string zone = zonesList[zoneid];
                        zones.Add(zone);                        
                    }

                }
                int i = 0;
                StringBuilder sb = new StringBuilder();
                foreach (var zone in zones.OrderBy(x => x))
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(zone);
                    i++;
                }
                ZoneDisplay = sb.ToString();
                Zones = zones.ToArray();
            }
        }

        public override Solution ToModel()
        {
            Solution model = new Solution();
            model.SolutionId = SolutionId;
            model.Name = Name;
            model.Alias = Alias;
            model.Description = Description;
            model.ScopeId = ScopeId;
            return model;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
