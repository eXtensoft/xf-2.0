// <copyright file="Artifact.cs" company="eXtensoft, LLC">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    public sealed class Artifact
    {

        #region local fields
        #endregion local fields

        #region properties

        public int ArtifactId { get; set; }

        public Guid Id { get; set; }

        public int ArtifactTypeId { get; set; }

        public string Mime { get; set; }

        public long ContentLength { get; set; }

        public string OriginalFilename { get; set; }

        public string Location { get; set; }

        public string Title { get; set; }

        #endregion properties

        public int ArtifactScopeId { get; set; }

        public int ArtifactScopeTypeId { get; set; }

        public DateTime Tds { get; set; }

        public int DocumentId { get; set; }
        #region constructors
        public Artifact() { }
        #endregion constructors

    }
}
