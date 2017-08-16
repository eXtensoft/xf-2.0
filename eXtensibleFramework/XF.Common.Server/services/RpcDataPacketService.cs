// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Wcf
{
    using System;
    using System.Reflection;
    using System.ServiceModel;
    using XF.DataServices;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class RpcDataPacketService : IRpcDataPacketService
    {

        #region Serializer (DataPacketSerializer)

        private DataPacketSerializer _Serializer = new DataPacketSerializer();

        /// <summary>
        /// Gets or sets the DataPacketSerializer value for Serializer
        /// </summary>
        /// <value> The DataPacketSerializer value.</value>

        public DataPacketSerializer Serializer
        {
            get { return _Serializer; }
            set
            {
                if (_Serializer != value)
                {
                    _Serializer = value;
                }
            }
        }

        #endregion

        #region DataService (IDataRequestService)

        private IRpcDataRequestService _DataService = new RpcDataRequestService(new RpcHandlerDataService());

        /// <summary>
        /// Gets or sets the IDataRequestService value for DataService
        /// </summary>
        /// <value> The IDataRequestService value.</value>

        public IRpcDataRequestService DataService
        {
            get { return _DataService; }
            set
            {
                if (_DataService != value)
                {
                    _DataService = value;
                }
            }
        }

        #endregion

        #region IRpcDataPacketService Members

        DataPacket IRpcDataPacketService.ExecuteRpc(DataPacket item)
        {
            return PassThroughRpc(item);
        }

        #endregion

        #region local RpcDataPacketService implementations

        private DataPacket PassThroughRpc(DataPacket item)
        {
            Type modelType = Type.GetType(item.Typename);
            Type contracts = typeof(IRpcDataRequestService);
            MethodInfo dataRequestContracts = contracts.GetMethod(item.ModelAction.ToString());
            MethodInfo standardContractMethod = dataRequestContracts.MakeGenericMethod(modelType);
            var mT = typeof(Message<>);
            var closedModel = mT.MakeGenericType(new Type[] { modelType });
            dynamic message = Activator.CreateInstance(closedModel, item.Items);
            message.Criterion = item.Criteria;

            InvokeStandardContract(DataService, standardContractMethod, new object[1] { message }, item);


            return item;
        }


        private static void InvokeStandardContract(IRpcDataRequestService dataService, MethodInfo info, object[] param, DataPacket item)
        {
            dynamic o = info.Invoke(dataService, param);
            o.Assimilate(item);
        }


        private DataPacket ExecutePing(Ping item)
        {
            //throw new NotImplementedException();
            return new DataPacket();
        }

        #endregion

    }
}
