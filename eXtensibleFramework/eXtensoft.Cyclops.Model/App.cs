// <copyright file="App.cs" company="eXtensoft, LLC">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops
{
    using System;

    [Serializable]
    public sealed class App
    {

        #region local fields
        #endregion local fields

        #region properties

        public int AppId { get; set; }

        public int AppTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        #endregion properties

        #region constructors
        public App() { }
        #endregion constructors

    }
}
