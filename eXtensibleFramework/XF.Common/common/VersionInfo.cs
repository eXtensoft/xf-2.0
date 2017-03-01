// <copyright company="eXtensible Solutions LLC" file="VersionInfo.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
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
