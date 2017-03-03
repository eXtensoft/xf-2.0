// <copyright file="ComponentConstruct.cs" company="eXtensoft, LLC">
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
    public sealed class ComponentConstruct
    {

        #region local fields
        #endregion local fields

        #region properties

        public int ComponentConstructId { get; set; }

        public int ComponentId { get; set; }

        public int ConstructId { get; set; }

        #endregion properties

        #region constructors
        public ComponentConstruct() { }
        #endregion constructors

    }
}
