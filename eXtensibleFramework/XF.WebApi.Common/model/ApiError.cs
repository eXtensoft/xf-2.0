// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System;
    using System.Xml;

    [Serializable]
    public sealed class ApiError
    {

        #region local fields
        #endregion local fields

        #region properties


        public DateTime CreatedAt { get; set; }

        public string ApplicationKey { get; set; }

        public string Zone { get; set; }

        public string AppContextInstance { get; set; }

        public Guid MessageId { get; set; }

        public string Category { get; set; }

        public string Severity { get; set; }

        public string Message { get; set; }

        public XmlDocument XmlData { get; set; }

        public string Items { get; set; }

        #endregion properties

        #region constructors
        public ApiError() { }
        #endregion constructors

    }

}
