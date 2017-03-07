using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XF.Common;
using XF.Common.Wcf;

namespace DemoService
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenHost();
        }

        private static void OpenHost()
        {
            //using (ServiceHost rpcHost = new ServiceHost(typeof(RpcDataPacketService)))
            //using (ServiceHost host = new ServiceHost(typeof(DataPacketService)))
            using (ServiceHost genericHost = new ServiceHost(typeof(GenericService)))
            {
                // host.Open();
                //ColorConsole.WriteLine(ConsoleColor.Green,"Generic Service started...");
                // rpcHost.Open();
                // ColorConsole.WriteLine(ConsoleColor.DarkRed, "Rpc Service started...");
                genericHost.Open();
                //ColorConsole.WriteLine(ConsoleColor.DarkMagenta, "Generic Service started...");
                Console.WriteLine("Generic Service started...");
                Console.ReadLine();
            }
        }
    }
}
