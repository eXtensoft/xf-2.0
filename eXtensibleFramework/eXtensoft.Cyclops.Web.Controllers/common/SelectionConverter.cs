using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Common;

namespace Cyclops
{
    public static class SelectionConverter
    {
        public static string Convert(int selectionId)
        {
            var items = GetSelections();
            var found = items.ToList().Find(x => x.SelectionId.Equals(selectionId));
            return found != null ? found.Display : String.Empty;
        }

        public static int ConvertToId(string selectionDisplay)
        {
            var items = GetSelections();
            var found = items.ToList().Find(x => x.Token.Equals(selectionDisplay, StringComparison.OrdinalIgnoreCase));
            return found != null ? found.SelectionId : 0;
        }


        private static IEnumerable<Selection> GetSelections()
        {
            List<Selection> list = new List<Selection>();
            var service = GetService();
            var response = service.GetAll<Selection>(null);
            if (response.IsOkay)
            {
                list = response.ToList();
            }
            return list;
        }


        private static IModelRequestService GetService()
        {
            return new PassThroughModelRequestService(
                        new DataRequestService(new XF.DataServices.ModelDataGatewayDataService())
                        );
        }
    }
}
