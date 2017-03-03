// <copyright file="SolutionZone.cs" company="eXtensoft, LLC">
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
    public sealed class SolutionZone
    {

        #region local fields
        #endregion local fields

        #region properties

        public int SolutionZoneId { get; set; }

        public int SolutionId { get; set; }

        public int ZoneId { get; set; }

        public int DomainId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        #endregion properties

        #region constructors
        public SolutionZone() { }
        #endregion constructors

    }
}
