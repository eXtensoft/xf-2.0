// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Quality
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class DatastoreSetting
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("key")]
        public string DatastoreConfigKey { get; set; }
    }
}
