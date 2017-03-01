

namespace XF.Common.Discovery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public static class DataMapProvider
    {
        private static List<DataMap> _Maps;

        static DataMapProvider()
        {
            InitializeMaps();
        }

        private static void InitializeMaps()
        {
            List<string> list = DiscoveryResources.DataMaps.ToListOfString();
            _Maps = new List<DataMap>();
            //foreach (string item in list)
            for (int i = 1; i < list.Count; i++)
            {
                string item = list[i];
                string[] t = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (t.Length == 6)
                {
                    try
                    {
                        XF.Common.Discovery.DataMap map = new XF.Common.Discovery.DataMap()
                        {
                            DatabaseType = t[0]
                            ,
                            ClrType = String.Format("System.{0}", t[1])
                            ,
                            DataAccessType = (System.Data.DbType)Enum.Parse(typeof(System.Data.DbType), t[2])
                            ,
                            DataReaderAccessor = t[4]
                        };
                        _Maps.Add(map);
                    }
                    catch (Exception ex)
                    {
                        int j = 0;
                    }
                }

            }
        }


        internal static string TranslateToSqlAssessor(string p)
        {
            var found = _Maps.Find(x => x.DatabaseType.ToString().Equals(p, StringComparison.OrdinalIgnoreCase));
            if (found != null)
            {
                return found.DataReaderAccessor;
            }
            else
            {
                return "GET" + p;
            }
        }
    }
}
