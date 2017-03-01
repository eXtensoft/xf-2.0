// <copyright company="eXtensible Solutions, LLC" file="SeverityType.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
   

    public enum SeverityType
    {
        None = 0,
        Critical = 1,
        Error = 2,
        Warning = 4,
        Information = 8,
        Verbose = 16
    }

}
