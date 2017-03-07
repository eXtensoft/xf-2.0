// <copyright company="eXtensoft, LLC" file="Note.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    [Serializable]
    public sealed class Note
    {


        #region local fields
        #endregion local fields

        #region properties
        [DataMember]
        public int NoteId { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string UserIdentity { get; set; }
        [DataMember]
        public DateTime Tds { get; set; }

        #endregion properties

        #region constructors
        public Note() { }
        #endregion constructors

    }

}


