// <copyright file="Selection.cs" company="eXtensoft, LLC">
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
    public sealed class Selection
    {

        #region local fields
        #endregion local fields

        #region properties

        public int SelectionId { get; set; }

        public string Display { get; set; }

        public string Token { get; set; }

        public int Sort { get; set; }

        public int GroupId { get; set; }

        public int MasterId { get; set; }

        public string Icon { get; set; }

        public string DisplayIcon { get; set; }

        #endregion properties

        #region constructors
        public Selection() { }
        #endregion constructors

    }
}
