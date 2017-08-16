// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
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
