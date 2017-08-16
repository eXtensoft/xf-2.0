// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;

    [ServiceContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    [ServiceKnownType(typeof(AlertInterest))]
    [ServiceKnownType(typeof(AlertTarget))]
    [ServiceKnownType(typeof(NotificationSetting))]
    [ServiceKnownType(typeof(CommunicationTypeOption))]
    [ServiceKnownType(typeof(SuspensionModeOption))]
    [ServiceKnownType(typeof(List<AlertTarget>))]
    [ServiceKnownType(typeof(OperatorTypeOption))]
    [ServiceKnownType(typeof(AlertSubscriber))]
    [ServiceKnownType(typeof(List<AlertInterest>))]
    [ServiceKnownType(typeof(List<AlertSubscriber>))]
    [ServiceKnownType(typeof(List<object>))]
    public interface IAlertService
    {
        [OperationContract]
        void Post(AlertSubscriber model);
        [OperationContract]
        void Put(AlertSubscriber model);
        [OperationContract]
        void Delete(string id);
        [OperationContract]
        AlertSubscriber Get(string id);
        [OperationContract]
        IEnumerable<AlertSubscriber> GetAll();
        [OperationContract]
        IEnumerable<IProjection> GetAllProjections();

    }
}
