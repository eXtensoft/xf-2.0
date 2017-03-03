// <copyright file="AppComponent.cs" company="eXtensoft, LLC">
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
    public sealed class AppComponent
    {

        #region local fields
        #endregion local fields

        #region properties

        public int AppComponentId { get; set; }

        public int AppId { get; set; }

        public int ComponentId { get; set; }

        #endregion properties

        #region constructors
        public AppComponent() { }
        #endregion constructors

    }
}
