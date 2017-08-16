// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Threading.Tasks;
    using XF.Common.Wcf;

    public class WcfRpcRequestService : ModelRequestService, IRpcRequestService
    {

        #region DataPacketService (IDataPacketService)

        private IRpcDataPacketService _DataPacketService = new RpcDataPacketServiceClient("default-rpc");

        /// <summary>
        /// Gets or sets the IDataPacketService value for DataPacketService
        /// </summary>
        /// <value> The IDataPacketService value.</value>

        public IRpcDataPacketService DataPacketService
        {
            get { return _DataPacketService; }
            set
            {
                if (_DataPacketService != value)
                {
                    _DataPacketService = value;
                }
            }
        }

        #endregion

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


        IResponse<T, U> IRpcRequestService.ExecuteRpc<T, U>(T model, ICriterion criterion)
        {
            return ExecuteRpc<T, U>(model, criterion);
        }

        void IRpcRequestService.ExecuteRpcAsync<T, U>(T model, ICriterion criterion, Action<IResponse<T, U>> callback)
        {
            new Action(async () =>
            {
                IResponse<T, U> result = await Task.Run<IResponse<T, U>>(() => ExecuteRpcAsync<T, U>(model, criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        private IResponse<T, U> ExecuteRpc<T, U>(T model, ICriterion criterion) where T : class, new()
        {
            
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T>(Context, ModelActionOption.ExecuteAction, c.Items);
            packetRequest.Buffer = GenericSerializer.ItemToByteArray(model);
            //object o = GenericSerializer.Deserialize<T>(packetRequest.Buffer);
            //object oo = GenericSerializer.ByteArrayToItem(packetRequest.Buffer,typeof(T));
            
            string s = Activator.CreateInstance<U>().GetType().AssemblyQualifiedName;
            packetRequest.Criteria.Add(XFConstants.Application.ActionResultModelType, s);

            DataPacket packetResponse = _DataPacketService.ExecuteRpc(packetRequest);

            IResponse<T,U> response = _Serializer.Deserialize<T,U>(packetResponse);

            return response;
        }

        private Task<IResponse<T, U>> ExecuteRpcAsync<T, U>(T model, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T, U>>(() => ExecuteRpc<T, U>(model, criterion));
        }



    }
}
