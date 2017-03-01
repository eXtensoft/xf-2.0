// <copyright company="eXtensible Solutions LLC" file="IAlertService.cs">
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
