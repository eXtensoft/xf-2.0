// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;
   

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
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
