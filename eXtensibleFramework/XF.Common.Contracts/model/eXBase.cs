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
    public abstract partial class eXBase 
    {

        #region properties

        private string createdAt;
        public string CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        [DataMember]
        public string ApplicationKey { get; set; }

        [DataMember]
        public string Zone { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DateTimeOffset Tds { get; set; }

        [DataMember]
        public List<TypedItem> Items { get; set; }

        [DataMember]
        public Guid MessageId { get; set; }

        #endregion properties


    }
}
