// <copyright file="IStatusCheck.cs" company="eXtensible Solutions LLC">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.ServiceModel;

    [ServiceContract(Namespace = "http://eXtensibleSolution/schemas/2014/04")]
    [ServiceKnownType(typeof(ListItem))]
    [ServiceKnownType(typeof(TypedItem))]
    [ServiceKnownType(typeof(StatusCheck))]
    public interface IStatusCheck
    {
        [OperationContract]
        StatusCheck ExecuteStatusCheck(StatusCheck item);
    }
}
