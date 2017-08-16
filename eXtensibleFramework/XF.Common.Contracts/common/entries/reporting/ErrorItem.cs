// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    public class ErrorItem 
    {
        [DataMember]
        public object Id { get; set; }
        [DataMember]
        public string ApplicationKey { get; set; }
        [DataMember]
        public string Zone { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public DateTimeOffset Tds { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Severity { get; set; }
        [DataMember]
        public string Verb { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string UICulture { get; set; }
        [DataMember]
        public string Dbtype { get; set; }
        [DataMember]
        public string Server { get; set; }
        [DataMember]
        public string Database { get; set; }
        [DataMember]
        public string CommandType { get; set; }
        [DataMember]
        public string Command { get; set; }
        [DataMember]
        public DateTime RequestBegin { get; set; }
        [DataMember]
        public DateTime DataRequestBegin { get; set; }
        [DataMember]
        public DateTime DataAccessBegin { get; set; }
        [DataMember]
        public DateTime DataAccessEnd { get; set; }
        [DataMember]
        public DateTime DataRequestEnd { get; set; }
        [DataMember]
        public DateTime RequestEnd { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public int Month { get; set; }
        [DataMember]
        public int Day { get; set; }
        [DataMember]
        public string DayOfWeek { get; set; }
        [DataMember]
        public int Hour { get; set; }
        [DataMember]
        public int Minute { get; set; }
        [DataMember]
        public string StackTrace { get; set; }
    }
}
