// <copyright file="Server.cs" company="eXtensoft, LLC">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    [DataContract(Namespace ="")]
    [Serializable]
    public sealed class Server
    {

        #region local fields
        #endregion local fields

        #region properties

        public int ServerId { get; set; }

        public int OperatingSystemId { get; set; }

        public int HostPlatformId { get; set; }

        public int SecurityId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public string ExternalIP { get; set; }

        public string InternalIP { get; set; }

        public string Tags { get; set; }

        #endregion properties

        #region constructors
        public Server() { }
        #endregion constructors

    }
}
