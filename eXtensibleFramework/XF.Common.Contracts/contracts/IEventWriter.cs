// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

    public interface IEventWriter
    {
        void WriteError(object errorMessage, SeverityType severity);

        void WriteError(object errorMessage, SeverityType severity, string errorCategory);

        void WriteError(object errorMessage, SeverityType severity, string errorCategory, IDictionary<string, object> properties);

        void WriteEvent(string eventMessage, IDictionary<string, object> properties);

        void WriteEvent(string eventMessage, string eventCategory, IDictionary<string, object> properties);

        void WriteEvent<T>(ModelActionOption modelAction, IDictionary<string, object> properties) where T : class, new();

        void WriteEvent<T>(ModelActionOption modelAction, object modelId, IDictionary<string, object> properties) where T : class, new();

        void WriteEvent<T>(ModelActionOption modelAction, T t, IDictionary<string, object> properties) where T : class, new();

        void WriteStatus(string modelType, object modelId, string modelStatus, string modelName);

        void WriteStatus(string modelType, object modelId, string modelStatus, string modelName, IDictionary<string,object> properties);

        void WriteStatus(string modelType, object modelId, string modelStatus, string modelName, DateTimeOffset statusEffective);

        void WriteStatus(string modelType, object modelId, string modelStatus, string modelName, DateTimeOffset statusEffective, IDictionary<string, object> properties);

        void WriteTask(string taskMessage, IDictionary<string, object> properties);

        void WriteMetric(eXMetric metric);
        
        void Write(EventTypeOption option,List<TypedItem> properties);


    }

}
