// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    public class VersionInfo
    {
        [DataMember]
        public int Major { get; set; }

        [DataMember]
        public int Minor { get; set; }

        [DataMember]
        public int Build { get; set; }

        [DataMember]
        public int Revision { get; set; }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}.{3}", Major.ToString(), Minor.ToString("#0"), Build.ToString("###0"), Revision.ToString());
        }
    }
}
