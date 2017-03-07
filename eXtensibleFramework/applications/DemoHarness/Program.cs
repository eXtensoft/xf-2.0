using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Execute();
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
