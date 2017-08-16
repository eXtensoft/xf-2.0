// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Db
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class DbConfig
    {
        [XmlAttribute("appKey")]
        public string AppContextKey { get; set; }

        private List<Model> _Models = new List<Model>();
        public List<Model> Models
        {
            get { return _Models; }
            set { _Models = value; }
        }
    }
}
