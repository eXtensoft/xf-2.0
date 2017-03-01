// <copyright company="eXtensible Solutions, LLC" file="Error.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class...
    /// </summary>
    [Serializable]
    public class Error
    {

        public string User { get; set; }
        public string Zone { get; set; }
        public string AppKey { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public int Severity { get; set; }

        public DateTimeOffset Tds { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        public string StackTrace { get; set; }

        public Error() { }

        public Error(List<TypedItem> items)
        {
            Message = items.GetValueAs<string>(XFConstants.EventWriter.Message);
            Category = items.GetValueAs<string>(XFConstants.EventWriter.Category);
            Severity = items.GetValueAs<int>(XFConstants.EventWriter.ErrorSeverity);
        }
    }

}
