// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.ServiceModel;
    using XF.Common.Wcf;

    public class DataPacketServiceClient : ServiceClient<IDataPacketService> , IDataPacketService
    {
        public DataPacketServiceClient()
            :this("default"){}

        public DataPacketServiceClient(string endpointName)
        {
            Initialize(endpointName);
        }
        #region IDataPacketService Members

        DataPacket IDataPacketService.Post(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.Post(item);
            }
            catch (CommunicationException commEx)
            {
                response = item;
                response.SetError(500,commEx.Message);
                EventWriter.WriteError(commEx.Message, SeverityType.Warning);
            }
            catch(TimeoutException timeoutEx)
            {
                response = item;
                response.SetError(500,timeoutEx.Message);
                EventWriter.WriteError(timeoutEx.Message, SeverityType.Warning);
            }
            return response;
        }

        DataPacket IDataPacketService.Put(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.Put(item);
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

        DataPacket IDataPacketService.Delete(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.Delete(item);
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

        DataPacket IDataPacketService.Get(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.Get(item);
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

        DataPacket IDataPacketService.GetAll(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.GetAll(item);
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

        DataPacket IDataPacketService.GetAllProjections(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.GetAllProjections(item);
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

        DataPacket IDataPacketService.ExecuteAction(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.ExecuteAction(item);
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

        DataPacket IDataPacketService.ExecuteCommand(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.ExecuteCommand(item);
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

        DataPacket IDataPacketService.ExecuteMany(DataPacket item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.ExecuteMany(item);
            }
            catch (CommunicationException commEx)
            {
                response = item;
                response.SetError(500, commEx.Message);
                EventWriter.WriteError(commEx.Message, SeverityType.Warning);
            }
            catch (TimeoutException timeoutEx)
            {
                response = item;
                response.SetError(500, timeoutEx.Message);
                EventWriter.WriteError(timeoutEx.Message, SeverityType.Warning);
            }
            return response;
        }


        DataPacket IDataPacketService.ExecutePing(Ping item)
        {
            DataPacket response = null;
            try
            {
                response = Proxy.ExecutePing(item);
            }
            catch (CommunicationException commEx)
            {
                EventWriter.WriteError(commEx.Message, SeverityType.Warning);
            }
            catch (TimeoutException timeoutEx)
            {
                EventWriter.WriteError(timeoutEx.Message, SeverityType.Warning);
            }
            return response;
        }



        #endregion


    }
}
