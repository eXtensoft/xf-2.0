// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public class WcfModelRequestService : ModelRequestService, IModelRequestService
    {

        #region DataPacketService (IDataPacketService)

        private IDataPacketService _DataPacketService = new DataPacketServiceClient();

        /// <summary>
        /// Gets or sets the IDataPacketService value for DataPacketService
        /// </summary>
        /// <value> The IDataPacketService value.</value>

        public IDataPacketService DataPacketService
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

        #region IModelRequestService Members

        IResponse<T> IModelRequestService.Post<T>(T model)
        {
            return Post<T>(model);
        }

        IResponse<T> IModelRequestService.Put<T>(T model, ICriterion criterion)
        {
            return Put<T>(model, criterion);
        }

        IResponse<T> IModelRequestService.Delete<T>(ICriterion criterion)
        {
            return Delete<T>(criterion);
        }

        IResponse<T> IModelRequestService.Get<T>(ICriterion criterion)
        {
            return Get<T>(criterion);
        }

        IResponse<T> IModelRequestService.GetAll<T>(ICriterion criterion)
        {
            return GetAll<T>(criterion);
        }

        IResponse<T> IModelRequestService.GetAllProjections<T>(ICriterion criterion)
        {
            return GetAllProjections<T>(criterion);
        }

        IResponse<T, U> IModelRequestService.ExecuteAction<T, U>(T model, ICriterion criterion)
        {
            return ExecuteAction<T, U>(model, criterion);
        }

        IResponse<T> IModelRequestService.ExecuteCommand<T>(System.Data.DataSet ds, ICriterion criterion)
        {
            return ExecuteCommand<T>(ds, criterion);
        }

        IResponse<T, U> IModelRequestService.ExecuteMany<T, U>(IEnumerable<T> list, ICriterion criterion)
        {
            return ExecuteMany<T, U>(list, criterion);
        }


        void IModelRequestService.PostAsync<T>(T model, Action<IResponse<T>> callback)
        {
            new Action(async () =>
            {
                IResponse<T> result = await Task.Run<IResponse<T>>(() => PostAsync<T>(model));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.PutAsync<T>(T model, ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
            {
                IResponse<T> result = await Task.Run<IResponse<T>>(() => PutAsync<T>(model, criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.DeleteAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
            {
                IResponse<T> result = await Task.Run<IResponse<T>>(() => DeleteAsync<T>(criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.GetAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
            {
                IResponse<T> result = await Task.Run<IResponse<T>>(() => GetAsync<T>(criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.GetAllAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
            {
                IResponse<T> result = await Task.Run<IResponse<T>>(() => GetAllAsync<T>(criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.GetAllProjectionsAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
            {
                IResponse<T> result = await Task.Run<IResponse<T>>(() => GetAllProjectionsAsync<T>(criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.ExecuteActionAsync<T, U>(T model, ICriterion criterion, Action<IResponse<T, U>> callback)
        {
            new Action(async () =>
            {
                IResponse<T, U> result = await Task.Run<IResponse<T, U>>(() => ExecuteActionAsync<T, U>(model, criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.ExecuteCommand<T>(DataSet ds, ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
            {
                IResponse<T> result = await Task.Run<IResponse<T>>(() => ExecuteCommand<T>(ds, criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.ExecuteManyAsync<T, U>(IEnumerable<T> list, ICriterion criterion, Action<IResponse<T, U>> callback)
        {
            new Action(async () =>
            {
                IResponse<T, U> result = await Task.Run<IResponse<T, U>>(() => ExecuteManyAsync<T, U>(list, criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        #endregion


        #region local IModelRequestService implementations

        private IResponse<T> Post<T>(T model) where T : class, new()
        {
            DataPacket packetRequest = _Serializer.Serialize<T>(Context,ModelActionOption.Post,model);

            DataPacket packetResponse = DataPacketService.Post(packetRequest);

            IResponse<T> response = _Serializer.Deserialize<T>(packetResponse);

            return response;
        }

        private IResponse<T> Put<T>(T model, ICriterion criterion) where T : class, new()
        {
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T>(Context, ModelActionOption.Put, model, c.Items);

            DataPacket packetResponse = _DataPacketService.Get(packetRequest);

            IResponse<T> response = _Serializer.Deserialize<T>(packetResponse);

            return response;
        }

        private IResponse<T> Delete<T>(ICriterion criterion) where T : class, new()
        {
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T>(Context, ModelActionOption.Delete, c.Items);

            DataPacket packetResponse = _DataPacketService.Delete(packetRequest);

            IResponse<T> response = _Serializer.Deserialize<T>(packetResponse);

            return response;
        }

        private IResponse<T> Get<T>(ICriterion criterion) where T : class, new()
        {
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T>(Context, ModelActionOption.Get, c.Items);

            DataPacket packetResponse = _DataPacketService.Get(packetRequest);

            IResponse<T> response = _Serializer.Deserialize<T>(packetResponse);

            return response;
        }

        private IResponse<T> GetAll<T>(ICriterion criterion) where T : class, new()
        {
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T>(Context, ModelActionOption.GetAll, c.Items);

            DataPacket packetResponse = _DataPacketService.GetAll(packetRequest);

            IResponse<T> response = _Serializer.Deserialize<T>(packetResponse);

            return response;

        }

        private IResponse<T> GetAllProjections<T>(ICriterion criterion) where T : class, new()
        {
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T>(Context, ModelActionOption.GetAllProjections, c.Items);

            DataPacket packetResponse = _DataPacketService.GetAllProjections(packetRequest);

            IResponse<T> response = _Serializer.Deserialize<T>(packetResponse);

            return response;
        }

        private IResponse<T, U> ExecuteAction<T, U>(T model, ICriterion criterion) where T : class, new()
        {
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T,U>(Context, ModelActionOption.ExecuteAction,model, c.Items);

            DataPacket packetResponse = _DataPacketService.ExecuteAction(packetRequest);

            IResponse<T,U> response = _Serializer.Deserialize<T,U>(packetResponse);

            return response;
        }

        private IResponse<T> ExecuteCommand<T>(DataSet ds, ICriterion criterion) where T : class, new()
        {
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T>(Context, ModelActionOption.ExecuteCommand, c.Items);
            packetRequest.Tables = ds;

            DataPacket packetResponse = _DataPacketService.ExecuteCommand(packetRequest);

            IResponse<T> response = _Serializer.Deserialize<T>(packetResponse);

            return response;
        }

        private IResponse<T, U> ExecuteMany<T, U>(IEnumerable<T> list, ICriterion criterion) where T : class, new()
        {
            ICriterion c = criterion ?? new Criterion();
            DataPacket packetRequest = _Serializer.Serialize<T,U>(Context, ModelActionOption.ExecuteMany, list, c.Items);

            DataPacket packetResponse = _DataPacketService.ExecuteAction(packetRequest);

            IResponse<T, U> response = _Serializer.Deserialize<T, U>(packetResponse);

            return response;
        }

        private Task<IResponse<T>> PostAsync<T>(T t) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Post<T>(t));
        }

        private Task<IResponse<T>> PutAsync<T>(T t, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Put<T>(t, criterion));
        }

        private Task<IResponse<T>> DeleteAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Delete<T>(criterion));
        }


        private Task<IResponse<T>> GetAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Get<T>(criterion));
        }

        private Task<IResponse<T>> GetAllAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => GetAll<T>(criterion));
        }

        private Task<IResponse<T>> GetAllProjectionsAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => GetAllProjections<T>(criterion));
        }

        private Task<IResponse<T, U>> ExecuteActionAsync<T, U>(T model, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T, U>>(() => ExecuteAction<T, U>(model, criterion));
        }

        private Task<IResponse<T>> ExecuteCommandAsync<T>(DataSet ds, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => ExecuteCommand<T>(ds, criterion));
        }

        private Task<IResponse<T, U>> ExecuteManyAsync<T, U>(IEnumerable<T> list, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T, U>>(() => ExecuteMany<T, U>(list, criterion));
        }

        #endregion




    }
}
