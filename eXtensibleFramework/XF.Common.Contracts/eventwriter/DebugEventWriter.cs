// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;


    public class DebugEventWriter : EventWriterBase
    {

        protected override void Publish(EventTypeOption eventType, List<TypedItem> properties)
        {
            Debug.WriteLine(String.Format("An {0} occurred", eventType.ToString()));
            foreach (var property in properties)
            {
                Debug.WriteLine(String.Format("{0}:\t{1}", property.Key, property.Value.ToString()));
            }
        }

        protected override void Publish(eXMetric metric)
        {
            Debug.WriteLine(metric.ToString());
        }
    }
}
