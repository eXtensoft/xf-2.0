// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract(Namespace="http://eXtensoft/xf/schemas/2017/09")]
    [ServiceKnownType(typeof(TypedItem))]
    [ServiceKnownType(typeof(EventTypeOption))]
    public interface IEventService 
    {
        [OperationContract]
        void Write(EventTypeOption option, List<TypedItem> items);
    }
    
}
