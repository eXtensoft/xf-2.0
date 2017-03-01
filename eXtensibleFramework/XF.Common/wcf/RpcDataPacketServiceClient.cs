// <copyright company="eXtensible Solutions LLC" file="RpcDataPacketServiceClient.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.ServiceModel;
    using XF.Common.Wcf;

    public class RpcDataPacketServiceClient : ServiceClient<IRpcDataPacketService>, IRpcDataPacketService
    {
        public RpcDataPacketServiceClient()
            : this("default") { }

        public RpcDataPacketServiceClient(string endpointName)
        {
            Initialize(endpointName);
        }
        #region IDataPacketService Members


        DataPacket IRpcDataPacketService.ExecuteRpc(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.ExecuteRpc(item);
            }
            catch (CommunicationException commEx)
            {
                response = item;
                response.SetError(500,commEx.Message);
                EventWriter.WriteError(commEx.Message, SeverityType.Warning);
            }
            catch (TimeoutException timeoutEx)
            {
                response = item;
                response.SetError(500,timeoutEx.Message);
                EventWriter.WriteError(timeoutEx.Message, SeverityType.Warning);
            }
            return response;
        }


        #endregion

    }
}
