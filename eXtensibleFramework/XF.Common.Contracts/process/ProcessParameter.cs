// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.Quality
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ProcessParameter
    {

        [XmlAttribute("src")]
        public string Source { get; set; }

        [XmlAttribute("direction")]
        public DirectionOption Direction { get; set; }

        [XmlAttribute("index")]
        public int Index { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("ns")]
        public string Namespace { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }


        public object Value { get; set; }



        public override string ToString()
        {
            return String.Format("{0}: {1}", Key, Value);
        }

    }
}
