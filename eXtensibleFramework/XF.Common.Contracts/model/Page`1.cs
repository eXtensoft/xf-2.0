// <copyright company="eXtensoft, LLC" file="Page_1.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    [Serializable]
    public sealed class Page<T> where T : class, new()
    {

        public List<T> Items { get; set;}

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }
    }

}
