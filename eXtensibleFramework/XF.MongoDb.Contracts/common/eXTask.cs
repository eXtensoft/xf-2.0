// <copyright company="eXtensible Solutions, LLC" file="eXTask.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common.MongoDb
{
    using System;
    using System.Runtime.Serialization;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class eXTask : XF.Common.eXTask
    {
        [DataMember]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }

}
