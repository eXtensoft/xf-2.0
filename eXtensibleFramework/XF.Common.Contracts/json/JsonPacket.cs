// <copyright company="eXtensible Solutions LLC" file="JsonPacket.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    public class JsonPacket 
    {
        [DataMember]
        public string Typename { get; set; }
        [DataMember]
        public ApplicationContext Context { get; set; }
        [DataMember]
        public List<TypedItem> Items { get; set; }
        [DataMember]
        public ModelActionOption ModelAction { get; set; }
        [DataMember]
        public Criterion Criteria { get; set; }

        [DataMember]
        public string Buffer { get; set; }

    }
}
