// <copyright company="eXtensible Solutions, LLC" file="ExtensionMethods.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.DataServices
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using XF.Common;

    public static class ExtensionMethods
    {
        public static bool ContainsKey<T>(this List<TypedItem> list, string key)
        {
            bool b = false;
            Type type = typeof(T);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list.Where(x => x.Value != null))
                {

                    if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase) && item.Value.GetType().Equals(typeof(T)))
                    {
                        b = true;
                        break;
                    }
                }
            }
            return b;
        }

        public static T Get<T>(this List<TypedItem> list, string key)
        {
            T t = default(T);
            bool b = false;
            if (list != null && list.Count > 0)
            {
                for (int i = 0; !b && i < list.Count; i++)
                {
                    var item = list[i];
                    if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase) && item.Value is T)
                    {
                        t = (T)item.Value;
                        b = true;
                    }
                }
            }
            return t;
        }

        private static IList<string> queryExclusions = new List<string>
        {
            {"skip"},
            {"take"},
            {"limit"},
            {"id"}
        };

        public static QueryDocument ToQueryDocument<T>(this ICriterion criterion) where T : class, new()
        {
            return criterion.ToQueryDocument<T>("id");
        }

        public static QueryDocument ToQueryDocument<T>(this ICriterion criterion, string modelId) where T : class, new()
        {
            string modelName = GetModelName<T>().ToLower();
            string[] t = new string[2] { modelId, modelName };
            Dictionary<string, object> d = new Dictionary<string, object>();
            HashSet<string> hs = new HashSet<string>();
            foreach (var item in criterion.Items)
            {
                if (hs.Add(item.Key))
                {
                    if (t.Contains(item.Key.ToLower()))
                    {
                        d.Add("_id", new ObjectId(item.Value.ToString()));
                    }
                    else if(!queryExclusions.Contains(item.Key.ToLower()))
                    {
                        d.Add(item.Key, item.Value);
                    }
                    
                }
            }
            return new QueryDocument(d);
        }

        public static IMongoQuery ToMongoQuery(this ICriterion criterion)
        {
            List<IMongoQuery> list = new List<IMongoQuery>();
            foreach (var item in criterion.Items)
            {
                if (!queryExclusions.Contains(item.Key.ToLower()))
                {
                    list.Add(ToMongoQuery(item));
                }
            }

            return Query.And(list);
               
        }

        public static IMongoUpdate ToMongoPatch(this ICriterion criterion)
        {
            int i = 0;
            UpdateBuilder builder = null;
            foreach (var item in criterion.Items)
            {
                HashSet<string> hs = new HashSet<string>();
                if (!queryExclusions.Contains(item.Key.ToLower()))
                {
                    if (hs.Add(item.Key.ToLower()))
                    {
                        if (i++ == 0)
                        {
                            builder = Update.Set(item.Key, item.ToBsonValue());
                        }
                        else
                        {
                            builder.Set(item.Key, item.ToBsonValue());
                        }
                    }
                }
            }
            return builder;
        }

        public static IDictionary<string,object> ToPatchDictionary(this ICriterion criterion)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (var item in criterion.Items)
            {
                HashSet<string> hs = new HashSet<string>();
                if (!queryExclusions.Contains(item.Key.ToLower()))
                {
                    if (hs.Add(item.Key.ToLower()))
                    {
                        d.Add(item.Key, item.Value);
                    }
                }
            }
            return d;
        }

        private static IMongoQuery ToMongoQuery(TypedItem item)
        {
            IMongoQuery query = null;

            switch (item.Operator)
            {
                case OperatorTypeOption.None:
                case OperatorTypeOption.EqualTo:
                    query = Query.EQ(item.Key, item.ToBsonValue());//object to BsonValue
                    break;
                case OperatorTypeOption.NotEqualTo:
                    query = Query.NE(item.Key, item.ToBsonValue());
                    break;
                case OperatorTypeOption.LessThan:
                    query = Query.LT(item.Key, item.ToBsonValue());
                    break;
                case OperatorTypeOption.GreaterThan:
                    query = Query.GT(item.Key, item.ToBsonValue());
                    break;
                case OperatorTypeOption.X:
                    break;
                case OperatorTypeOption.And:
                    break;
                case OperatorTypeOption.Or:
                    break;                    
                default:
                    query = Query.EQ(item.Key, item.ToBsonValue());
                    break;
            }
            return query;
        }

        private static BsonValue ToBsonValue(this TypedItem item)
        {
            BsonValue bson = null;
            Type type = item.Value.GetType();
            switch (type.Name.ToLower())
            {
                case "string":
                    bson = new BsonString(item.Value.ToString());
                    break;
                case "datetime":
                    bson = new BsonDateTime((DateTime)item.Value);
                    break;
                case "int16":                    
                case "int32":
                    bson = new BsonInt32((Int32)item.Value);
                    break;
                case "int64":
                    bson = new BsonInt64((Int64)item.Value);
                    break;
                case "double":
                    bson = new BsonDouble((double)item.Value);
                    break;
                case "boolean":
                    bson = new BsonBoolean((bool)item.Value);
                    break;
                case "byte[]":
                    bson = new BsonObjectId((byte[])item.Value);
                    break;
                default:
                    bson = new BsonString(item.Value.ToString());
                    break;
            }
            return bson;
        }

        private static string GetModelName<T>()
        {
            return GetModelType<T>().Name;
        }

        private static Type GetModelType<T>()
        {
            return Activator.CreateInstance<T>().GetType();
        }

    }

}
