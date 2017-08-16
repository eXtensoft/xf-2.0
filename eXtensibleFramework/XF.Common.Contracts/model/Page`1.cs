// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    [Serializable]
    public sealed class Page<T> where T : class, new()
    {
        [DataMember]
        public List<T> Items { get; set;}
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public string Marker { get; set; }
        [DataMember]
        public string Display { get; set; }
        [DataMember]
        public int PageCount
        {
            get
            {
                int numberofPages = 0;
                if (Total > 0 && PageSize > 0)
                {
                    numberofPages = (int)Math.Ceiling(Total / (double)PageSize);
                }
                return numberofPages;
            }
        }
    }

}
