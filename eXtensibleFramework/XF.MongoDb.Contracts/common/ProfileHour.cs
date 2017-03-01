// <copyright file="ProfileHour.cs" company="eXtensible Solutions LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
    using XF.Common.MongoDb;


    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class ProfileHour
    {
        [DataMember]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Month { get; set; }

        [DataMember]
        public int Day { get; set; }

        [DataMember]
        public int Hour { get; set; }

        [DataMember]
        public int Count { get; set; }

        public List<MongoDb.ProfileItem> Items { get; set; }
    }
}