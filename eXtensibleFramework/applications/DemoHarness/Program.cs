using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XF.Common;
using XF.Common.Wcf;
using XF.Common.Special;


namespace DemoHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ready...");
            Console.ReadLine();
            //GetSelections();
            //Execute();
            // ExecuteSchema();
            //ConfigProvide();
            ExecuteStyx();
            //ExecuteAlert();
            Console.WriteLine("done...");
            Console.ReadLine();
        }
        private static void ExecuteStyx()
        {
            var svc = GetWcfModelService();

            Styx.ProjectManagement.Task t = new Styx.ProjectManagement.Task()
            {
                Title = "Work",
                Body = "Work Hard",
                Phase = "Phase 0",
                Group = "group",
                GroupToken = "group-token",
                TaskType = "task-type",
                CurrentState = "none"
            };
        
            var response = svc.Post<Styx.ProjectManagement.Task>(t);
            Console.WriteLine(response.Status.Code);
            if (response.IsOkay)
            {

            }
            else
            {
                Console.WriteLine(response.Status.Description);
            }
        }
        private static void ExecuteSchema()
        {
            DateTime d = DateTime.Now;
            Console.WriteLine(d.ToLongDateString());
            Console.WriteLine(d.ToSchema( DateTimeSchemaOption.None));
            Console.WriteLine(d.ToSchema(DateTimeSchemaOption.None, "foo"));
            Console.WriteLine(d.ToSchema(DateTimeSchemaOption.WeekOfYear));
            Console.WriteLine(d.ToSchema(DateTimeSchemaOption.DayOfWeek));
            Console.WriteLine(d.ToSchema(DateTimeSchemaOption.DayOfYear));
            Console.WriteLine(d.ToSchema(DateTimeSchemaOption.MonthOfYear));
            Console.WriteLine(d.ToSchema(DateTimeSchemaOption.HourOfDay));
            Console.WriteLine(d.ToSchema(DateTimeSchemaOption.HourOfDayOfWeek));
            Console.WriteLine(d.ToString(XFConstants.DateTimeFormat));
            Console.WriteLine(d.ToString("HH"));

        }

        private static void ExecuteAlert()
        {
            //List<string> list = new List<string>();
            //list.Add(AlertAudiences.Developer.ToString());
            //list.Add(AlertAudiences.CTO.ToString());
            //var props = eXtensibleConfig.GetProperties();
            //AlertWriter.Alert("alert title","alert message",new string[] { "DataAccess","METL" }, ScaleOption.High, ScaleOption.High,props,list.ToArray());

            AlertWriter alert = new AlertWriter("source", "title", "message");
            alert.Categories = AlertCategories.WebService | AlertCategories.Database;
            alert.Audiences = AlertAudiences.Business | AlertAudiences.Operations;
            alert.Importance = ScaleOption.Low;
            alert.Urgency = ScaleOption.MediumHigh;
            alert.SendAlert();

        }

        private static void ConfigProvide()
        {
            foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
            {
                Console.WriteLine(String.Format("{0}\t{1}", item.Name, item.ConnectionString));
            }
            foreach (ConnectionStringSettings item in ConfigurationProvider.ConnectionStrings)
            {
                ColorConsole.WriteLine(ConsoleColor.Blue,String.Format("{0}\t{1}", item.Name, item.ConnectionString));
            }
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
