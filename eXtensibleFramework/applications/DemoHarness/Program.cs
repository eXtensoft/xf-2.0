using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XF.Common;
using XF.Common.Wcf;


namespace DemoHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ready...");
            Console.ReadLine();
            GetSelections();
            //Execute();
            Console.WriteLine("done...");
            Console.ReadLine();
        }

        private static void Execute()
        {
            var svc = GetWcfModelService();

            Cyclops.Note model = new Cyclops.Note() { Subject = "success", Body = "body of note", Tds = DateTime.Now, UserIdentity = "princ" };

            var response = svc.Post<Cyclops.Note>(model);
            if (response.IsOkay)
            {
                Console.WriteLine("server created");
            }
            else
            {
                Console.WriteLine(response.Status.Description);
            }

        }

        private static void GetSelections()
        {
            string s = @"c:\users\princ\desktop\cyclops.data\arc.selections.xml";
            if (File.Exists(s))
            {
                FileInfo info = new FileInfo(s);
                string xml = File.ReadAllText(info.FullName);
                XDocument xdoc = XDocument.Parse(xml);
                var els = (from c in xdoc.Descendants("Selection") select new
                {
                    id = Int32.Parse(c.Element("SelectionId").Value),
                    display = c.Element("Display").Value,
                    token = c.Element("Token").Value,
                    Sort = Int32.Parse(c.Element("Sort").Value),
                    GroupId = Int32.Parse(c.Element("GroupId").Value),
                    Icon = c.Element("Icon").Value,
                    MasterId = Int32.Parse(c.Element("MasterId").Value)
                }).ToDataTable();
                //XmlDocument x =  new XmlDocument()
            }
        }

        //private static IModelRequestService GetModelService()
        //{
        //    IDataRequestService dataService = new DataRequestService(new ModelDataGatewayDataService());
        //    IModelRequestService service = new PassThroughModelRequestService(dataService);
        //    return service;
        //}

        //private static IRpcRequestService GetRpcService()
        //{
        //    IRpcDataRequestService dataService = new RpcDataRequestService(new RpcHandlerDataService());
        //    IRpcRequestService service = new PassThroughRpcRequestService(dataService);
        //    return service;
        //}

        private static IRpcRequestService GetWcfRpcService()
        {
            IRpcRequestService service = new WcfRpcRequestService();

            return service;
        }


        private static IModelRequestService GetWcfModelService()
        {
            IModelRequestService service = new WcfModelRequestService();
            return service;
        }
    }
}
