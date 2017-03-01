// <copyright file="IDataPacketService.cs" company="eXtensible Solutions LLC">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;

    [ServiceContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    [ServiceKnownType(typeof(Ping))]
    [ServiceKnownType(typeof(TypedItem))]
    [ServiceKnownType(typeof(Criterion))]
    [ServiceKnownType(typeof(ApplicationContext))]
    [ServiceKnownType(typeof(DataTable))]
    [ServiceKnownType(typeof(DateTimeOffset))]
    public interface IDataPacketService 
    {
        [OperationContract]
        DataPacket Post(DataPacket item);

        [OperationContract]
        DataPacket Put(DataPacket item);

        [OperationContract]
        DataPacket Delete(DataPacket item);

        [OperationContract]
        DataPacket Get(DataPacket item);

        [OperationContract]
        DataPacket GetAll(DataPacket item);

        [OperationContract]
        DataPacket GetAllProjections(DataPacket item);

        [OperationContract]
        DataPacket ExecuteAction(DataPacket item);

        [OperationContract]
        DataPacket ExecuteCommand(DataPacket item);

        [OperationContract]
        DataPacket ExecuteMany(DataPacket item);

        [OperationContract]
        DataPacket ExecutePing(Ping item);

    }
}
