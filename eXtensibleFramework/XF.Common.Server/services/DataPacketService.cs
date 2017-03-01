// <copyright company="eXtensible Solutions LLC" file="DataPacketService.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.ServiceModel;
    using XF.DataServices;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class DataPacketService : IDataPacketService
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

        private IDataRequestService _DataService = new DataRequestService(new ModelDataGatewayDataService());

        /// <summary>
        /// Gets or sets the IDataRequestService value for DataService
        /// </summary>
        /// <value> The IDataRequestService value.</value>

        public IDataRequestService DataService
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



        #region IDataPacketService Members

        DataPacket IDataPacketService.Post(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.Put(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.Delete(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.Get(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.GetAll(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.GetAllProjections(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.ExecuteAction(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.ExecuteCommand(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.ExecuteMany(DataPacket item)
        {
            return PassThrough(item);
        }

        DataPacket IDataPacketService.ExecutePing(Ping item)
        {
            return ExecutePing(item);
        }



        #endregion

        #region local DataPacketService implementations
        private static IList<ModelActionOption> verbsWithModel = new List<ModelActionOption>
        {
            ModelActionOption.Post,
            ModelActionOption.Put,
            ModelActionOption.ExecuteAction
        };
        private DataPacket PassThrough(DataPacket item)
        {
            Type modelType = Type.GetType(item.Typename);
            if (modelType == null)
            {
                item.SetError(500, String.Format("Could not resolve the type: '{0}'", item.Typename));
            }
            else
            {
                Type contracts = typeof(IDataRequestService);
                MethodInfo dataRequestContracts = contracts.GetMethod(item.ModelAction.ToString());           
                MethodInfo standardContractMethod = dataRequestContracts.MakeGenericMethod(modelType);
                var mT = typeof(Message<>);
                var closedModel = mT.MakeGenericType(new Type[] { modelType });
                dynamic message = Activator.CreateInstance(closedModel, item.Items);
                message.Criterion = item.Criteria;

                if (verbsWithModel.Contains(item.ModelAction) && item.Buffer != null)
                {
                    object model = GenericSerializer.ByteArrayToItem(item.Buffer);
                    //var m = Convert.ChangeType(model, modelType);
                    message.TryAdd(model);
                    //message.Content.Insert(0, m);
                    
                }
                InvokeStandardContract(DataService, standardContractMethod, new object[1]{message}, item);                
            }
            
            return item;           
        }


        private static void InvokeStandardContract(IDataRequestService dataService, MethodInfo info, object[] param, DataPacket item)
        {
            dynamic o = info.Invoke(dataService, param );
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
