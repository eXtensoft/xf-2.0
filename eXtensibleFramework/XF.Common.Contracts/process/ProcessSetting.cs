// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Quality
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class ProcessSetting
    {
        [XmlAttribute("ctx")]
        public string Context { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement]
        public string ApiRoot { get; set; }

        [XmlElement("Datastore")]
        public List<DatastoreSetting> Datastores { get; set; }
    }
}
