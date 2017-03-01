// <copyrigh company="eXtensible Solutions LLC"t file="MongoUpdate.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using MongoDB.Driver;

    public class MongoUpdate
    {
        public IMongoQuery Query { get; set; }
        public IMongoUpdate Update { get; set; }
    }
}
