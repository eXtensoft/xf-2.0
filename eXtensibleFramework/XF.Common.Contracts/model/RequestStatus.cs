// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class RequestStatus
    {
        [XmlAttribute("code")]
        public int Code { get; set; }

        [XmlAttribute("desc")]
        public string Description { get; set; }

    }
}
