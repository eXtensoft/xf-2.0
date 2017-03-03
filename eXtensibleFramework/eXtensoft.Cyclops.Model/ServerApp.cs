// <copyright file="ServerApp.cs" company="eXtensoft, LLC">
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
    public sealed class ServerApp
    {

        #region local fields
        #endregion local fields

        #region properties

        public int ServerAppId { get; set; }

        public int ServerId { get; set; }

        public int AppId { get; set; }

        public int ZoneId { get; set; }

        public int ScopeId { get; set; }

        #endregion properties

        public int DomainId { get; set; }

        public string Folderpath { get; set; }

        public string BackupFolderpath { get; set; }

        public string Icon { get; set; }

        #region server properties

        public int SecurityId { get; set; }

        public string ExternalIP { get; set; }

        public string InternalIP { get; set; }

        public string Url { get; set; }

        #endregion

        #region constructors
        public ServerApp() { }
        #endregion constructors

    }
}
