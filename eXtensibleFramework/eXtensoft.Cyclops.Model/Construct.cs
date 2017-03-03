// <copyright file="Construct.cs" company="eXtensoft, LLC">
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
    public sealed class Construct
    {

        #region local fields
        #endregion local fields

        #region properties

        public int ConstructId { get; set; }

        public int ConstructTypeId { get; set; }

        public int ScopeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        #endregion properties

        #region constructors
        public Construct() { }
        #endregion constructors

    }
}
