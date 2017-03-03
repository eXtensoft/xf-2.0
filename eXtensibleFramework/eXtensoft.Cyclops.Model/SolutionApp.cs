// <copyright file="SolutionApp.cs" company="eXtensoft, LLC">
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
    public sealed class SolutionApp
    {

        #region local fields
        #endregion local fields

        #region properties

        public int SolutionAppId { get; set; }

        public int SolutionId { get; set; }

        public int AppId { get; set; }

        public int Sort { get; set; }

        #endregion properties

        #region constructors
        public SolutionApp() { }
        #endregion constructors

    }
}
