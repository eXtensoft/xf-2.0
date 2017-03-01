// <copyright company="eXtensible Solutions, LLC" file="IEventWriter.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
