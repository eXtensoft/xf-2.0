// <copyright company="eXtensible Solutions, LLC" file="Projection.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    public class Projection : IProjection
    {
        #region properties

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Display { get; set; }

        [DataMember]
        public string DisplayAlt { get; set; }

        [DataMember]
        public string Typename { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public int IntVal { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public string Uri { get; set; }

        [DataMember]
        public string MasterId { get; set; }

        [DataMember]
        public IEnumerable<TypedItem> Items { get; set; }

        #endregion properties

    }
}
