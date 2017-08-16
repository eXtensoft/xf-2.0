// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi

{
    using System;

    [Serializable]
    public sealed class ApiSession
    {

        #region local fields
        #endregion local fields

        #region properties

        public long Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Guid BasicToken { get; set; }

        public string BearerToken { get; set; }

        public int TenantId { get; set; }

        public int PatronId { get; set; }

        public int SsoPatronId { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public string PassKey { get; set; }

        public string LinesOfBusiness { get; set; }

        #endregion properties

        #region constructors
        public ApiSession() { }
        #endregion constructors

    }
}


