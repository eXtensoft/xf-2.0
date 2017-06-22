// <copyright file="Zone.cs" company="eXtensoft, LLC">
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
    public sealed class Zone
    {

        #region local fields
        #endregion local fields

        #region properties

        public int ZoneId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Token { get; set; }

        #endregion properties

        #region constructors
        public Zone() { }
        #endregion constructors

    }
}
