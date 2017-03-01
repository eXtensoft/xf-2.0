//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace XF.Common.Data
//{
//    public static class DataMapProvider
//    {
//        private static List<DataMap> _Maps;

//        static DataMapProvider()
//        {
//            InitializeMaps();
//        }

//        private static void InitializeMaps()
//        {
//            List<string> list = DiscoveryResources.DataMaps.ToListOfString();
//            _Maps = new List<DataMap>();
//            //foreach (string item in list)
//            for (int i = 1; i < list.Count; i++)
//            {
//                string item = list[i];
//                string[] t = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
//                if (t.Length == 6)
//                {
//                    try
//                    {
//                        DataMap map = new DataMap()
//                        {
//                            DatabaseType = t[0]
//                            ,
//                            ClrType = String.Format("System.{0}", t[1])
//                            ,
//                            DataAccessType = (System.Data.DbType)Enum.Parse(typeof(System.Data.DbType), t[2])
//                            ,
//                            DataReaderAccessor = t[4]
//                        };
//                        _Maps.Add(map);
//                    }
//                    catch (Exception ex)
//                    {
//                        int j = 0;
//                    }
//                }

//            }
//            //DataTable dt = _Maps.ToDataTable();
//            //GenericObjectManager.WriteGenericList<DataMap>(_Maps, @"c:\local\dmSat.xml");
//        }

//        internal static System.Data.DbType TranslateToDbType(string p)
//        {
//            var found = _Maps.Find(x => x.DatabaseType.Equals(p, StringComparison.OrdinalIgnoreCase));
//            if (found != null)
//            {
//                return found.DataAccessType;
//            }
//            else
//            {
//                return System.Data.DbType.Object;
//            }
//        }

//        internal static string TranslateToSqlAssessor(string p)
//        {
//            var found = _Maps.Find(x => x.DatabaseType.ToString().Equals(p, StringComparison.OrdinalIgnoreCase));
//            if (found != null)
//            {
//                return found.DataReaderAccessor;
//            }
//            else
//            {
//                return "GET" + p;
//            }
//        }
//    }
//}
