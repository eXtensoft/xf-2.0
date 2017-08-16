// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Wcf
{
    using System;
    using System.Data;
    using System.ServiceModel;

    [ServiceContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    [ServiceKnownType(typeof(Ping))]
    [ServiceKnownType(typeof(TypedItem))]
    [ServiceKnownType(typeof(Criterion))]
    [ServiceKnownType(typeof(ApplicationContext))]
    [ServiceKnownType(typeof(DataTable))]
    [ServiceKnownType(typeof(DateTimeOffset))]
    public interface IRpcDataPacketService
    {

        [OperationContract]
        DataPacket ExecuteRpc(DataPacket item);

    }
}
