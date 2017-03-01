// <copyright company="eXtensible Solutions LLC" file="eXMetric.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;
   

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class eXMetric : eXBase
    {
        public eXMetric() { }

        public eXMetric(List<TypedItem> list)
        {
            this.Items = list;
            this.Tds = DateTime.Now;
            this.Message = "Event";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Metric: {0}", Tds.ToLocalTime()));
            foreach (var item in Items)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
    }
}
