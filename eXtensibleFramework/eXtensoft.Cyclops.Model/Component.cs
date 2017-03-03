// <copyright file="Component.cs" company="eXtensoft, LLC">
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
    public sealed class Component
    {

        #region local fields
        #endregion local fields

        #region properties

        public int ComponentId { get; set; }

        public int ComponentTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        #endregion properties

        #region constructors
        public Component() { }
        #endregion constructors

    }
}
