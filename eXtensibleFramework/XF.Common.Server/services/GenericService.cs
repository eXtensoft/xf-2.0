// <copyright company="eXtensible Solutions LLC" file="GenericService.cs">
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
    public class GenericService : IDataPacketService, IRpcDataPacketService
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

        #region RpcDataService (IRpcDataRequestService)

        private IRpcDataRequestService _RpcDataService = new RpcDataRequestService(new RpcHandlerDataService());

        /// <summary>
        /// Gets or sets the IDataRequestService value for DataService
        /// </summary>
        /// <value> The IDataRequestService value.</value>

        public IRpcDataRequestService RpcDataService
        {
            get { return _RpcDataService; }
            set
            {
                if (_RpcDataService != value)
                {
                    _RpcDataService = value;
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

        #region IRpcDataPacketService Members

        DataPacket IRpcDataPacketService.ExecuteRpc(DataPacket item)
        {
            return PassThroughRpc(item);
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
            if (modelType != null)
            {
                bool b = false;
                List<Type> types = new List<Type>();
                types.Add(modelType);
                if (!String.IsNullOrEmpty(item.SecondaryTypename))
                {
                    b = true;
                    Type secondaryType = Type.GetType(item.SecondaryTypename);
                    types.Add(secondaryType);
                }
                Type contracts = typeof(IDataRequestService);
                MethodInfo dataRequestContracts = contracts.GetMethod(item.ModelAction.ToString());
                MethodInfo standardContractMethod = dataRequestContracts.MakeGenericMethod(types.ToArray());
                var mT = !b ? typeof(Message<>) : typeof(Message<,>);
                //var closedModel = mT.MakeGenericType(new Type[] { modelType });
                var closedModel = mT.MakeGenericType(types.ToArray() );
                dynamic message = Activator.CreateInstance(closedModel, item.Items);
                message.Criterion = item.Criteria;

                if (verbsWithModel.Contains(item.ModelAction) && item.Buffer != null)
                {
                    object model = GenericSerializer.ByteArrayToItem(item.Buffer);
                    message.InsertContent(model);
                }
                else if(item.ModelAction.Equals(ModelActionOption.ExecuteMany) && item.Buffer != null)
                {
                    dynamic list = GenericSerializer.ByteArrayToItem(item.Buffer);
                    message.InsertContentList(list);
                }
                else if (item.ModelAction.Equals(ModelActionOption.ExecuteCommand) && item.Tables != null)
                {
                    message.Data = item.Tables;
                }
                InvokeStandardContract(DataService, standardContractMethod, new object[1] { message }, item);
                
            }
            else
            {
                item.SetError(500,String.Format("The GenericService could not resolve type '{0}'.\r\nMake certain the 'Model' dll is accessible", item.Typename));
            }


            return item;
        }

        private DataPacket PassThroughRpc(DataPacket item)
        {
            Type modelType = Type.GetType(item.Typename);
            if (modelType != null)
            {
                string returnTypename = item.Criteria.GetValue<string>(XFConstants.Application.ActionResultModelType);
                Type returnType = Type.GetType(returnTypename);

                if (returnType != null)
                {
                    Type contracts = typeof(IRpcDataRequestService);
                    MethodInfo dataRequestContracts = contracts.GetMethod("ExecuteRpc");
            
                    MethodInfo standardContractMethod = dataRequestContracts.MakeGenericMethod(modelType,returnType);
                    var mT = typeof(Message<,>);
                    var closedModel = mT.MakeGenericType(new Type[] { modelType,returnType });
                    dynamic message = Activator.CreateInstance(closedModel, item.Items);
                    message.Criterion = item.Criteria;

                    object model = GenericSerializer.ByteArrayToItem(item.Buffer);
                    message.InsertContent(model);

                    InvokeStandardContract(RpcDataService, standardContractMethod, new object[1] { message }, item);                     
                }
                else
                {
                    string message = String.Format("The RpcService could not resolve type '{0}'.\r\nMake certain the 'Model' dll is accessible", returnTypename);
                    item.SetError(500, message );
                    EventWriter.WriteError(message, SeverityType.Error);

                }              
            }
            else
            {
                string message = String.Format("The RpcService could not resolve type '{0}'.\r\nMake certain the 'Model' dll is accessible", item.Typename);
                item.SetError(500, message);
                EventWriter.WriteError(message, SeverityType.Error);
            }

            return item;
        }


        private static void InvokeStandardContract(IDataRequestService dataService, MethodInfo info, object[] param, DataPacket item)
        {
            dynamic o = info.Invoke(dataService, param);
            o.Assimilate(item);
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
